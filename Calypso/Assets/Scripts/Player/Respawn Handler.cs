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

    [SerializeField] private OnRespawnPlayerEventSO respawnEvent;

    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private DayCycle _dayNightCycle;
    [SerializeField] private ResourceTracker _resourceTracker;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private GenericHealth _baseHealth; 

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
        // daynight: cycle set to morning and add 1 day

        // spawn manager: clear enemies, reset wave sequence, set wave composite index accordingly

        // resource tracker: check if player is gambling for recources
        // if gambling then run check for good or bad path
        // Bad path: lose all resources
        // Good path: lose no resources and get to keep 1 passive as if player hasn't died
    }

}
