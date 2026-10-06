using OneM.AwaitableSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace OneM.DialogueSystem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class DialogueUIBoard : MonoBehaviour
    {
        [SerializeField] private DialogueUIActor actor;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject marker;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private DialogueUIChoices choices;

        [Header("LINES")]
        [SerializeField] private TMP_Text textLine;
        [SerializeField] private LocalizeStringEvent localizedLine;

        [Header("TIMERS")]
        [SerializeField, Tooltip("The fade In/Out animation duration (in seconds).")]
        private float fadeDuration = 0.2f;
        [SerializeField, Tooltip("The initial time to wait (in seconds) before start to writing.")]
        private float initialWaitingTime = 1f;
        [SerializeField, Tooltip("The time (in seconds) to wait between each letter.")]
        private float typeWriteTime = 0.02f;

        public bool HasChoices { get; private set; }
        public bool IsTypeWriting { get; private set; }
        public bool IsNextLineAvailable { get; private set; }

        private int lastAdvanceFrame;

        private void Reset() => canvasGroup = GetComponent<CanvasGroup>();
        private void Awake() => choices.Initialize(this);

        public async Awaitable PlayAsync(DialogueData dialogue)
        {
            actor.Load(dialogue.Actor);
            SetMarkerEnable(false);
            SetCanvasGroupAlpha(0f);

            choices.Hide();
            gameObject.SetActive(true);

            await AwaitableUtility.WaitForSecondsRealtimeAsync(initialWaitingTime);
            await FadeInAsync();

            actor.SetPortraitActive(true);

            foreach (var line in dialogue.Lines)
            {
                choices.Hide();

                IsNextLineAvailable = false;
                HasChoices = line.Choices.Length > 0;
                localizedLine.StringReference = line.LocalizedLine;

                actor.SetPortrait(dialogue.GetPortrait(line.Mood));

                await WaitUntilLocalizedLineIsFullyLoadedAsync();
                await PlayTypeWriteLineAnimationAsync();

                if (HasChoices)
                {
                    await AwaitableUtility.WaitForSecondsRealtimeAsync(0.2F);
                    choices.Show(line.Choices);
                }

                await WaitUntilNextLineIsAvailableAsync();
            }

            await FadeOutAsync();
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

        internal void ConfirmChoice(int id)
        {
            DialogueManager.ConfirmChoice(id);
            HasChoices = false;
            Advance();
        }

        private bool CanAdvance()
        {
            if (HasChoices) return false;

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
            SetMarkerEnable(!HasChoices);
        }

        private async Awaitable WaitUntilNextLineIsAvailableAsync() =>
            await AwaitableUtility.WaitUntilAsync(() => IsNextLineAvailable);

        private async Awaitable FadeInAsync() => await FadeAsync(startAlpha: 0F, finalAlpha: 1F);
        private async Awaitable FadeOutAsync() => await FadeAsync(startAlpha: 1F, finalAlpha: 0F);
        private async Awaitable FadeAsync(float startAlpha, float finalAlpha) =>
            await AwaitableUtility.LerpAsync(startAlpha, finalAlpha, fadeDuration, SetCanvasGroupAlpha);

        private void CompleteTypeWrite() => textLine.maxVisibleCharacters = textLine.text.Length;
        private void SetMarkerEnable(bool isEnable) => marker.SetActive(isEnable);
        private void SetCanvasGroupAlpha(float alpha) => canvasGroup.alpha = alpha;
    }
}