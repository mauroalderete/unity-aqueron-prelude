using UnityEngine;

public class SilverEnemy : EnemyBase
{
    Decision.IDecisionMoveProvider moveProvider;
    [SerializeField] int lossingStreak;    

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
                        lossingStreak = 0;
                        OffSuperMode();
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
        moveProvider = new Decision.Random.Rational(15f, 0.05f, 1f);
    }

    protected override void OffSuperMode()
    {
        superMode = false;
        moveProvider = new Decision.Random.Simple();
    }
}
