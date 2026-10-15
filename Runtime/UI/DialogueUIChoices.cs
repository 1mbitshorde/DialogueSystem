using OneM.AnimationSystem;
using OneM.UISystem;
using UnityEngine;

namespace OneM.DialogueSystem
{
    [DisallowMultipleComponent]
    public sealed class DialogueUIChoices : MonoBehaviour
    {
        [SerializeField] private ListController list;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("ANIMATIONS")]
        [SerializeField] private AbstractAnimation showAnimation;
        [SerializeField] private AbstractAnimation hideAnimation;

        private AudioHandler handler;
        private DialogueUIBoard board;

        internal void Initialize(DialogueUIBoard board)
        {
            this.board = board;
            handler = GetComponentInParent<AudioHandler>();
        }

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetVisibility(bool isVisible)
        {
            canvasGroup.alpha = isVisible ? 1f : 0f;
            canvasGroup.interactable = isVisible;
        }

        public async Awaitable ShowAsync(DialogueLine line)
        {
            SetVisibility(false);
            handler.UnbindElements();
            //await AwaitableUtility.WaitForSecondsRealtimeAsync(0.2F);

            list.Clear();

            foreach (var choice in line.Choices)
            {
                var button = list.Add<ActionButton>();

                button.interactable = choice.IsAvailable;
                button.Label.UpdateLocalization(choice.GetLocalizedString());
                button.OnClicked += () => board.ConfirmChoice(choice);
                button.OnCanceled += () => board.CancelChoice(choice);
            }

            await showAnimation.PlayAsync();
            SetVisibility(true);

            handler.BindElements(list.itemContainer);
            var availableChoiceIndex = line.GetFirstAvailableChoiceIndex();
            if (availableChoiceIndex >= 0) list.Select(availableChoiceIndex);
        }
    }
}