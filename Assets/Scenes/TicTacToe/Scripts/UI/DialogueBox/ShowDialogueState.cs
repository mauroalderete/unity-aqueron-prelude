using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dialogue;
using System;
using System.Threading.Tasks;

namespace UI.DialogueBox
{
    public class ShowDialogueState : IDialogueBoxState
    {
        public void EnterState(DialogueBoxContext context)
        {
            context.Queue.Add(context.DialogueEntered);
            context.DialogueEntered.Result = DialogueEventResult.Success;
            DequeueDialogue(context);
        }

        private async void DequeueDialogue(DialogueBoxContext context)
        {
            if ( context.CurrentDialogue != null )
            {
                context.DialogueController.NotifyDialogueFinished(context.CurrentDialogue);
            }

            if (context.Queue.Count == 0)
            {
                context.Window.SetActive(false);
                context.ContentText.text = string.Empty;

                context.TransitionToState(new WaitDialogueState());
                return;
            }

            context.CurrentDialogue = context.Queue[0];
            context.Queue.RemoveAt(0);

            context.ContentText.text = Utils.DialogueCurator.Curate(context.CurrentDialogue.Message);
            context.Window.SetActive(true);

            await DequeueDialogueRoutine(context);
        }

        private async Task DequeueDialogueRoutine(DialogueBoxContext context)
        {
            await Task.Delay(3000);

            DequeueDialogue((DialogueBoxContext)context);
        }

        public void ShowDialogue(DialogueBoxContext context) {
            switch (context.CurrentDialogue.QOS)
            {
                case DialogueEventQOS.AtMostOnce:
                    {
                        context.DialogueEntered.Result = DialogueEventResult.Busy;
                    }
                    break;
                case DialogueEventQOS.AtLeastOnce:
                    {
                        context.Queue.Add(context.DialogueEntered);
                        context.DialogueEntered.Result = DialogueEventResult.Queued;
                    }
                    break;
                case DialogueEventQOS.HighPriority:
                    {
                        context.Queue.Clear();
                        context.Queue.Add(context.DialogueEntered);
                        context.TransitionToState(new ShowDialogueState());
                    }
                    break;
            }
        }

        public void WaitDialogue(DialogueBoxContext context) { }
    }

}