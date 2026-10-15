using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

namespace OneM.DialogueSystem
{
    /// <summary>
    /// Class to hold Dialogue Actor data.
    /// </summary>
    [CreateAssetMenu(fileName = "Actor", menuName = "OneM/Dialogue System/New Actor")]
    public class Actor : ScriptableObject
    {
        [field: SerializeField, Tooltip("The loacalized name used to show the actor name during dialogues.")]
        public LocalizedString LocalizedName { get; private set; }
        [field: SerializeField, Tooltip("The portraits used to show the actor mood during dialogues.")]
        public Dictionary<ActorMood, Sprite> Portraits { get; private set; }

        public bool HasLocalizedName() => !LocalizedName.IsEmpty;
        public virtual string GetName() => string.Empty;
    }
}