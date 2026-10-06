# Dialogue System

Localized Dialogue System for Player and Npcs.

Each dialogue is played between one or multiple Actors. The Actor name and dialogue lines should be localized.

## How To Use

### Creating Dialogue Assets

Create an Actor Scriptable Object (SO) asset by the creation menu, **OneM > Dialogue System > New Actor** and set its fields.

Create a Dialogue Data SO asset by the creation menu, **OneM > Dialogue System > New Dialogue Data** and set the its fields, including the Actor for this dialogue.

### Playing the Dialogue

Add the prefab [P_DialogueManager](/Prefabs/P_DialogueManager.prefab) into your Scene and call `DialogueManager.PlayAsync()` function from any other script, passing the Dialogue Data you have created.

You can create your own P_DialogueManager prefab. Check the one provided in this package to know how.

Finally, you can attach the component [DialogueTrigger](/Runtime/DialogueTrigger.cs) into any GameObject and starts a dialogue by calling `StartDialogue()` function.

### Dialogue Choices

Every line into the Dialogue Data has a Choice Dictionary. Use this field to add a Key and a Localized String for every choice.

In your code, you can handle the selected choice by listening to the `OnChoiceConfirmed` event:

```csharp
using UnityEngine;
using OneM.DialogueSystem;

namespace YourNamespace
{
    public sealed class DialogueChoiceHandler : MonoBehaviour
    {
        [SerializeField] private DialogueData dialogue;

        private void OnEnable() => dialogue.OnChoiceConfirmed += HandleChoiceConfirmed;
        private void OnDisable() => dialogue.OnChoiceConfirmed -= HandleChoiceConfirmed;
        private void HandleChoiceConfirmed(string key) => print($"Selected Choice: {key}")
    }
}
```

## Installation

### Using the Git URL

You will need a **Git client** installed on your computer with the Path variable already set and the correct git credentials to 1M Bits Horde.

- In this repo, go to Code button, select SSH and copy the URL.
- In Unity, use the **Package Manager** "Add package from git URL..." feature and paste the URL.
- Set the version adding the suffix `#[x.y.z]` at URL

---

**1 Million Bits Horde**

[Website](https://www.1mbitshorde.com) -
[GitHub](https://github.com/1mbitshorde) -
[LinkedIn](https://www.linkedin.com/company/1m-bits-horde)
