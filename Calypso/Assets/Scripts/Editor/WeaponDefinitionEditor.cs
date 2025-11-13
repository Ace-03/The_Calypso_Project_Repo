using UnityEditor;
using UnityEngine;

/*
[CustomEditor(typeof(WeaponDefinitionSO))]
public class WeaponDefinitionEditor : Editor
{
    SerializedProperty weaponName;
    SerializedProperty weaponDescription;
    SerializedProperty icon;
    SerializedProperty weaponBehaviorPrefab;
    SerializedProperty bulletSprite;
    SerializedProperty baseCooldown;
    SerializedProperty baseDamage;
    SerializedProperty baseProjectileSpeed;
    SerializedProperty baseAccuracy;
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
        weaponName = serializedObject.FindProperty("weaponDescription");
        weaponName = serializedObject.FindProperty("icon");
        weaponBehaviorPrefab = serializedObject.FindProperty("weaponBehaviorPrefab");
        bulletSprite = serializedObject.FindProperty("bulletSprite");
        baseCooldown = serializedObject.FindProperty("baseCooldown");
        baseDamage = serializedObject.FindProperty("baseDamage");
        baseProjectileSpeed = serializedObject.FindProperty("baseProjectileSpeed");
        baseAccuracy = serializedObject.FindProperty("baseAccuracy");
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
        EditorGUILayout.PropertyField(weaponDescription);
        EditorGUILayout.PropertyField(icon);
        EditorGUILayout.PropertyField(weaponBehaviorPrefab);
        EditorGUILayout.PropertyField(bulletSprite);
        EditorGUILayout.PropertyField(baseCooldown);
        EditorGUILayout.PropertyField(baseDamage);
        EditorGUILayout.PropertyField(baseProjectileSpeed);
        EditorGUILayout.PropertyField(baseAccuracy);
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
*/