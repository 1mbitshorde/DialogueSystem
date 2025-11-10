using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Components;

namespace OneM.DialogueSystem
{
    [DisallowMultipleComponent]
    public sealed class DialogueUIActor : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent localizedName;
        [SerializeField] private Image portrait;
    }
}
