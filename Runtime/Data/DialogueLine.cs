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
        [field: SerializeField, Tooltip("The position where the line will be in the UI.")]
        public ActorPosition Position { get; private set; }
        [field: SerializeField, Tooltip("The localized line used for this dialogue.")]
        public LocalizedString LocalizedLine { get; private set; }
    }
}