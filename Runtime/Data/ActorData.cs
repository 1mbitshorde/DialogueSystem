using UnityEngine;
using UnityEngine.Localization;

namespace OneM.DialogueSystem
{
    /// <summary>
    /// Class to hold Dialogue Actor data.
    /// </summary>
    [CreateAssetMenu(fileName = "ActorData", menuName = "OneM/Dialogue System/New Actor Data")]
    public class ActorData : ScriptableObject
    {
        [field: SerializeField, Tooltip("The loacalized name used to show the actor name during dialogues.")]
        public LocalizedString LocalizedName { get; private set; }

        public bool HasLocalizedName() => !LocalizedName.IsEmpty;
        public virtual string GetName() => string.Empty;
    }
}