using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[Serializable]
public class UIGameplayPlayer
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Mark;
}

[Serializable]
public class UIGameplayAI
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Label;
    public TextMeshProUGUI Mark;
}

public class UIManager : MonoBehaviour
{
    public enum GameEndResult
    {
        Win,
        Lose,
        Tie
    }

    [SerializeField] UIGameplayPlayer playerUI;
    [SerializeField] UIGameplayAI aiUI;
    [SerializeField] GameObject gameEnd;
    [SerializeField] TextMeshProUGUI labelGameEnd;
    [SerializeField] AudioClip ButtonAgainClip;
    [SerializeField] AudioClip ButtonBackToMenuClip;

    [SerializeField] GlobalConfig config;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        playerUI.Score.text = 0.ToString();
        aiUI.Score.text = 0.ToString();
        aiUI.Label.text = config.EnemiesConfig.Selected.GetComponent<EnemyBase>().Title;

        if (config.PlayWithCross)
        {
            playerUI.Mark.text = "X";
            playerUI.Mark.color = Color.red;
            aiUI.Mark.text = "O";
            aiUI.Mark.color = new Color(0, 1, 1, 1);
        }
        else
        {
            playerUI.Mark.text = "O";
            playerUI.Mark.color = new Color(0, 1, 1, 1);
            aiUI.Mark.text = "X";
            aiUI.Mark.color = Color.red;
        }
    }

    public void ShowGameEnd(GameEndResult result)
    {
        gameEnd.SetActive(true);

        switch (result)
        {
            case GameEndResult.Win:
                {
                    labelGameEnd.text = "YOU WIN";

                    GameManager.Instance.PlayerScore++;
                    playerUI.Score.text = GameManager.Instance.PlayerScore.ToString();
                }
                break;
            case GameEndResult.Lose:
                {
                    labelGameEnd.text = "DEFEAT";

                    GameManager.Instance.AIScore++;
                    aiUI.Score.text = GameManager.Instance.AIScore.ToString();
                }
                break;
            case GameEndResult.Tie:
                {
                    labelGameEnd.text = "TIE";
                }
                break;
        }
    }

    public void HideGameEnd()
    {
        gameEnd.SetActive(false);
    }

    public void OnButtonAgainClick()
    {
        audioSource.Stop();
        audioSource.clip = ButtonAgainClip;
        audioSource.Play();
        Invoke("AgainRoutine", ButtonAgainClip.length);
    }

    public void OnButtonBackToMenuClick()
    {
        audioSource.Stop();
        audioSource.clip = ButtonBackToMenuClip;
        audioSource.Play();

        Invoke("BackToMenuRoutine", ButtonBackToMenuClip.length);
    }

    private void AgainRoutine()
    {
        GameManager.Instance.ResetGame();
    }

    private void BackToMenuRoutine()
    {
        GameManager.Instance.BackToMenu();
    }
}
