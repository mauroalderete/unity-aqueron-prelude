using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class Tie : MonoBehaviour
    {
        BoardController boardController;
        DialogueController dialogueController;
        List<Line> tieDialogues;

        private void Awake()
        {
            dialogueController = GetComponent<DialogueController>();

            if (dialogueController == null) { return; }
            if (dialogueController.Located == null) { return; }
            if (dialogueController.Located.DialogueLines.Tie.Count == 0) { return; }

            tieDialogues = dialogueController.Located.DialogueLines.Tie;

            boardController = FindObjectOfType<BoardController>();
            if (boardController != null)
            {
                boardController.TiedGame += BoardController_TiedGame;
            }
        }

        private void BoardController_TiedGame(object sender, System.EventArgs e)
        {
            if (tieDialogues == null) { return; }

            int idx = Random.Range(0, tieDialogues.Count);

            dialogueController.SendDialogue(
                line: tieDialogues[idx],
                qos: DialogueEventQOS.HighPriority);
        }
    }
}
