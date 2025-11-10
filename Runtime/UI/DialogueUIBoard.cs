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

        public async Awaitable EnableAsync()
        {
            gameObject.SetActive(true);
            await Awaitable.WaitForSecondsAsync(initialAnimationTime);
        }

        public void Disable() => gameObject.SetActive(false);
    }
}
