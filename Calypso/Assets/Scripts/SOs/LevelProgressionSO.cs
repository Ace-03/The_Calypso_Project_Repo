using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProgressionDefinition", menuName = "LevelProgressionSO")]

public class LevelProgressionSO : ScriptableObject
{
    [Tooltip("Experience is cumulative across levels. The list starts at level 2.")]
    public List<int> LevelRequirements;
}
