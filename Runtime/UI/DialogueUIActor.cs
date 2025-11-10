using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace OneM.DialogueSystem
{
    [DisallowMultipleComponent]
    public sealed class DialogueUIActor : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent localizedName;
        [SerializeField] private Image portrait;

        public void SetPortrait(Sprite image) => portrait.sprite = image;
        public void SetName(LocalizedString name) => localizedName.StringReference = name;
    }
}
