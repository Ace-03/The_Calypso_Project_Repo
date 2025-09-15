using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;


    [Header("player Stats")]
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int armor = 100;
    [SerializeField]
    private int recovery = 100;
    [SerializeField]
    private int luck = 100;

    [Header("Movement Stats")]
    [SerializeField]
    private int maxSpeed = 5;
    [SerializeField]
    private int acceleration = 5;
    [SerializeField]
    private int deceleration = 5;



    [Header("Weapon Modifiers")]
    [SerializeField]
    private int strength = 100;
    [SerializeField]
    private int range = 100;
    [SerializeField]
    private int cooldown = 100;
    [SerializeField]
    private int duration = 100;
    [SerializeField]
    private int ammount = 100;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }
}
