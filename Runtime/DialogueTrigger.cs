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
        public Dialogue CurrentDialogue { get; set; }

        public bool IsInteracting { get; private set; }

        public bool CanStartDialogue() => DialogueManager.CanPlay() && !IsInteracting;

        public bool TryStartDialogue()
        {
            var canStartDialogue = CanStartDialogue();
            if (canStartDialogue) StartDialogue();
            return canStartDialogue;
        }

        public async void StartDialogue()
        {
            IsInteracting = true;
            await DialogueManager.PlayAsync(CurrentDialogue);
            IsInteracting = false;
        }
    }
}