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
            gameObject.SetActive(true);
            await Awaitable.WaitForSecondsAsync(initialAnimationTime);

            foreach (var line in dialogue.Lines)
            {
                /*var actor = actors[line.Position];
                
                actor.SetPortrait(line.Actor.Portrait);
                actor.SetName(line.Actor.LocalizedName);*/

                this.line.StringReference = line.LocalizedLine;
                await Awaitable.WaitForSecondsAsync(1f);
            }

            Disable();
        }

        public void Disable() => gameObject.SetActive(false);
    }
}
