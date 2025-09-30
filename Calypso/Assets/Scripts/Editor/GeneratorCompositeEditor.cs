using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PropGeneratorComposite))]
public class GeneratorCompositeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PropGeneratorComposite composite = (PropGeneratorComposite)target;

        if (GUILayout.Button("Regenerate Props"))
            composite.RegenerateAllProps();

        if (GUILayout.Button("Clear Props In Editor"))
            composite.ClearAllProps();
    }
}
