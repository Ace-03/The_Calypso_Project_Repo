using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PropGeneratorComposite : MonoBehaviour
{
    public List<PropGenerator> generatorComposite;

    [HideInInspector]
    public bool usingExclusionZones;
    
    private List<PropGenerator> nullTracker = new List<PropGenerator>();


    public void RegenerateAllProps()
    {
        foreach (PropGenerator propGenerator in generatorComposite)
        {
            if (CheckNullItem(propGenerator))
                continue;

            propGenerator.RegenerateProps();
        }
        removeNulls();
    }

    public void ClearAllProps()
    {
        foreach (PropGenerator propGenerator in generatorComposite)
        {
            if (CheckNullItem(propGenerator))
                continue;

            propGenerator.ClearProps();
        }
        removeNulls();
    }

    public bool ToggleExclusionZones()
    {
        usingExclusionZones = !usingExclusionZones;

        foreach (PropGenerator propGenerator in generatorComposite)
        {
            if (CheckNullItem(propGenerator))
                continue;

            propGenerator.useExclusionZones = usingExclusionZones;
        }
        removeNulls();

        return usingExclusionZones;
    }

    bool CheckNullItem(PropGenerator gen)
    {
        if (gen == null)
        {
            nullTracker.Add(null);
            return true;
        }

        return false;
    }

    void removeNulls()
    {
        if (nullTracker.Count <= 0)
            return;

        foreach (PropGenerator gen in nullTracker)
        {
            if (generatorComposite.Contains(gen))
                generatorComposite.Remove(gen);
        }

        nullTracker.Clear();
    }
}
