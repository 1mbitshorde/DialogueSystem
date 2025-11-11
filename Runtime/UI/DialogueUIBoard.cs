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

            foreach (var line in dialogue.LocalizedLine)
            {
                this.line.StringReference = line;
                await Awaitable.WaitForSecondsAsync(2f);
            }

            Disable();
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            DisposeActors();
        }

        private void LoadActors(DialogueActorLine[] actorLines)
        {
            foreach (var line in actorLines)
            {
                var actor = actors[line.Position];
                actor.SetPortrait(line.Actor.Portrait);
                actor.SetName(line.Actor.LocalizedName);
            }
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
