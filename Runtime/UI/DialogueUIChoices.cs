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

            foreach (var (choiceKey, choiceLocalization) in line.Choices)
            {
                var button = list.Add<ActionButton>();

                button.Label.UpdateLocalization(choiceLocalization);
                button.OnClicked += () => board.ConfirmChoice(choiceKey);
            }

            handler.BindElements(list.itemContainer);
            if (hasChoices) list.Select(0);

            SetActive(hasChoices);
        }
    }
}