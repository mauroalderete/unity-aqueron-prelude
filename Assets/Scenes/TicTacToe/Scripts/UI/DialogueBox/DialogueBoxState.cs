using Dialogue;

namespace UI.DialogueBox
{
    public interface IDialogueBoxState
    {
        void EnterState(DialogueBoxContext context);
        void WaitDialogue(DialogueBoxContext context);
        void ShowDialogue(DialogueBoxContext context);
    }
}
