using UnityEngine;

namespace OneM.DialogueSystem
{
    /// <summary>
    /// Class to hold Actor and Position data for dialogues.
    /// </summary>
    [System.Serializable]
    public struct DialogueActor
    {
        [field: SerializeField, Tooltip("The Actor used on this dialogue line.")]
        public Actor Actor { get; private set; }
        [field: SerializeField, Tooltip("The position where the Actor will be in the UI.")]
        public ActorPosition Position { get; private set; }
    }
}