using System;
using UnityEngine;

namespace OneM.DialogueSystem
{
    /// <summary>
    /// Triggers a Dialogue when interacted with.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class DialogueTrigger : MonoBehaviour
    {
        [field: SerializeField, Tooltip("The current dialogue to play.")]
        public DialogueData CurrentDialogue { get; set; }

        public bool IsInteracting { get; private set; }

        public event Action<Transform> OnDialogueStarted;
        public event Action<Transform> OnDialogueFinished;

        public bool CanStartDialogue() => DialogueManager.CanPlay() && !IsInteracting;

        public async void StartDialogue(Transform interactor)
        {
            if (!CanStartDialogue()) return;

            IsInteracting = true;
            OnDialogueStarted?.Invoke(interactor);

            await DialogueManager.PlayAsync(CurrentDialogue);

            IsInteracting = false;
            OnDialogueFinished?.Invoke(interactor);
        }
    }
}