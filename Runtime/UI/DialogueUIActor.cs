using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace OneM.DialogueSystem
{
    [DisallowMultipleComponent]
    public sealed class DialogueUIActor : MonoBehaviour
    {
        [SerializeField] private TMP_Text textName;
        [SerializeField] private LocalizeStringEvent localizedName;

        public void Load(ActorData actor)
        {
            SetNameActive(true);

            if (actor.HasLocalizedName()) SetName(actor.LocalizedName);
            else textName.text = actor.GetName();
        }

        public void SetName(LocalizedString name) => localizedName.StringReference = name;
        public void SetNameActive(bool isEnabled) => localizedName.gameObject.SetActive(isEnabled);

        public void Dispose()
        {
            SetName(null);
            SetNameActive(false);
            textName.text = string.Empty;
        }
    }
}
