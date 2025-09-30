using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PropGeneratorComposite : MonoBehaviour
{
    public List<PropGenerator> generatorComposite;

    [HideInInspector]
    public bool usingExclusionZones;

    public void RegenerateAllProps()
    {
        foreach (PropGenerator propGenerator in generatorComposite)
            propGenerator.RegenerateProps();
    }

    public void ClearAllProps()
    {
        foreach (PropGenerator propGenerator in generatorComposite)
            propGenerator.ClearProps();
    }

    public bool ToggleExclusionZones()
    {
        usingExclusionZones = !usingExclusionZones;

        foreach (PropGenerator propGenerator in generatorComposite)
            propGenerator.useExclusionZones = usingExclusionZones;

        return usingExclusionZones;
    }

}
