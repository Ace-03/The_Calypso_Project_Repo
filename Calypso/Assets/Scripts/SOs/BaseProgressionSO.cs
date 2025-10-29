using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBaseLevelProgressionDefinition", menuName = "BaseLevelProgressionSO")]
public class BaseProgressionSO : ScriptableObject
{
    [Tooltip("Experience is cumulative across levels. The list starts at level 2.")]
    public List<BaseLevelUpRequirements> BaseLevelProgression;
}

[Serializable]
public class BaseLevelUpRequirements
{
    public int playerLevel;
    public int iron;
    public int stone;
    public int boatCount;
}
