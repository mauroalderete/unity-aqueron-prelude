using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class Defeat : MonoBehaviour
    {
        BoardController boardController;
        DialogueController dialogueController;
        List<Line> defeatDialogues;

        private void Awake()
        {
            dialogueController = GetComponent<DialogueController>();

            if (dialogueController == null) { return; }
            if (dialogueController.Located == null) { return; }
            if (dialogueController.Located.DialogueLines.Defeat.Count == 0) { return; }

            defeatDialogues = dialogueController.Located.DialogueLines.Defeat;

            boardController = FindObjectOfType<BoardController>();
            if (boardController != null)
            {
                boardController.PlayerWon += BoardController_PlayerWon;
            }
        }

        private void BoardController_PlayerWon(object sender, System.EventArgs e)
        {
            if (defeatDialogues == null) { return; }

            int idx = Random.Range(0, defeatDialogues.Count);

            dialogueController.SendDialogue(defeatDialogues[idx], DialogueEventQOS.HighPriority);
        }
    }
}
