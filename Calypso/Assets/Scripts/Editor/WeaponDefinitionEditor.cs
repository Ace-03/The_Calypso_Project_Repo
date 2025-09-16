using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponDefinitionSO))]
public class WeaponDefinitionEditor : Editor
{
    SerializedProperty weaponName;
    SerializedProperty weaponBehaviorPrefab;
    SerializedProperty icon;
    SerializedProperty baseCooldown;
    SerializedProperty baseDamage;
    SerializedProperty baseProjectileSpeed;
    SerializedProperty baseDuration;
    SerializedProperty baseAmmount;
    SerializedProperty hasAOE;
    SerializedProperty aoeAreaSize;
    SerializedProperty aoeTickRate;
    SerializedProperty aoeShape;

    private void OnEnable()
    {
        weaponName = serializedObject.FindProperty("weaponName");
        weaponBehaviorPrefab = serializedObject.FindProperty("weaponBehaviorPrefab");
        icon = serializedObject.FindProperty("icon");
        baseCooldown = serializedObject.FindProperty("baseCooldown");
        baseDamage = serializedObject.FindProperty("baseDamage");
        baseProjectileSpeed = serializedObject.FindProperty("baseProjectileSpeed");
        baseDuration = serializedObject.FindProperty("baseDuration");
        baseAmmount = serializedObject.FindProperty("baseAmmount");
        hasAOE = serializedObject.FindProperty("hasAOE");
        aoeAreaSize = serializedObject.FindProperty("aoeAreaSize");
        aoeTickRate = serializedObject.FindProperty("aoeTickRate");
        aoeShape = serializedObject.FindProperty("aoeShape");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(weaponName);
        EditorGUILayout.PropertyField(weaponBehaviorPrefab);
        EditorGUILayout.PropertyField(icon);
        EditorGUILayout.PropertyField(baseCooldown);
        EditorGUILayout.PropertyField(baseDamage);
        EditorGUILayout.PropertyField(baseProjectileSpeed);
        EditorGUILayout.PropertyField(baseDuration);
        EditorGUILayout.PropertyField(baseAmmount);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(hasAOE, new GUIContent("Has Area of Effect"));

        if (hasAOE.boolValue)
        {
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(aoeAreaSize);
            EditorGUILayout.PropertyField(aoeTickRate);
            EditorGUILayout.PropertyField(aoeShape);

        }

        serializedObject.ApplyModifiedProperties();
    }
}
