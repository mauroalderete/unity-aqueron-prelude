using System;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogue
{
    public class Distraction : MonoBehaviour
    {
        [SerializeField] Utils.Range<float> distractionTime;
        [SerializeField] int maxNumberDialogue;

        BoardController boardController;
        DialogueController dialogueController;
        List<Line> distractionDialogues;

        int uses;
        bool enable;
        float targetTime;
        Guid dialogueEmited;

        private void Awake()
        {
            dialogueController = GetComponent<DialogueController>();

            if (dialogueController == null) { return; }
            if (dialogueController.Located == null) { return; }

            boardController = FindObjectOfType<BoardController>();
            if (boardController == null ) { return; }

            distractionDialogues = new List<Line>();
            distractionDialogues.AddRange(dialogueController.Located.DialogueLines.Distractions);
            distractionDialogues.AddRange(dialogueController.Located.DialogueLines.Jokes);
            distractionDialogues.AddRange(dialogueController.Located.DialogueLines.Story);

            dialogueController.DialogueFinished += DialogueController_DialogueFinished;

            boardController.EndedGame += BoardController_EndGame;
            boardController.Reseted += BoardController_Reseted;
        }

        private void Start()
        {
            ResetDistractionSeries();
        }

        private void BoardController_EndGame(object sender, EventArgs e)
        {
            enable = false;
        }

        private void BoardController_Reseted(object sender, System.EventArgs e)
        {
            ResetDistractionSeries();
        }

        private void DialogueController_DialogueFinished(object sender, EventArgs e)
        {
            if (e ==null) { return; }

            if (GameManager.Instance.State == GameManager.GameState.GameplayLoaded ||
                        GameManager.Instance.State == GameManager.GameState.GameplayLoading ||
                        GameManager.Instance.State == GameManager.GameState.GameplayCircleTurn ||
                        GameManager.Instance.State == GameManager.GameState.GameplayCrossTurn)
            {
                ResetDistractionSeries();
            }
        }

        private void ResetDistractionSeries()
        {
            if (distractionDialogues == null) { return; }

            dialogueEmited = Guid.Empty;
            enable = true;
            uses = 0;
            targetTime = UnityEngine.Random.Range(distractionTime.From, distractionTime.To);
        }

        private void Update()
        {
            if (!enable) { return; }
            if (uses >= maxNumberDialogue) { return; }

            targetTime -= Time.deltaTime;
            if (targetTime < 0) {

                int idx = UnityEngine.Random.Range(0, distractionDialogues.Count);

                var response = dialogueController.SendDialogue(
                    line: distractionDialogues[idx],
                    qos: DialogueEventQOS.AtMostOnce);

                if (response.Result == DialogueEventResult.Success)
                {
                    dialogueEmited = response.GUID;
                    enable = false;
                    uses++;
                }
            }
        }
    }
}
