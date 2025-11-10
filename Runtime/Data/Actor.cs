using UnityEngine;
using UnityEngine.Localization;

namespace OneM.DialogueSystem
{
    /// <summary>
    /// Class to hold Dialogue Actor data.
    /// </summary>
    [CreateAssetMenu(fileName = "Actor", menuName = "OneM/Dialogue System/New Actor", order = 110)]
    public sealed class Actor : ScriptableObject
    {
        [field: SerializeField, Tooltip("The portrait image used to show the actor during dialogues.")]
        public Sprite Portrait { get; private set; }
        [field: SerializeField, Tooltip("The loacalized name used to show the actor name during dialogues.")]
        public LocalizedString LocalizedName { get; private set; }
    }
}