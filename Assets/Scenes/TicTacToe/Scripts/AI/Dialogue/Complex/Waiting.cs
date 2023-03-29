using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Dialogue
{
    public class Waiting : MonoBehaviour
    {
        [SerializeField] Utils.Range<float> waitingTime;

        BoardController boardController;
        DialogueController dialogueController;
        List<Line> waitingDialogues;

        bool wait;
        float targetTime;

        private void Awake()
        {
            dialogueController = GetComponent<DialogueController>();

            if (dialogueController == null) { return; }
            if (dialogueController.Located == null) { return; }
            if (dialogueController.Located.DialogueLines.Waiting.Count == 0) { return; }

            boardController = FindObjectOfType<BoardController>();
            if (boardController == null) { return; }

            waitingDialogues = dialogueController.Located.DialogueLines.Waiting;
            
            boardController.Reseted += BoardController_Reseted;
            boardController.EndedGame += BoardController_EndedGame;
            boardController.TurnOver += BoardController_TurnOver;

            dialogueController.DialogueFinished += DialogueController_DialogueFinished;
        }

        private void Start()
        {
            ResetWaiting();
        }

        private void BoardController_EndedGame(object sender, EventArgs e)
        {
            StopWaiting();
        }

        private void BoardController_TurnOver(object sender, System.EventArgs e)
        {
            ResetWaiting();
        }

        private void BoardController_Reseted(object sender, System.EventArgs e)
        {
            ResetWaiting();
        }

        private void DialogueController_DialogueFinished(object sender, EventArgs e)
        {
            ResetWaiting();
        }

        private void ResetWaiting()
        {
            if (waitingDialogues == null) { return; }

            if (GameManager.Instance.IsPlayerTurn() ||
                GameManager.Instance.State == GameManager.GameState.GameplayLoaded ||
                GameManager.Instance.State == GameManager.GameState.GameplayLoading)
            {
                targetTime = UnityEngine.Random.Range(waitingTime.From, waitingTime.To);
                wait = true;
            } 
        }

        private void StopWaiting()
        {
            if (waitingDialogues == null) { return; }
            
            wait = false;
        }

        private void Update()
        {
            if (waitingDialogues == null) { return; }
            if (!wait) { return; }

            targetTime -= Time.deltaTime;
            if (targetTime < 0)
            {
                int idx = UnityEngine.Random.Range(0, waitingDialogues.Count);

                dialogueController.SendDialogue(
                    line: waitingDialogues[idx],
                    qos: DialogueEventQOS.AtMostOnce);

                StopWaiting();
            }
        }
    }
}
