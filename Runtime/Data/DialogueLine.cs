using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

namespace OneM.DialogueSystem
{
    /// <summary>
    /// Class to hold Line and Position data for dialogues.
    /// </summary>
    [System.Serializable]
    public struct DialogueLine
    {
        [Tooltip("The Actor for this line.")]
        public ActorData Actor;
        [Tooltip("[Optional] The Animation to play when entering in this line. Use LayerName.AnimationName if animation is inside layers.")]
        public string AnimationName;
        [Tooltip("The localized line used for this dialogue.")]
        public LocalizedString LocalizedLine;

        [Space]
        [SerializeField, Tooltip("The choices for this line ending.")]
        public List<DialogueChoice> Choices;

        public event Action<string> OnAnimationTriggered;

        public readonly bool HasChoices() => Choices.Count > 0;
        public readonly bool HasAnimationName() => !string.IsNullOrEmpty(AnimationName);

        public void SetChoices(params string[] choices) => SetChoices(new List<string>(choices));

        public void SetChoices(List<string> choices)
        {
            Choices = new List<DialogueChoice>();
            foreach (var choice in choices)
            {
                Choices.Add(new DialogueChoice(choice));
            }
        }

        public readonly int GetFirstAvailableChoiceIndex()
        {
            for (int i = 0; i < Choices.Count; i++)
            {
                if (Choices[i].IsAvailable)
                {
                    return i;
                }
            }
            return -1;
        }

        internal readonly void TryTriggerAnimation()
        {
            if (HasAnimationName()) OnAnimationTriggered?.Invoke(AnimationName);
        }
    }
}