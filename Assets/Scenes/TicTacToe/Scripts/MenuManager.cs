using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    public bool PlayCrux { get; private set; }

    [SerializeField] AudioClip PlayClip;

    AudioSource audioSource;

    [SerializeField] GlobalConfig config;

    [SerializeField] GameObject EnemySelector;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWithCrux()
    {
        PlayCrux = true;
        config.PlayWithCross = true;

        LoadGameplay();
    }

    public void PlayWithCircle()
    {
        PlayCrux = false;
        config.PlayWithCross = false;

        LoadGameplay();
    }

    private void LoadGameplay()
    {
        audioSource.Stop();
        audioSource.clip = PlayClip;
        audioSource.Play();
 
        Invoke("LoadGameplayRoutine", PlayClip.length*2);
    }

    private void LoadGameplayRoutine()
    {
        SceneManager.LoadScene("Scenes/TicTacToe/GameScene", LoadSceneMode.Single);
    }
}
