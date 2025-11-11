using UnityEngine;
using UnityEngine.Localization.Components;
using ActionCode.SerializedDictionaries;

namespace OneM.DialogueSystem
{
    [DisallowMultipleComponent]
    public sealed class DialogueUIBoard : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent line;
        [SerializeField] private float initialAnimationTime = 1f;

        [Space]
        [SerializeField] private SerializedDictionary<ActorPosition, DialogueUIActor> actors;

        public async Awaitable PlayAsync(Dialogue dialogue)
        {
            LoadActors(dialogue.Actors);

            gameObject.SetActive(true);
            await Awaitable.WaitForSecondsAsync(initialAnimationTime);

            foreach (var line in dialogue.Lines)
            {
                EnableActorName(line.Position);
                this.line.StringReference = line.LocalizedLine;

                await Awaitable.WaitForSecondsAsync(2f);
            }

            Disable();
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            line.StringReference = null;
            DisposeActors();
        }

        private void LoadActors(DialogueActor[] dialogueActors)
        {
            foreach (var dialogue in dialogueActors)
            {
                var actor = actors[dialogue.Position];

                actor.SetPortrait(dialogue.Actor.Portrait);
                actor.SetName(dialogue.Actor.LocalizedName);
                actor.SetNameActive(false);
            }
        }

        private void EnableActorName(ActorPosition position)
        {
            foreach (var actor in actors.Values)
            {
                actor.SetNameActive(false);
            }
            actors[position].SetNameActive(true);
        }

        private void DisposeActors()
        {
            foreach (var actor in actors.Values)
            {
                actor.Dispose();
            }
        }
    }
}
