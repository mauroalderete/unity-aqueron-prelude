using UnityEngine;

public class RubyEnemy : EnemyBase
{
    [SerializeField] GlobalConfig config;
    [SerializeField] int lossingStreak;

    Decision.IDecisionMoveProvider moveProvider;

    int turn;

    private void Start()
    {
        turn = 0;
        lossingStreak = 0;

        OffSuperMode();
    }

    public override void Restart(GameManager.GameState lastGameResult)
    {
        turn = 0;
        switch (lastGameResult)
        {
            case GameManager.GameState.GameEndTie:
                {
                    if (superMode)
                    {
                        lossingStreak--;
                        if (lossingStreak <= 0)
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
                        lossingStreak+=2;
                        if (lossingStreak >= tolerance)
                        {
                            OnSuperMode();
                        }
                    }
                }
                break;
        }
    }

    public override Decision.IDecisionMove Move(string gameState)
    {
        turn++;
        return moveProvider.GetMove(gameState);
    }

    public int UtilityFunction(string gameState)
    {
        if (Rules.Tie(gameState))
        {
            return 0;
        }

        if (Rules.CrossWin(gameState))
        {
            if (config.PlayWithCross)
            {
                return -100;
            }
            else
            {
                return 100;
            }
        }

        if (Rules.CircleWin(gameState))
        {
            if (config.PlayWithCross)
            {
                return 100;
            }
            else
            {
                return -100;
            }
        }

        return 0;
    }

    protected override void OnSuperMode()
    {
        superMode = true;
        moveProvider = new Decision.Minimax.AlphaBetaPruning(
            maximizer: new Decision.Minimax.Roll(config.PlayWithCross ? "o" : "x"),
            minimizer: new Decision.Minimax.Roll(config.PlayWithCross ? "x" : "o"),
            maxDepth: 8,
            config);
    }

    protected override void OffSuperMode()
    {
        superMode = false;

        Debug.Log(turn);

        if (turn <= 1)
        {
            moveProvider = new Decision.Random.Rational(15f, 0.1f, 0.5f);
        } else
        {
            moveProvider = new Decision.Minimax.AlphaBetaPruning(
            maximizer: new Decision.Minimax.Roll(config.PlayWithCross ? "o" : "x"),
            minimizer: new Decision.Minimax.Roll(config.PlayWithCross ? "x" : "o"),
            maxDepth: 2,
            config);
        }
    }
}
