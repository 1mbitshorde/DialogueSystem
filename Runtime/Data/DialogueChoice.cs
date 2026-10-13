using OneM.UISystem;
using UnityEngine;

namespace OneM.DialogueSystem
{
    /// <summary>
    /// A class representing a dialogue choice with localization support and availability status.
    /// </summary>
    [System.Serializable]
    public struct DialogueChoice
    {
        [Tooltip("The fallback text to be used when localization is not found.")]
        public string Fallback;
        [Tooltip("The Table Id where the entry is located.")]
        public string TableId;
        [Tooltip("The Entry Id to be localized.")]
        public string EntryId;
        [Tooltip("Whether the choice will be available for selection.")]
        public bool IsAvailable;

        public DialogueChoice(string fallback, bool isAvailable = true, string tableId = "", string entryId = "")
        {
            TableId = tableId;
            EntryId = entryId;
            Fallback = fallback;
            IsAvailable = isAvailable;
        }

        public readonly LocalizedString GetLocalizedString() => new(TableId, EntryId, Fallback);
    }
}