using UnityEngine;
using UnityEngine.Localization;

namespace OneM.DialogueSystem
{
    /// <summary>
    /// Class to hold a complete Dialogue with multiple lines.
    /// </summary>
    [CreateAssetMenu(fileName = "Dialogue", menuName = "OneM/Dialogue System/New Dialogue", order = 110)]
    public sealed class Dialogue : ScriptableObject
    {
        [field: SerializeField, Tooltip("All Actors present in this dialogue.")]
        public DialogueActorLine[] Actors { get; private set; }

        [field: SerializeField, Tooltip("The localized lines used on this dialogue.")]
        public LocalizedString[] LocalizedLine { get; private set; }
    }
}