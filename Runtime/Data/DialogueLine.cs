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
        [field: SerializeField, Tooltip("The Actor mood for this line used in the UI.")]
        public ActorMood Mood { get; private set; }
        [field: SerializeField, Tooltip("The localized line used for this dialogue.")]
        public LocalizedString LocalizedLine { get; private set; }
    }
}