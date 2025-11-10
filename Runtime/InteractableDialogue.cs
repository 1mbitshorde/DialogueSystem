using UnityEngine;
using OneM.InteractableSystem;

namespace OneM.DialogueSystem
{
    /// <summary>
    /// Component used to start dialogue when interacted. <see cref="CurrentDialogue"/> must be set.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class InteractableDialogue : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public Dialogue CurrentDialogue { get; private set; }
        [SerializeField] private GameObject interactionInput;

        public bool IsInteracting { get; private set; }

        public bool CanInteract() => DialogueManager.CanPlay() && !IsInteracting;

        public async void Interact()
        {
            if (CurrentDialogue == null) return;

            ChangeAvailability(false);

            IsInteracting = true;
            await DialogueManager.PlayAsync(CurrentDialogue);
            IsInteracting = false;
        }

        public void ChangeAvailability(bool isAvailable) => interactionInput.SetActive(isAvailable);

        public void ShowInteractionFail()
        {
            //TODO play some SEF
            Debug.LogWarning("Cannot Interact");
        }
    }
}