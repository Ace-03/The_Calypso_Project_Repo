using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TutorialSequence
{
    [HideInInspector] public bool cleared = false;
    public List<string> messages;
}
