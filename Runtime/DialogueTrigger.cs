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
        public Dialogue CurrentDialogue { get; private set; }

        public bool IsInteracting { get; private set; }

        public bool CanStartDialogue() => DialogueManager.CanPlay() && !IsInteracting;

        public async void StartDialogue()
        {
            if (!CanStartDialogue()) return;

            IsInteracting = true;
            await DialogueManager.PlayAsync(CurrentDialogue);
            IsInteracting = false;
        }
    }
}