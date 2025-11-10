using UnityEngine;

namespace OneM.DialogueSystem
{
    [DisallowMultipleComponent]
    public sealed class DialogueManager : MonoBehaviour
    {
        [SerializeField] private DialogueUIBoard board;

        public static DialogueManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            board.Disable();
        }

        private void OnDestroy() => Instance = null;

        public async Awaitable PlayAsync(Dialogue dialogue)
        {
            await board.EnableAsync();

        }
    }
}