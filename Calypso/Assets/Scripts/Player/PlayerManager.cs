using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameObject player;

    public static PlayerManager Instance;

    #region Stats

    [Header("Player Stats")]
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int armor = 5;
    [SerializeField]
    private float recovery = 1; // health regen per second
    [SerializeField]
    private float invulnerabilityPeriod = 0.5f;
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
    private float strength = 1.1f;
    [SerializeField]
    private float dexterity = 1.2f;
    [SerializeField]
    private float area = 1.3f;
    [SerializeField]
    private float cooldown = 0.8f;
    [SerializeField]
    private float duration = 1.3f;
    [SerializeField]
    private int ammount = 1;
    
    #endregion

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
    public float GetDexterity()
    {
        return dexterity;
    }

    public float GetArea()
    {
        return area;
    }

    public float GetCooldown()
    {
        return cooldown;
    }
    
    public float GetDuration()
    {
        return duration;
    }
    
    public int GetAmount()
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
    public void SetDexterity(float value)
    {
        dexterity = value;
    }

    public void SetRange(float value)
    {
        area = value;
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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        InitializePlayer();
    }

    private void InitializePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        InitializePlayerHealth();
    }

    private void InitializePlayerHealth()
    {
        if (!player.TryGetComponent<PlayerHealth>(out var hs))
            hs = player.AddComponent<PlayerHealth>();

        hs.Initialize(new PlayerHealthData
        {
            invulnerabilityDuration = invulnerabilityPeriod,
            maxHP = maxHealth,
        });
    }

    /* ApplyPassiveModifiers exists to apply passive item
     * effects to the weapon. 
     * 
     * the number of passive items and specific effects are
     * unknown. To approach this there will likely be a
     * "passives controller" script which holds an array of
     * passive items. each passive will have an interface with
     * a "ApplyModifier(PlayerStats stats)" method that 
     * will do the work to modify the stats.
     * 
     */
    public void ApplyPassiveModifiers()
    {

    }
}
