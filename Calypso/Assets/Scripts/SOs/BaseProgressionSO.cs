using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBaseLevelProgressionDefinition", menuName = "BaseLevelProgressionSO")]

public class BaseProgressionSO : ScriptableObject
{
    [Tooltip("Experience is cumulative across levels. The list starts at level 2.")]
    public List<int> LevelRequirements;
}
