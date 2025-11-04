using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField]
    private GameObject hotBarGroup;
    [SerializeField]
    private GameObject levelGroup;
    [SerializeField]
    private GameObject healthGroup;
    [SerializeField]
    private GameObject resourcesGroup;
    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private HotBarUIElements hotBarElements;
    [SerializeField]
    private LevelUIElements levelElements;
    [SerializeField]
    private HealthUIElements healthElements;
    [SerializeField]
    private ResourceUIElements resourcesElements;

    [HideInInspector]
    public HotBarUI hotBar;
    [HideInInspector]
    public LevelUI level;
    [HideInInspector]
    public HealthUI health;
    [HideInInspector]
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

        resources.UpdateBoatIcons(0);
        gameOverScreen.SetActive(false);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeComponents();
    }

    public void StartGameOver()
    {
        ToggleHud(false);
        gameOverScreen.SetActive(true);
    }

    public void ToggleHud(bool toggle)
    {
        hotBarGroup.SetActive(toggle);
        levelGroup.SetActive(toggle);
        healthGroup.SetActive(toggle);
        resourcesGroup.SetActive(toggle);
    }
}
