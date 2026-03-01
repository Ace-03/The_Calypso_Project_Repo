using UnityEngine;

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
    [SerializeField] private BaseHealth _baseHealth;
    [SerializeField] private PassiveRestrictionScreen _passiveRestrictionScreen;

    [Header("Parameters")]
    [Tooltip("The number of resources needed to cap out on gamble assist")]
    [SerializeField] private float gambleResourceAsssistCap = 100;
    [Tooltip("increases odds by a maximum of n percent")]
    [Range(0, 0.8f)][SerializeField] private float itemAssistRate = 0.2f;
    [Tooltip("1 in n success rate")]
    [Range(2.5f,8)][SerializeField] private float gambleSuccessOdds = 3;

    public static RespawnHandler Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        Debug.Log($"item assist rate is {itemAssistRate}");
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
        // daynight: cycle set to morning and add 1 day
        _dayNightCycle.SkipCurrentDay();

        // spawn manager: clear enemies, reset wave sequence, set wave composite index accordingly
        _spawnManager.RemoveEnemies();

        // resource tracker: check if player is gambling to keep an item
        // if gambling then run check for good or bad path
        // Bad path: lose all resources
        // Good path: lose no resources and get to keep 1 passive as if player hasn't died
        if (!payload.gambleSuccess)
        {
            _passiveRestrictionScreen.OnSkipSelected();
            _resourceTracker.SetResource("stone", 0);
            _resourceTracker.SetResource("iron", 0);
        }

            // player manager: initialize health and reset player position to base
            _playerManager.ResetPlayer();

        // base health: bases loses x health, base loses health dependent on payload
        _baseHealth.TakeDamageRaw(payload.calculatedBaseDamage);
    }

    public float CalculateBaseDamage()
    {
        float flatRate = _baseHealth.maxHP / 10;
        float nightProgressValue = (1 - _dayNightCycle.GetDayProgressPercentage()) * (_baseHealth.maxHP / 3);
        return flatRate + nightProgressValue;
    }

    public bool RollToKeepItem()
    {
        // 1. Calculate Base Chance (Inverse of SuccessOdds)
        // If gambleSuccessOdds is 3, baseChance is 0.33 (33%)
        float baseChance = 1f / gambleSuccessOdds;

        // 2. Calculate Resource Bonus
        float totalResources = _resourceTracker.GetResource("stone") + _resourceTracker.GetResource("iron");
        float resourceRatio = Mathf.Clamp01(totalResources / gambleResourceAsssistCap);

        // 3. Calculate Final Success Threshold
        // itemAssistRate acts as the "Max Bonus" added to the base chance.
        // Example: 0.33 (Base) + (1.0 (Full Resources) * 0.2 (Assist Rate)) = 0.53 (53% total chance)
        float successThreshold = baseChance + (resourceRatio * itemAssistRate);
        successThreshold = Mathf.Clamp01(successThreshold); // Ensure it never exceeds 100%

        // 4. The Roll
        float roll = Random.value; // Returns a float between 0.0 and 1.0
        bool isWin = roll <= successThreshold;

        // Debugging
        Debug.Log($"--- Gambling Roll ---");
        Debug.Log($"Base Chance: {baseChance * 100}%");
        Debug.Log($"Resource Bonus: {resourceRatio * itemAssistRate * 100}%");
        Debug.Log($"Total Win Chance: {successThreshold * 100}%");
        Debug.Log($"Roll Result: {roll} (Needs to be <= {successThreshold})");
        Debug.Log($"Outcome: {(isWin ? "SUCCESS" : "FAIL")}");

        return isWin;
    }
}
