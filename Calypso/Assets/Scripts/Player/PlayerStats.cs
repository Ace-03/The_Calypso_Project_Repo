using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    #region Stats
    
    [Header("Character Stats")]
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int armor = 5;
    [SerializeField]
    private float recovery = 1; // health regen per second
    [SerializeField]
    private int luck = 100; // affects loot drops and critical hits


    [Header("Movement Stats")]
    [SerializeField]
    private float maxSpeed = 5;
    [SerializeField]
    private float acceleration = 5;
    [SerializeField]
    private float deceleration = 5;


    [Header("Movement Modifiers")]
    [SerializeField]
    private float spdModifier = 5;
    [SerializeField]
    private float accModifier = 5;
    [SerializeField]
    private float decelModifier = 5;


    [Header("Weapon Modifiers")]
    [SerializeField]
    private float strength = 100;
    [SerializeField]
    private float range = 100;
    [SerializeField]
    private float cooldown = 100;
    [SerializeField]
    private float duration = 100;
    [SerializeField]
    private int ammount = 100;
    
    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }


    #region Getters

    #region Player Stats
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetArmor()
    {
        return armor;
    }

    public float GetRecovery()
    {
        return recovery;
    }

    public int GetLuck()
    {
        return luck;
    }

    #endregion

    #region Movement Stats

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

    public float GetAcceleration()
    {
        return acceleration;
    }

    public float GetDeceleration()
    {
        return deceleration;
    }

    #endregion

    #region Movement Modifiers

    public float GetSpdModifier()
    {
        return spdModifier;
    }

    public float GetAccModifier()
    {
        return accModifier;
    }

    public float GetDecelModifier()
    {
        return decelModifier;
    }

    #endregion

    #region Weapon Modifiers

    public float GetStrength()
    {
        return strength;
    }

    public float GetRange()
    {
        return range;
    }

    public float GetCooldown()
    {
        return cooldown;
    }
    
    public float GetDuration()
    {
        return duration;
    }
    
    public int GetAmmount()
    {
        return ammount;
    }

    #endregion

    #endregion

    #region Setters

    #region Player Stats
    public void SetMaxHealth(int value)
    {
        maxHealth = value;
    }

    public void SetArmor(int value)
    {
        armor = value;
    }

    public void SetRecovery(float value)
    {
        recovery = value;
    }

    public void SetLuck(int value)
    {
        luck = value;
    }

    #endregion

    #region Movement Stats

    public void SetMaxSpeed(float value)
    {
        maxSpeed = value;
    }

    public void SetAcceleration(float value)
    {
        acceleration = value;
    }

    public void SetDeceleration(float value)
    {
        deceleration = value;
    }

    #endregion

    #region Movement Modifiers

    public void SetSpdModifier(float value)
    {
        spdModifier = value;
    }

    public void SetAccModifier(float value)
    {
        accModifier = value;
    }

    public void SetDecelModifier(float value)
    {
        decelModifier = value;
    }

    #endregion

    #region Weapon Modifiers

    public void SetStrength(float value)
    {
        strength = value;
    }

    public void SetRange(float value)
    {
        range = value;
    }

    public void SetCooldown(float value)
    {
        cooldown = value;
    }

    public void SetDuration(float value)
    {
        duration = value;
    }

    public void SetAmmount(int value)
    {
        ammount = value;
    }

    #endregion

    #endregion
}
