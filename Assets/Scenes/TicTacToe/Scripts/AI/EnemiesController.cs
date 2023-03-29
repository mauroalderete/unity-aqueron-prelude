using Dialogue;
using UI.DialogueBox;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] BoardController board;
    [SerializeField] GlobalConfig config;
    [SerializeField] DialogueBoxController dialogueBoxController;

    GameObject enemy;
    EnemyBase script;
    Animator anim;

    GameManager.GameState lastGameState;

    GameManager.GameState lastResult;

    private void Start()
    {
        var selected = config.EnemiesConfig.Selected;
        if (selected == null)
        {
            script = null;
            return;
        }

        enemy = Instantiate(selected, transform);
        script = enemy.GetComponent<EnemyBase>();
        anim = enemy.GetComponent<Animator>();

        if (dialogueBoxController != null )
        {
            var dialogController = enemy.GetComponent<DialogueController>();
            if ( dialogController != null )
            {
                dialogueBoxController.LoadDialogController(dialogController);
            }
        }

        Tie();
    }

    public void Restart()
    {
        script.Restart(lastResult);
    }

    public void Execute()
    {
        if (script == null)
        {
            Debug.LogError("Not AI player selected yet");
            GameManager.Instance.PassTurn();
            return;
        }

        string gameState = board.CurrentGameState();

        Decision.IDecisionMove move = script.Move(gameState);

        board.MarkCell(move.Cell);
    }

    public void Win()
    {
        lastResult = GameManager.GameState.GameEndVictory;
    }

    public void Defeat()
    {
        lastResult = GameManager.GameState.GameEndDefeat;
    }

    public void Tie()
    {
        lastResult = GameManager.GameState.GameEndTie;
    }

    private void Update()
    {
        if ( lastGameState != GameManager.Instance.State )
        {
            switch ( GameManager.Instance.State )
            {
                case GameManager.GameState.GameEndVictory:
                    {
                        anim.SetBool("hitted", true);
                    }
                    break;
                case GameManager.GameState.GameEndTie:
                    {
                        anim.SetBool("hitted", true);
                    }
                    break;
                default:
                    {
                        anim.SetBool("hitted", false);
                    }break;
            }
        }
        
        lastGameState = GameManager.Instance.State;
    }
}
