using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.DialogueBox
{
    public class WaitDialogueState : IDialogueBoxState
    {
        public void DequeueDialog(DialogueBoxContext context) { }

        public void EnterState(DialogueBoxContext context) {
            context.CurrentDialogue = null;
            context.DialogueEntered = null;
            context.Queue.Clear();
        }

        public void HideDialogue(DialogueBoxContext context) { }

        public void NotifyDialogueFinished(DialogueBoxContext context) { }

        public void ProcessDialogueEntered(DialogueBoxContext context) { }

        public void QueueUpDialog(DialogueBoxContext context) { }

        public void ShowDialogue(DialogueBoxContext context) {
            context.TransitionToState(new ShowDialogueState());
        }

        public void WaitDialogue(DialogueBoxContext context) { }
    }

}
