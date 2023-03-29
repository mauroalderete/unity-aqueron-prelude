using System;
using System.Collections.Generic;
using UnityEngine;

using Dialogue;
using TMPro;

namespace UI.DialogueBox
{
    [Serializable]
    public class DialogueBoxContext
    {
        public DialogueEventArgs CurrentDialogue { get; set; }
        public DialogueEventArgs DialogueEntered { get; set; }
        public List<DialogueEventArgs> Queue { get; set; } = new List<DialogueEventArgs>();

        [SerializeField] private DialogueController dialogueController;
        [SerializeField] private GameObject window;
        [SerializeField] private TextMeshProUGUI contentText;

        public DialogueController DialogueController { get { return dialogueController; } set { dialogueController = value; } }
        public GameObject Window { get { return window; } set { window = value; } }
        public TextMeshProUGUI ContentText { get { return contentText; } set { contentText = value; } }

        public IDialogueBoxState CurrentState { get; set; }

        public void TransitionToState(IDialogueBoxState state)
        {
            CurrentState = state;
            CurrentState.EnterState(this);
        }
    }

}
