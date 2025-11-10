using UnityEngine;

namespace OneM.DialogueSystem
{
    /// <summary>
    /// Class to hold a complete Dialogue with multiple lines.
    /// </summary>
    [CreateAssetMenu(fileName = "Dialogue", menuName = "OneM/Dialogue System/New Dialogue", order = 110)]
    public sealed class Dialogue : ScriptableObject
    {
        [field: SerializeField] public DialogueLine[] Lines { get; private set; }
    }
}