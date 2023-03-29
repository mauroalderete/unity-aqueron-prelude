using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Dialogue
{
    public class Greeting : MonoBehaviour
    {
        DialogueController dialogueController;
        List<Line> greetings;

        private void Awake()
        {
            dialogueController = GetComponent<DialogueController>();

            if (dialogueController == null) { return; }
            if (dialogueController.Located == null) { return; }
            if (dialogueController.Located.DialogueLines.Greetings.Count == 0) { return; }

            greetings = dialogueController.Located.DialogueLines.Greetings;
        }

        void Start()
        {
            if (greetings == null) { return; }

            int idx = Random.Range(0, greetings.Count);

            var response = dialogueController.SendDialogue(
                line: greetings[idx],
                qos: DialogueEventQOS.HighPriority);
        }
    }
}
