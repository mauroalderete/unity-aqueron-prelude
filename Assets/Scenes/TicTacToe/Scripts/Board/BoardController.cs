using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardController : MonoBehaviour
{
    [SerializeField] CellCollection cellCollection;

    [SerializeField] AudioClip WinClip;
    [SerializeField] AudioClip LoseClip;
    [SerializeField] AudioClip TieClip;

    AudioSource audioSource;

    public event EventHandler Initialized;
    public event EventHandler Reseted;
    public event EventHandler PlayerWon;
    public event EventHandler BotWon;
    public event EventHandler TiedGame;
    public event EventHandler TurnOver;
    public event EventHandler EndedGame;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Initialize();

        DispatchEvent(Initialized, EventArgs.Empty);
    }

    private void Initialize()
    {
        foreach (GameObject obj in cellCollection.Collection)
        {
            CellController cell = obj.GetComponent<CellController>();
            
            cell.Marked += Cell_Marked;
        }
    }

    public void Restart()
    {
        audioSource.Stop();

        foreach (GameObject obj in cellCollection.Collection)
        {
            CellController cell = obj.GetComponent<CellController>();

            cell.Reset();
        }

        DispatchEvent(Reseted, EventArgs.Empty);
    }

    public string CurrentGameState()
    {
        return cellCollection.Datagram();
    }

    private void Cell_Marked(object sender, CellController.MarkedEventArgs e)
    {
        string gameDatagram = CurrentGameState();

        if (Rules.SomeWinner(gameDatagram))
        {
            if (GameManager.Instance.IsPlayerTurn())
            {
                if (WinClip != null)
                {
                    audioSource.clip = WinClip;
                    audioSource.Play();
                }

                DispatchEvent(EndedGame, EventArgs.Empty);
                DispatchEvent(PlayerWon, EventArgs.Empty);

            } else
            {
                if (LoseClip != null)
                {
                    audioSource.clip = LoseClip;
                    audioSource.Play();
                }

                DispatchEvent(EndedGame, EventArgs.Empty);
                DispatchEvent(BotWon, EventArgs.Empty);
            }

            DispatchEvent(EndedGame, EventArgs.Empty);
            return;
        }
        
        if (Rules.Tie(gameDatagram))
        {
            if (TieClip != null)
            {
                audioSource.clip = TieClip;
                audioSource.Play();
            }

            DispatchEvent(EndedGame, EventArgs.Empty);
            DispatchEvent(TiedGame, EventArgs.Empty);
            return;
        }

        DispatchEvent(TurnOver, EventArgs.Empty);
    }

    public void MarkCell(int idx)
    {
        CellController cell = cellCollection.Collection[idx].GetComponent<CellController>();

        cell.Mark();
    }

    private void DispatchEvent(EventHandler handler, EventArgs eventArgs)
    {
        if (handler != null)
        {
            handler(this, eventArgs);
        }
    }
}
