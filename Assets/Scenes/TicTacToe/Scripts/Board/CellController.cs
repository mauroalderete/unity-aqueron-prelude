using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public class MarkedEventArgs : EventArgs
    {
        public int ID { get; set; }

        public MarkedEventArgs(int idx)
        {
            this.ID = idx;
        }
    }

    public enum CellStates
    {
        None,
        HoverVisible,
        HoverInvisible,
        Cross,
        Circle
    }

    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    bool lastIsPlayerTurn = false;

    public event EventHandler<MarkedEventArgs> Marked;

    [SerializeField] Sprite crux;
    [SerializeField] Sprite circle;
    [SerializeField] AudioClip CrossMarkClip;
    [SerializeField] AudioClip CircleMarkClip;

    [SerializeField] GlobalConfig config;

    [SerializeField] CellStates state;
    public CellStates State
    {
        get { return state; }
        private set { state = value; }
    }
    public int ID;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource= GetComponent<AudioSource>();
    }

    void Start()
    {
        State = CellStates.None;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        spriteRenderer.sprite = null;
    }

    private void Update()
    {
        if (State == CellStates.HoverVisible && Input.GetMouseButtonDown(0))
        {
            Mark();
        }

        if (lastIsPlayerTurn != GameManager.Instance.IsPlayerTurn())
        {
            if (GameManager.Instance.IsPlayerTurn())
            {
                if (State == CellStates.HoverInvisible)
                {
                    ShowHover();
                }
            } else
            {
                if (State == CellStates.HoverVisible)
                {
                    HideHover();
                }
            }
        }

        lastIsPlayerTurn = GameManager.Instance.IsPlayerTurn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { return; }
        if (State == CellStates.Cross || State == CellStates.Circle) { return; }

        if (GameManager.Instance.IsPlayerTurn())
        {
            ShowHover();
        }
        else
        {
            HideHover();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (State == CellStates.HoverVisible || State == CellStates.HoverInvisible)
        {
            EmptyCell();
        }
    }

    private void EmptyCell()
    {
        State = CellStates.None;
        spriteRenderer.sprite = null;
    }

    private void ShowHover()
    {
        State = CellStates.HoverVisible;
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
        if (config.PlayWithCross)
        {
            spriteRenderer.sprite = crux;
        }
        else
        {
            spriteRenderer.sprite = circle;
        }
    }

    private void HideHover()
    {
        State = CellStates.HoverInvisible;
        spriteRenderer.sprite = null;
    }

    public void Mark()
    {
        if (State != CellStates.Cross && State != CellStates.Circle)
        {
            if (GameManager.Instance.State == GameManager.GameState.GameplayCrossTurn)
            {
                State = CellStates.Cross;
                spriteRenderer.sprite = crux;

                if (CrossMarkClip != null)
                {
                    audioSource.clip = CrossMarkClip;
                    audioSource.Play();
                }
            }
            else
            {
                State = CellStates.Circle;
                spriteRenderer.sprite = circle;

                if (CircleMarkClip != null)
                {
                    audioSource.clip = CircleMarkClip;
                    audioSource.Play();
                }
            }
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

            OnMarked();
        }
    }

    private void OnMarked()
    {
        var handler = Marked;
        if (handler != null)
        {
            handler(this, new MarkedEventArgs(ID));
        }
    }

    public void Reset()
    {
        if (State == CellStates.Cross || State == CellStates.Circle) {
            State = CellStates.None;
            spriteRenderer.sprite = null;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
