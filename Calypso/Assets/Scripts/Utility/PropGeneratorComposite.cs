using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PropGeneratorComposite : MonoBehaviour
{
    public List<PropGenerator> generatorComposite;

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

}
