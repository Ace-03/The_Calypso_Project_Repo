using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private OnDeathEventSO deathEvent;
    [SerializeField] private OnStatsUpdatedSO statsUpdatedEvent;

    [SerializeField] private GameObject playerVisuals;
    [SerializeField] private GameObject inputManager;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private SphereCollider attractorTrigger;
    [SerializeField] private WeaponController primaryWeapon;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerController playerController;

    private StatSystem statSystem;

    public static PlayerManager Instance;

    private void OnEnable()
    {
        statsUpdatedEvent.RegisterListener(UpdatePlayerStats);
    }

    private void OnDisable()
    {
        statsUpdatedEvent.UnregisterListener(UpdatePlayerStats);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        statSystem = ContextRegister.Instance.GetContext().statSystem;
        InitializePlayer();
    }

    private void InitializePlayer()
    {
        InitializePlayerHealth();
    }

    private void InitializePlayerHealth()
    {
        if (!TryGetComponent<PlayerHealth>(out var hs))
            hs = gameObject.AddComponent<PlayerHealth>();

        hs.Initialize(new PlayerHealthData
        {
            invulnerabilityDuration = statSystem.GetFinalValue(StatType.Invulnerability),
            maxHP = (int)statSystem.GetFinalValue(StatType.MaxHealth),
        });
    }

    public void OnDeath()
    {
        DeathPayload payload = new DeathPayload()
        {
            entity = this.gameObject,
        };

        deathEvent.Raise(payload);

        ToggleMovement(false);
        ToggleWeapons(false);
        ToggleVisuals(false);

        SpawnDeathParticle();
        Debug.Log("Game Over");

        HudManager.Instance.StartGameOver();
    }

    #region Player State Handlers

    public void ToggleMovement(bool state)
    {
        inputManager.GetComponent<SimpleInputManager>().enabled = state;
    }

    public void ToggleWeapons(bool state)
    {
        gameObject.GetComponent<WeaponController>().enabled = state;
    }

    public void ToggleVisuals(bool state)
    {
        playerVisuals.SetActive(state);
        gameObject.GetComponent<PlayerController>().ToggleCollider(state);
    }

    #endregion

    public void SpawnDeathParticle()
    {
        GameObject particle = Instantiate(deathParticle, transform.position, Quaternion.identity);
    }

    public void UpdateAttractorSize(float value)
    {
        attractorTrigger.radius = value;
    }

    private void UpdatePlayerStats(StatUpdatePayload payload)
    {
        primaryWeapon.RecalculateStats();
        playerHealth.UpdateHealthStats(payload.statSystem);
        playerController.RecalculateMovementStats(payload.statSystem);
    }

    public WeaponController GetPrimaryWeapon()
    {
        return primaryWeapon;
    }
}
