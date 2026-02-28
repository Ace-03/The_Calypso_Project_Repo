using System.Resources;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RespawnHandler : MonoBehaviour
{
    /*
    what does this class need to do?
    currently when the player dies the scene hard resets. this class exists to add functionality to soft reset the scene
    this means when respawning the scene does not hard reset but instead just resets the player, removes enemies, adjusted in game time and day, and adjust related values
    this would be things like the player losing resoures, the base losing health etc.
    this class needs a public method that handles the soft reset logic
    it needs a reference to day night cycle, spawn manager, resource tracker, base health, and player health
    */

    [Header("Events")]
    [SerializeField] private OnRespawnPlayerEventSO respawnEvent;

    [Header("Components")]
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private DayCycle _dayNightCycle;
    [SerializeField] private ResourceTracker _resourceTracker;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private GenericHealth _baseHealth;

    [Header("Parameters")]
    [Tooltip("The number of resources needed to cap out on gamble assist")]
    [SerializeField] private float gambleResourceAsssistCap = 100;
    [Tooltip("increases odds by a maximum of n percent")]
    [Range(0, 1)][SerializeField] private float itemAssistRate = 0.5f;
    [Tooltip("1 in n success rate")]
    [Range(2.5f,8)][SerializeField] private float gambleSuccessOdds = 3;

    public static RespawnHandler instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void OnEnable()
    {
        respawnEvent.RegisterListener(RespawnPlayer);
    }

    private void OnDisable()
    {
        respawnEvent.UnregisterListener(RespawnPlayer);
    }

    public void RespawnPlayer(RespawnScenePayload payload)
    {
        bool gambleSuccess = false;

        // daynight: cycle set to morning and add 1 day
        _dayNightCycle.SkipCurrentDay();

        // spawn manager: clear enemies, reset wave sequence, set wave composite index accordingly
        _spawnManager.RemoveEnemies();

        // resource tracker: check if player is gambling to keep an item
        // if gambling then run check for good or bad path
        // Bad path: lose all resources
        // Good path: lose no resources and get to keep 1 passive as if player hasn't died
        if (payload.gambleResources)
            gambleSuccess = RollToKeepItem();

        // player manager: initialize health and reset player position to base
        _playerManager.ResetPlayer();

        // base health: bases loses x health, base loses health dependent on payload
    }

    public float CalculateBaseDamage()
    {
        // method to determine how much damage the base takes
        // base take flat 10 percent damage plus additional ammount relative to how
        // early into the night the player dies
        // This needs to be split into a separate method because base destruction needs
        // to be known earlier than when we try respawning the player
        return 0;
    }

    public bool RollToKeepItem()
    {
        float total = _resourceTracker.GetResource("stone") +
            _resourceTracker.GetResource("iron");

        float itemHelpRatio = total / gambleResourceAsssistCap;
        float trueOdds = gambleSuccessOdds * ((1 - itemAssistRate) * itemHelpRatio);
        float roll = Random.Range(0f, trueOdds);

        if (roll <= 1) return true;

        return false;
    }
}
