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
        /// <remarks>
        /// The string parameter represents the key of the choice 
        /// in the <see cref="DialogueLine.Choices"/> dictionary.
        /// </remarks>
        public event Action<string> OnChoiceConfirmed;

        /// <summary>
        /// Event triggered when a choice is canceled by the Player, by pressing the Esc or back button while selecting the choice.
        /// </summary>
        /// <remarks>
        /// The string parameter represents the key of the choice 
        /// in the <see cref="DialogueLine.Choices"/> dictionary.
        /// </remarks>
        public event Action<string> OnChoiceCanceled;

        public Sprite GetPortrait(ActorMood mood) => Actor.Portraits[mood];

        internal void ConfirmChoice(string key) => OnChoiceConfirmed?.Invoke(key);
        internal void CancelChoice(string key) => OnChoiceCanceled?.Invoke(key);
    }
}