using System;
using UnityEngine;

namespace OneM.DialogueSystem
{
    /// <summary>
    /// Data class to hold a complete Dialogue with multiple lines.
    /// </summary>
    [CreateAssetMenu(fileName = "DialogueData", menuName = "OneM/Dialogue System/New Dialogue Data")]
    public sealed class DialogueData : ScriptableObject
    {
        [Tooltip("All Actors present in this dialogue.")]
        public Actor Actor;
        [Tooltip("The lines used on this dialogue.")]
        public DialogueLine[] Lines;

        /// <summary>
        /// Event triggered when a choice is confirmed by the Player.
        /// </summary>
        public event Action<DialogueChoice> OnChoiceConfirmed;

        /// <summary>
        /// Event triggered when a choice is canceled by the Player, by pressing the Esc or back button while selecting the choice.
        /// </summary>
        /// <remarks>
        public event Action<DialogueChoice> OnChoiceCanceled;

        public Sprite GetPortrait(ActorMood mood)
        {
            var hasPortrait = Actor.Portraits.TryGetValue(mood, out var portrait);
            return hasPortrait ? portrait : null;
        }

        internal void ConfirmChoice(DialogueChoice choice) => OnChoiceConfirmed?.Invoke(choice);
        internal void CancelChoice(DialogueChoice choice) => OnChoiceCanceled?.Invoke(choice);
    }
}