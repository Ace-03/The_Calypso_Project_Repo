using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPrimaryWeaponProgressionDefinition", menuName = "Progression/PrimaryWeaponProgression")]
public class PrimaryWeaponProgressionSO : ScriptableObject
{
    [Tooltip("Experience is cumulative across levels. The list starts at level 2.")]
    public List<WeaponProgressionInfo> weaponLevelProgression;
}

[Serializable]
public class WeaponProgressionInfo
{
    public int playerLevel;
    public int iron;
    public int stone;
    public WeaponDefinitionSO LevelUpReward;
}
