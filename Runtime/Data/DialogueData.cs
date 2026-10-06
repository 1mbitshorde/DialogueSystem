using UnityEngine;

namespace OneM.DialogueSystem
{
    /// <summary>
    /// Data class to hold a complete Dialogue with multiple lines.
    /// </summary>
    [CreateAssetMenu(fileName = "DialogueData", menuName = "OneM/Dialogue System/New Dialogue Data")]
    public sealed class DialogueData : ScriptableObject
    {
        [field: SerializeField, Tooltip("All Actors present in this dialogue.")]
        public Actor Actor { get; private set; }

        [field: SerializeField, Tooltip("The lines used on this dialogue.")]
        public DialogueLine[] Lines { get; private set; }

        public Sprite GetPortrait(ActorMood mood) => Actor.Portraits[mood];
    }
}