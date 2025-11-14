using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerBaseStats", menuName = "Scriptable Objects/PlayerBaseStats")]
public class PlayerBaseStatsSO : ScriptableObject
{
    [Header("Player Stats")]
    public int maxHealth = 100;
    public int armor = 0;
    public float recovery = 1; // health regen per second
    public float invulnerabilityPeriod = 0.5f;
    public int luck = 100; // affects loot drops and critical hits
    public float ItemAttraction = 1.2f; // radius for item attraction

    [Header("Movement Stats")]
    public float maxSpeed = 10;
    public float acceleration = 20;
    public float deceleration = 40;

    [Header("Weapon Modifiers")]
    public float strength = 1f;
    public float dexterity = 1f;
    public float size = 1f;
    public float cooldown = 1f;
    public float duration = 1f;
    public int amount = 0;
}
