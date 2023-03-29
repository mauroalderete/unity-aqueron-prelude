using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class Rematch : MonoBehaviour
    {
        BoardController boardController;
        DialogueController dialogueController;
        List<Line> rematchDialogues;

        [SerializeField] float rate;

        private void Awake()
        {
            dialogueController = GetComponent<DialogueController>();

            if (dialogueController == null) { return; }
            if (dialogueController.Located == null) { return; }
            if (dialogueController.Located.DialogueLines.Rematch.Count == 0) { return; }

            boardController = FindObjectOfType<BoardController>();
            
            if (boardController == null) { return; }

            rematchDialogues = dialogueController.Located.DialogueLines.Rematch;
            boardController.Reseted += BoardController_Reseted;
        }

        private void BoardController_Reseted(object sender, System.EventArgs e)
        {
            if (rematchDialogues == null) { return; }
            if ( Random.Range(0,1) > rate) { return; }

            int idx = Random.Range(0, rematchDialogues.Count);

            dialogueController.SendDialogue(
                line: rematchDialogues[idx],
                qos: DialogueEventQOS.AtLeastOnce);
        }
    }
}
