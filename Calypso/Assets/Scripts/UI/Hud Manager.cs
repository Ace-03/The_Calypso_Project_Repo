using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField]
    private HotBarUIElements hotBarElements;
    [SerializeField]
    private LevelUIElements levelElements;
    [SerializeField]
    private HealthUIElements healthElements;
    [SerializeField]
    private ResourceUIElements resourcesElements;

    public HotBarUI hotBar;
    public LevelUI level;
    public HealthUI health;
    public ResourcesUI resources;

    public static HudManager Instance;

    public void InitializeComponents()
    {
        if (!TryGetComponent<HotBarUI>(out hotBar))
            hotBar = gameObject.AddComponent<HotBarUI>();
        
        if (!TryGetComponent<LevelUI>(out level))
            level = gameObject.AddComponent<LevelUI>();
        
        if (!TryGetComponent<HealthUI>(out health))
            health = gameObject.AddComponent<HealthUI>();

        if (!TryGetComponent<ResourcesUI>(out resources))
            resources = gameObject.AddComponent<ResourcesUI>();
    
        hotBar.SetElements(hotBarElements);
        level.SetElements(levelElements);
        health.SetElements(healthElements);
        resources.SetElements(resourcesElements);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeComponents();
    }
}
