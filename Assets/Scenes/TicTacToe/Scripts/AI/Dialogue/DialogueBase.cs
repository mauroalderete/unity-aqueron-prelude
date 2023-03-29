using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public class Line
    {
        int uses;

        [SerializeField] string message;

        public int Uses { get { return uses; } protected set { uses = value; } }
        public string Message { get { return message; } protected set { message = value; } }
    }

    [Serializable]
    public class DialogueLines
    {
        [SerializeField] List<Line> greetings;
        [SerializeField] List<Line> rematch;
        [SerializeField] List<Line> waiting;
        [SerializeField] List<Line> distractions;
        [SerializeField] List<Line> jokes;
        [SerializeField] List<Line> victory;
        [SerializeField] List<Line> defeat;
        [SerializeField] List<Line> tie;
        [SerializeField] List<Line> story;

        public List<Line> Greetings { get { return greetings; } protected set { greetings = value; } }
        public List<Line> Rematch { get { return rematch; } protected set { rematch = value; } }
        public List<Line> Waiting { get { return waiting; } protected set { waiting = value; } }
        public List<Line> Distractions { get { return distractions; } protected set { distractions = value; } }
        public List<Line> Jokes { get { return jokes; } protected set { jokes = value; } }
        public List<Line> Victory { get { return victory; } protected set { victory = value; } }
        public List<Line> Defeat { get { return defeat; } protected set { defeat = value; } }
        public List<Line> Tie { get { return tie; } protected set { tie = value; } }
        public List<Line> Story { get { return story; } protected set { story = value; } }
    }

    [Serializable]
    public class Dialogue
    {
        [SerializeField] SystemLanguage language;
        [SerializeField] DialogueLines dialogueLines;

        public SystemLanguage Language { get { return language; } protected set { language = value; } }
        public DialogueLines DialogueLines { get { return dialogueLines; } protected set { dialogueLines = value; } }
    }

    public class DialogueEventArgs : EventArgs
    {
        public string Message { get; protected set; }
        public DialogueEventResult Result { get; set; }
        public DialogueEventQOS QOS { get; protected set; }

        public Guid GUID { get; protected set; }

        public DialogueEventArgs(
            string message,
            DialogueEventQOS qos)
        {
            Message = message;
            QOS = qos;
            GUID = Guid.NewGuid();
        }
    }

    public enum DialogueEventResult
    {
        Success,
        Busy,
        Queued,
        Error
    }

    public enum DialogueEventQOS
    {
        AtMostOnce,
        AtLeastOnce,
        HighPriority,
    }
}
