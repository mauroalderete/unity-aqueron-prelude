using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dialogue
{
    public class DialogueController : MonoBehaviour
    {
        [SerializeField] protected List<Dialogue> dialogues;
        protected Dialogue located;

        public List<Dialogue> Dialogues { get { return dialogues; } }
        public Dialogue Located { get { return located; } }

        public event EventHandler DialogueEmited;
        public event EventHandler DialogueFinished;

        private void Awake()
        {
            located = dialogues.SingleOrDefault(d => d.Language == Application.systemLanguage);
            if (located != null) { return; }

            located = null;
        }

        public SendDialogueResult SendDialogue(
            Line line,
            DialogueEventQOS qos)
        {
            EventHandler handler = DialogueEmited;
            if (handler == null) {
                return new SendDialogueResult(Guid.Empty, DialogueEventResult.Error);
            }

            DialogueEventArgs dialogueEventArgs = new DialogueEventArgs(
                message: line.Message,
                qos: qos);

            handler(this, dialogueEventArgs);

            return new SendDialogueResult(dialogueEventArgs.GUID, dialogueEventArgs.Result);
        }

        public void NotifyDialogueFinished(DialogueEventArgs dialogueEventArgs)
        {
            EventHandler handler = DialogueFinished;
            if (handler != null)
            {
                handler(this, dialogueEventArgs);
            }
        }

        public class SendDialogueResult
        {
            public Guid GUID { get; protected set; }
            public DialogueEventResult Result { get; protected set; }

            public SendDialogueResult(Guid guid, DialogueEventResult result)
            {
                GUID = guid;
                Result = result;
            }
        }
    }
}
