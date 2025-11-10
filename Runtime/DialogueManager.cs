using System;
using UnityEngine;

namespace OneM.DialogueSystem
{
    [DisallowMultipleComponent]
    public sealed class DialogueManager : MonoBehaviour
    {
        [SerializeField] private DialogueUIBoard board;

        public static DialogueManager Instance { get; private set; }

        public static bool IsPlaying { get; private set; }

        public static event Action OnDialogueStarted;
        public static event Action OnDialogueStopped;

        private void Awake()
        {
            Instance = this;
            board.Disable();
        }

        private void OnDestroy() => Dispose();

        public static bool CanPlay() => Instance != null && !IsPlaying;

        public static async Awaitable PlayAsync(Dialogue dialogue)
        {
            OnDialogueStarted?.Invoke();
            await Instance.board.PlayAsync(dialogue);
            OnDialogueStopped?.Invoke();
        }

        private static void Dispose()
        {
            Instance = null;
            IsPlaying = false;
        }
    }
}