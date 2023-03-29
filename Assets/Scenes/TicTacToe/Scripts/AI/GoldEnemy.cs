using System;
using UnityEngine;

public class GoldEnemy : EnemyBase
{
    [SerializeField] GlobalConfig config;
    [SerializeField] int lossingStreak;

    Decision.IDecisionMoveProvider moveProvider;

    public void Start()
    {
        lossingStreak = 0;

        OffSuperMode();
    }

    public override Decision.IDecisionMove Move(string gameState)
    {
        return moveProvider.GetMove(gameState);
    }

    public override void Restart(GameManager.GameState lastGameResult)
    {
        switch (lastGameResult)
        {
            case GameManager.GameState.GameEndTie:
                {
                    if (superMode)
                    {
                        lossingStreak--;
                        if (lossingStreak < 0)
                        {
                            OffSuperMode();
                        }
                    }
                }
                break;
            case GameManager.GameState.GameEndVictory:
                {
                    if (superMode)
                    {
                        lossingStreak = 0;
                        OffSuperMode();
                    }
                }
                break;
            case GameManager.GameState.GameEndDefeat:
                {
                    if (!superMode)
                    {
                        lossingStreak++;
                        if (lossingStreak >= tolerance)
                        {
                            OnSuperMode();
                        }
                    }
                }
                break;
        }
    }

    protected override void OnSuperMode()
    {
        superMode = true;
        moveProvider = new Decision.Minimax.AlphaBetaPruning(
            maximizer: new Decision.Minimax.Roll(config.PlayWithCross ? "o" : "x"),
            minimizer: new Decision.Minimax.Roll(config.PlayWithCross ? "x" : "o"),
            maxDepth: 2,
            config);
    }

    protected override void OffSuperMode()
    {
        superMode = false;
        moveProvider = new Decision.Random.Rational(15f, 0.05f, 1f);
    }
}
