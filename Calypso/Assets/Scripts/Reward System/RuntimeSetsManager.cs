using System;
using UnityEngine;

public class RuntimeSetsManager : MonoBehaviour
{
    [SerializeField] private PassiveItemSetSO passiveItemSet;
    [SerializeField] private WeaponSetSO weaponSet;

    private const string passiveItemsPath = "Scriptable Objects/Items";
    private const string weaponsPath = "Scriptable Objects/PlayerWeapons";

    private void Awake()
    {
        LoadList(passiveItemSet, passiveItemsPath);
        LoadList(weaponSet, weaponsPath); 
    }
    
    private void LoadList<T>(RuntimeSetSO<T> runtimeSet, string resourcePath) where T : ScriptableObject
    {
        runtimeSet.Clear();
        T[] items = Resources.LoadAll<T>(resourcePath);
        foreach (T item in items)
        {
            runtimeSet.Add(item);
        }
    }
}
