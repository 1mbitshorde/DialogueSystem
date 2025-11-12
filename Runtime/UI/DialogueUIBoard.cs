using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using ActionCode.AwaitableSystem;
using ActionCode.SerializedDictionaries;

namespace OneM.DialogueSystem
{
    [DisallowMultipleComponent]
    public sealed class DialogueUIBoard : MonoBehaviour
    {
        [SerializeField] private TMP_Text textLine;
        [SerializeField] private LocalizeStringEvent localizedLine;
        [SerializeField] private GameObject completionLineMarker;

        [Header("TIMERS")]
        [SerializeField, Tooltip("The initial time to wait (in seconds) before start to writing.")]
        private float initialWaitingTime = 1f;
        [SerializeField, Tooltip("The time (in seconds) to wait between each letter.")]
        private float typeWriteTime = 0.02f;

        [Space]
        [SerializeField, Tooltip("The actors instances.")]
        private SerializedDictionary<ActorPosition, DialogueUIActor> actors;

        public bool IsTypeWriting { get; private set; }
        public bool IsNextLineAvailable { get; private set; }

        public async Awaitable PlayAsync(Dialogue dialogue)
        {
            SetCompletionLineMarkerEnable(false);
            LoadActors(dialogue.Actors);

            gameObject.SetActive(true);
            await AwaitableUtility.WaitForSecondsRealtimeAsync(initialWaitingTime);

            foreach (var line in dialogue.Lines)
            {
                IsNextLineAvailable = false;
                SetActorNameEnable(line.Position);
                localizedLine.StringReference = line.LocalizedLine;

                // Waits the localized string to fully load.
                await localizedLine.StringReference.GetLocalizedStringAsync().Task;
                await TypeWriteAsync();
                await AwaitableUtility.WaitUntilAsync(() => IsNextLineAvailable);
            }

            Disable();
        }

        /// <summary>
        /// Completes the current line or advance to the next one.
        /// </summary>
        public void Advance()
        {
            if (IsTypeWriting) CompleteTypeWrite();
            else AdvanceToNextLine();
        }

        public void AdvanceToNextLine() => IsNextLineAvailable = true;

        public void Disable()
        {
            gameObject.SetActive(false);
            SetCompletionLineMarkerEnable(false);

            textLine.text = string.Empty;
            localizedLine.StringReference = null;

            DisposeActors();
        }

        private async Awaitable TypeWriteAsync()
        {
            var textLength = textLine.text.Length;
            textLine.maxVisibleCharacters = 0;

            IsTypeWriting = true;
            SetCompletionLineMarkerEnable(false);

            while (textLine.maxVisibleCharacters < textLength)
            {
                textLine.maxVisibleCharacters++;
                await Awaitable.WaitForSecondsAsync(typeWriteTime);
            }

            IsTypeWriting = false;
            SetCompletionLineMarkerEnable(true);
        }

        private void CompleteTypeWrite() => textLine.maxVisibleCharacters = textLine.text.Length;

        private void LoadActors(DialogueActor[] dialogueActors)
        {
            foreach (var dialogue in dialogueActors)
            {
                var actor = actors[dialogue.Position];
                actor.Load(dialogue.Actor);
            }
        }

        private void SetActorNameEnable(ActorPosition position)
        {
            foreach (var actor in actors.Values)
            {
                actor.SetNameActive(false);
            }
            actors[position].SetNameActive(true);
        }

        private void SetCompletionLineMarkerEnable(bool isEnable) =>
            completionLineMarker.SetActive(isEnable);

        private void DisposeActors()
        {
            foreach (var actor in actors.Values)
            {
                actor.Dispose();
            }
        }
    }
}