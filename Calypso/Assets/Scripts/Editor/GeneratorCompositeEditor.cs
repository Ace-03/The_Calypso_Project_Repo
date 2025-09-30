using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PropGeneratorComposite))]
public class GeneratorCompositeEditor : Editor
{
    private bool usingExclusionZones;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PropGeneratorComposite composite = (PropGeneratorComposite)target;

        usingExclusionZones = composite.usingExclusionZones;

        if (GUILayout.Button("Regenerate Props"))
            composite.RegenerateAllProps();

        if (GUILayout.Button("Clear Props In Editor"))
            composite.ClearAllProps();

        if (GUILayout.Button("Toggle Use Exclusion Zones (Currently " + usingExclusionZones + ")"))
        {
            usingExclusionZones = composite.ToggleExclusionZones();
        }
    }
}
