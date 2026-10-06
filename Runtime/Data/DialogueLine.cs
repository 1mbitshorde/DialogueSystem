using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

namespace OneM.DialogueSystem
{
    /// <summary>
    /// Class to hold Line and Position data for dialogues.
    /// </summary>
    [System.Serializable]
    public struct DialogueLine
    {
        [Tooltip("The Actor mood for this line used in the UI.")]
        public ActorMood Mood;
        [Tooltip("The localized line used for this dialogue.")]
        public LocalizedString LocalizedLine;

        [Space]
        [SerializeField, Tooltip("The choices for this line ending.")]
        public Dictionary<string, LocalizedString> Choices;

        public readonly bool HasChoices() => Choices.Count > 0;
    }
}