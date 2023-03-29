using Dialogue;
using System;
using TMPro;
using UI.DialogueBox;
using UnityEngine;

namespace UI.DialogueBox
{
    public class DialogueBoxController : MonoBehaviour
    {
        [SerializeField] private DialogueBoxContext context;

        private void Awake()
        {
            context.Queue = new System.Collections.Generic.List<DialogueEventArgs>();
            context.CurrentState = new WaitDialogueState();
        }

        public void LoadDialogController(DialogueController dialogueController)
        {
            context.DialogueController = dialogueController;
            context.DialogueController.DialogueEmited += DialogueController_DialogueEmited;
        }

        private void DialogueController_DialogueEmited(object sender, EventArgs e)
        {
            context.DialogueEntered = (DialogueEventArgs)e;

            context.TransitionToState(new ShowDialogueState());
        }

    }
}
