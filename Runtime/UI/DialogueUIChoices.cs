using OneM.UISystem;
using UnityEngine;

namespace OneM.DialogueSystem
{
    [DisallowMultipleComponent]
    public sealed class DialogueUIChoices : MonoBehaviour
    {
        [SerializeField] private ListController list;

        private AudioHandler handler;
        private DialogueUIBoard board;

        internal void Initialize(DialogueUIBoard board)
        {
            this.board = board;
            handler = GetComponentInParent<AudioHandler>();
        }

        public void Show() => SetActive(true);
        public void Hide() => SetActive(false);
        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void Show(DialogueLine line)
        {
            handler.UnbindElements();

            var hasChoices = line.HasChoices();
            list.Clear();

            foreach (var choice in line.Choices)
            {
                var button = list.Add<ActionButton>();

                button.interactable = choice.IsAvailable;
                button.Label.UpdateLocalization(choice.GetLocalizedString());
                button.OnClicked += () => board.ConfirmChoice(choice);
                button.OnCanceled += () => board.CancelChoice(choice);
            }

            handler.BindElements(list.itemContainer);

            var availableChoiceIndex = line.GetFirstAvailableChoiceIndex();
            if (hasChoices && availableChoiceIndex >= 0) list.Select(availableChoiceIndex);

            SetActive(hasChoices);
        }
    }
}