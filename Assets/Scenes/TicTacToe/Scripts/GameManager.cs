using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Menu,
        GameplayLoading,
        GameplayLoaded,
        GameplayCrossTurn,
        GameplayCircleTurn,
        GameEndTie,
        GameEndVictory,
        GameEndDefeat
    }

    public static GameManager Instance;
    [SerializeField] public bool playerPlayWithCrux;

    [SerializeField] public GameState state;
    public GameState State
    {
        get { return state; }
        private set { state = value; }
    }    

    [SerializeField] GameObject board;
    [SerializeField] UIManager uiManager;
    [SerializeField] EnemiesController enemyController;
    public int PlayerScore = 0;
    public int AIScore = 0;

    BoardController boardManager;

    [SerializeField] GlobalConfig config;

    private void Awake()
    {
        Instance = this;
        State = GameState.GameplayLoading;

        boardManager = board.GetComponent<BoardController>();
        boardManager.Initialized += BoardManager_Populated;
        boardManager.Reseted += BoardManager_Reseted;
        boardManager.BotWon += BoardManager_BotWon;
        boardManager.PlayerWon += BoardManager_PlayerWon;
        boardManager.TiedGame += BoardManager_TiedGame;
        boardManager.TurnOver += BoardManager_TurnOver;
    }

    private void BoardManager_PlayerWon(object sender, System.EventArgs e)
    {
        uiManager.ShowGameEnd(UIManager.GameEndResult.Win);
        State = GameState.GameEndVictory;
        enemyController.Defeat();
    }

    private void BoardManager_BotWon(object sender, System.EventArgs e)
    {
        uiManager.ShowGameEnd(UIManager.GameEndResult.Lose);
        State = GameState.GameEndDefeat;
        enemyController.Win();
    }

    private void BoardManager_TurnOver(object sender, System.EventArgs e)
    {
        PassTurn();

        if (!IsPlayerTurn())
        {
            Invoke("RequestAIMoveRoutine", Random.Range(0.2f, 2f));
        }
    }

    private void BoardManager_TiedGame(object sender, System.EventArgs e)
    {
        State = GameState.GameEndTie;

        uiManager.ShowGameEnd(UIManager.GameEndResult.Tie);

        enemyController.Tie();
    }

    private void BoardManager_Reseted(object sender, System.EventArgs e)
    {
        State = GameState.GameplayLoaded;
    }

    private void BoardManager_Populated(object sender, System.EventArgs e)
    {
        State = GameState.GameplayLoaded;
    }

    private void Update()
    {
        if (State == GameState.GameplayLoaded)
        {
            State = GameState.GameplayCrossTurn;
            if (!config.PlayWithCross)
            {
                Invoke("RequestAIMoveRoutine", Random.Range(0.2f, 2f));
            }
        }
    }

    private void RequestAIMoveRoutine() {
        enemyController.Execute();
    }

    public void PassTurn()
    {
        if (State == GameState.GameplayCrossTurn)
        {
            State = GameState.GameplayCircleTurn;
        }
        else
        {
            State = GameState.GameplayCrossTurn;
        }
    }

    public void ResetGame()
    {
        State = GameState.GameplayLoading;
        boardManager.Restart();
        uiManager.HideGameEnd();
        enemyController.Restart();
    }

    public void BackToMenu()
    {
        State = GameState.Menu;
        SceneManager.LoadScene("Scenes/TicTacToe/MenuScene", LoadSceneMode.Single);
    }

    public bool IsPlayerTurn()
    {
        return config.PlayWithCross && State == GameState.GameplayCrossTurn ||
            !config.PlayWithCross && State == GameState.GameplayCircleTurn;
    }
}
