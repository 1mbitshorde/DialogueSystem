using UnityEngine;

namespace OneM.DialogueSystem
{
    [DisallowMultipleComponent]
    public sealed class DialogueManager : MonoBehaviour
    {
        [SerializeField] private DialogueUIBoard board;

        public static DialogueManager Instance { get; private set; }

        public static bool IsPlaying { get; private set; }

        private void Awake()
        {
            Instance = this;
            board.Disable();
        }

        private void OnDestroy() => Dispose();

        public static bool CanPlay() => Instance != null && !IsPlaying;

        public static async Awaitable PlayAsync(Dialogue dialogue)
        {
            await Instance.board.EnableAsync();

        }

        private static void Dispose()
        {
            Instance = null;
            IsPlaying = false;
        }
    }
}