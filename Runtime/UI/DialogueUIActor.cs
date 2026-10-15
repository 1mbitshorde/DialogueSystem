using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace OneM.DialogueSystem
{
    [DisallowMultipleComponent]
    public sealed class DialogueUIActor : MonoBehaviour
    {
        [SerializeField] private TMP_Text textName;
        [SerializeField] private LocalizeStringEvent localizedName;
        [SerializeField] private Image portrait;

        public void Load(Actor actor)
        {
            SetName(actor.LocalizedName);
            SetNameActive(true);
            SetPortraitActive(false);
        }

        public void SetPortraitActive(bool isEnabled) => portrait.gameObject.SetActive(isEnabled);
        public void SetName(LocalizedString name) => localizedName.StringReference = name;
        public void SetNameActive(bool isEnabled) => localizedName.gameObject.SetActive(isEnabled);

        public void SetPortrait(Sprite image)
        {
            portrait.sprite = image;
            portrait.enabled = image != null;
        }

        public void Dispose()
        {
            SetName(null);
            SetNameActive(false);
            SetPortrait(null);
            SetPortraitActive(false);
            textName.text = string.Empty;
        }
    }
}
