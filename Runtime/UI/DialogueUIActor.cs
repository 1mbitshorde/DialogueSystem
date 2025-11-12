using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

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
            SetPortrait(actor.Portrait);
            SetName(actor.LocalizedName);
            SetNameActive(false);
        }

        public void SetPortrait(Sprite image) => portrait.sprite = image;
        public void SetName(LocalizedString name) => localizedName.StringReference = name;
        public void SetNameActive(bool isEnabled) => localizedName.gameObject.SetActive(isEnabled);

        public void Dispose()
        {
            SetName(null);
            SetPortrait(null);
            textName.text = string.Empty;
        }
    }
}
