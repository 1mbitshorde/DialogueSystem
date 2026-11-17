using OneM.AnimationSystem;
using UnityEngine;

namespace OneM.DialogueSystem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(DynamicAnimator))]
    public sealed class DialogueAnimationHandler : MonoBehaviour
    {
        [SerializeField] private ActorData actor;
        [SerializeField] private DynamicAnimator animator;

        private bool isPlaying;

        private void Reset() => animator = GetComponent<DynamicAnimator>();
        private void OnEnable() => SubscribeEvents();
        private void OnDisable() => UnsubscribeEvents();

        private void SubscribeEvents()
        {
            DialogueManager.OnAnimationTriggered += HandleAnimationTriggered;
            DialogueManager.OnDialogueFinished += HandleDialogueFinished;
        }

        private void UnsubscribeEvents()
        {
            DialogueManager.OnAnimationTriggered -= HandleAnimationTriggered;
            DialogueManager.OnDialogueFinished -= HandleDialogueFinished;
        }

        private void HandleAnimationTriggered(ActorData actor, DynamicAnimationData animation)
        {
            if (this.actor != actor) return;
            animator.LoadAndPlay(animation);
            isPlaying = true;
        }

        private void HandleDialogueFinished()
        {
            if (isPlaying) animator.ClearAnimations();
            isPlaying = false;
        }
    }
}