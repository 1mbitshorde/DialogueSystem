using UnityEngine;
using UnityEngine.Localization;
using OneM.SerializedDictionaries;

namespace OneM.DialogueSystem
{
    /// <summary>
    /// Class to hold Dialogue Actor data.
    /// </summary>
    [CreateAssetMenu(fileName = "Actor", menuName = "OneM/Dialogue System/New Actor")]
    public sealed class Actor : ScriptableObject
    {
        [field: SerializeField, Tooltip("The loacalized name used to show the actor name during dialogues.")]
        public LocalizedString LocalizedName { get; private set; }
        [field: SerializeField, Tooltip("The portraits used to show the actor mood during dialogues.")]
        public SerializedDictionary<ActorMood, Sprite> Portraits { get; private set; }
    }
}