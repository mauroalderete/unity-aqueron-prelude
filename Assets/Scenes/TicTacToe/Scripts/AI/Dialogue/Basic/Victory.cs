using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class Victory : MonoBehaviour
    {
        BoardController boardController;
        DialogueController dialogueController;
        List<Line> victoryDialogues;

        private void Awake()
        {
            dialogueController = GetComponent<DialogueController>();

            if (dialogueController == null) { return; }
            if (dialogueController.Located == null) { return; }
            if (dialogueController.Located.DialogueLines.Victory.Count == 0) { return; }

            victoryDialogues = dialogueController.Located.DialogueLines.Victory;

            boardController = FindObjectOfType<BoardController>();
            if (boardController != null)
            {
                boardController.BotWon += BoardController_BotWon;
            }
        }

        private void BoardController_BotWon(object sender, System.EventArgs e)
        {
            if (victoryDialogues == null) { return; }

            int idx = Random.Range(0, victoryDialogues.Count);

            dialogueController.SendDialogue(
                line: victoryDialogues[idx],
                qos: DialogueEventQOS.HighPriority);
        }
    }
}
