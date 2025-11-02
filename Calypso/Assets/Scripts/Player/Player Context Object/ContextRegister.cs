using UnityEngine;

public class ContextRegister : MonoBehaviour
{

    private PlayerContext playerContext;

    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private ResourceTracker resourceTracker;
    [SerializeField] private PlayerLevelManager levelManager;
    [SerializeField] private DayCycle DayCycle;
    [SerializeField] private SpawnManager spawnManager;
    private Transform playerTransform;

    public static ContextRegister Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        playerTransform = FindFirstObjectByType<PlayerController>().transform;
        GenerateContext();
    }
    public PlayerContext GetContext()
    {
        return playerContext;
    }

    private void GenerateContext()
    {
        playerContext = new PlayerContext()
        {
            playerManager = playerManager,
            inventoryManager = inventoryManager,
            playerTransform = playerTransform,
            resourceTracker = resourceTracker,
            levelManager = levelManager,
            DayCycle = DayCycle,
            spawnManager = spawnManager
        };
    }
}


public class PlayerContext
{
    public PlayerManager playerManager;
    public InventoryManager inventoryManager;
    public Transform playerTransform;
    public ResourceTracker resourceTracker;
    public PlayerLevelManager levelManager;
    public DayCycle DayCycle;
    public SpawnManager spawnManager;
}
