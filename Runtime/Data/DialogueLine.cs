using UnityEngine;
using UnityEngine.Localization;

namespace OneM.DialogueSystem
{
    /// <summary>
    /// Class to hold a single Dialogue Line.
    /// </summary>
    [System.Serializable]
    public struct DialogueLine
    {
        [field: SerializeField, Tooltip("The Actor used on this dialogue line.")]
        public Actor Actor { get; private set; }
        [field: SerializeField, Tooltip("The localized line used on this dialogue.")]
        public LocalizedString LocalizedLine { get; private set; }
    }
}