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
        [field: SerializeField, Tooltip("The local Collider component.")]
        public Collider Collider { get; private set; }
        [field: SerializeField, Tooltip("The current dialogue to play.")]
        public Dialogue CurrentDialogue { get; private set; }

        [Space]
        [SerializeField] private GameObject interactionInput;

        public bool IsInteracting { get; private set; }

        private void Reset() => Collider = GetComponent<Collider>();

        public bool CanCollide() => enabled;
        public bool CanInteract() => DialogueManager.CanPlay() && !IsInteracting;

        public async void Interact()
        {
            if (CurrentDialogue == null) return;

            ChangeAvailability(false);

            IsInteracting = true;
            await DialogueManager.PlayAsync(CurrentDialogue);
            IsInteracting = false;

            ChangeAvailability(true);
        }

        public void ChangeAvailability(bool isAvailable)
        {
            if (interactionInput) interactionInput.SetActive(isAvailable);
        }

        public void EnterCollision(Transform interactor) => ChangeAvailability(true);
        public void ExitCollision(Transform interactor) => ChangeAvailability(false);

        public void ShowInteractionFail()
        {
            //TODO play some SEF
            Debug.LogWarning("Cannot Interact");
        }
    }
}