using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBaseLevelProgressionDefinition", menuName = "Progression/BaseLevelProgression")]
public class BaseProgressionSO : ScriptableObject
{
    [Tooltip("Experience is cumulative across levels. The list starts at level 2.")]
    public List<BaseProgressionInfo> BaseLevelProgression;
}

[Serializable]
public class BaseProgressionInfo
{
    public int playerLevel;
    public int iron;
    public int stone;
    public int boatCount;
}
