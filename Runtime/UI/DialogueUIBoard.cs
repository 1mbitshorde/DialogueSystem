using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using ActionCode.AwaitableSystem;

namespace OneM.DialogueSystem
{
    [DisallowMultipleComponent]
    public sealed class DialogueUIBoard : MonoBehaviour
    {
        [SerializeField] private DialogueUIActor actor;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject marker;

        [Header("LINES")]
        [SerializeField] private TMP_Text textLine;
        [SerializeField] private LocalizeStringEvent localizedLine;

        [Header("TIMERS")]
        [SerializeField, Tooltip("The initial time to wait (in seconds) before start to writing.")]
        private float initialWaitingTime = 1f;
        [SerializeField, Tooltip("The time (in seconds) to wait between each letter.")]
        private float typeWriteTime = 0.02f;

        public bool IsTypeWriting { get; private set; }
        public bool IsNextLineAvailable { get; private set; }

        private int lastAdvanceFrame;

        public async Awaitable PlayAsync(Dialogue dialogue)
        {
            actor.Load(dialogue.Actor);
            SetMarkerEnable(false);

            gameObject.SetActive(true);
            await AwaitableUtility.WaitForSecondsRealtimeAsync(initialWaitingTime);

            foreach (var line in dialogue.Lines)
            {
                IsNextLineAvailable = false;
                localizedLine.StringReference = line.LocalizedLine;

                await WaitUntilLocalizedLineIsFullyLoadedAsync();
                await PlayTypeWriteLineAnimationAsync();
                await WaitUntilNextLineIsAvailableAsync();
            }

            Disable();
        }

        /// <summary>
        /// Completes the current line or advance to the next one.
        /// </summary>
        public void Advance()
        {
            if (!CanAdvance()) return;

            if (IsTypeWriting) CompleteTypeWrite();
            else AdvanceToNextLine();

            lastAdvanceFrame = Time.frameCount;
        }

        public void AdvanceToNextLine() => IsNextLineAvailable = true;

        public void Disable()
        {
            actor.Dispose();
            gameObject.SetActive(false);
            SetMarkerEnable(false);

            textLine.text = string.Empty;
            localizedLine.StringReference = null;
        }

        private bool CanAdvance()
        {
            var framesSinceLastAdvance = Time.frameCount - lastAdvanceFrame;
            return framesSinceLastAdvance > 10;
        }

        private async Awaitable WaitUntilLocalizedLineIsFullyLoadedAsync() =>
            await localizedLine.StringReference.GetLocalizedStringAsync().Task;

        private async Awaitable PlayTypeWriteLineAnimationAsync()
        {
            var textLength = textLine.text.Length;
            textLine.maxVisibleCharacters = 0;

            audioSource.Play();
            IsTypeWriting = true;
            SetMarkerEnable(false);

            while (textLine.maxVisibleCharacters < textLength)
            {
                textLine.maxVisibleCharacters++;
                await AwaitableUtility.WaitForSecondsRealtimeAsync(typeWriteTime);
            }

            audioSource.Stop();
            IsTypeWriting = false;
            SetMarkerEnable(true);
        }

        private async Awaitable WaitUntilNextLineIsAvailableAsync() =>
            await AwaitableUtility.WaitUntilAsync(() => IsNextLineAvailable);

        private void CompleteTypeWrite() => textLine.maxVisibleCharacters = textLine.text.Length;
        private void SetMarkerEnable(bool isEnable) => marker.SetActive(isEnable);
    }
}