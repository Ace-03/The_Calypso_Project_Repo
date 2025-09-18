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
    SerializedProperty baseAmount;
    SerializedProperty hasAOE;
    SerializedProperty aoeAreaSize;
    SerializedProperty aoeTickRate;
    SerializedProperty aoeShape;

    SerializedProperty baseKnockback;
    SerializedProperty baseStun;
    SerializedProperty basePoison;
    SerializedProperty baseSlowdown;

    private void OnEnable()
    {
        weaponName = serializedObject.FindProperty("weaponName");
        weaponBehaviorPrefab = serializedObject.FindProperty("weaponBehaviorPrefab");
        icon = serializedObject.FindProperty("icon");
        baseCooldown = serializedObject.FindProperty("baseCooldown");
        baseDamage = serializedObject.FindProperty("baseDamage");
        baseProjectileSpeed = serializedObject.FindProperty("baseProjectileSpeed");
        baseDuration = serializedObject.FindProperty("baseDuration");
        baseAmount = serializedObject.FindProperty("baseAmount");
        hasAOE = serializedObject.FindProperty("hasAOE");
        aoeAreaSize = serializedObject.FindProperty("aoeAreaSize");
        aoeTickRate = serializedObject.FindProperty("aoeTickRate");
        aoeShape = serializedObject.FindProperty("aoeShape");

        baseKnockback = serializedObject.FindProperty("baseKnockback");
        baseStun = serializedObject.FindProperty("baseStun");
        basePoison = serializedObject.FindProperty("basePoison");
        baseSlowdown = serializedObject.FindProperty("baseSlowdown");
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
        EditorGUILayout.PropertyField(baseAmount);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(hasAOE, new GUIContent("Has Area of Effect"));

        if (hasAOE.boolValue)
        {
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(aoeAreaSize);
            EditorGUILayout.PropertyField(aoeTickRate);
            EditorGUILayout.PropertyField(aoeShape);

        }

        EditorGUILayout.PropertyField(baseKnockback);
        EditorGUILayout.PropertyField(baseStun);
        EditorGUILayout.PropertyField(basePoison);
        EditorGUILayout.PropertyField(baseSlowdown);

        serializedObject.ApplyModifiedProperties();
    }
}
