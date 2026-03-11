using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    [SerializeField] private OnEnemyDeathEventSO bossDeathEvent;
    [SerializeField] private EnemyDefinitionSO bossData;
    [SerializeField] private OnDeathEventSO playerDeathEvent;

    [Header("Boss Components")]
    [SerializeField] private Transform bossHomePosition;
    private NavMeshAgent navigation;
    private AI_NAV navController;
    private EnemyHealth bossHealth;
    private EnemyInitializer initializer;
    private Transform bossTransform;
    private WeaponController weapnoCtrl;
    public bool isActive;


    private void OnEnable()
    {
        bossDeathEvent.RegisterListener(OnBossDeath);
        playerDeathEvent.RegisterListener(ForceDeactivateBoss);
    }

    private void OnDisable()
    {
        bossDeathEvent.UnregisterListener(OnBossDeath);
        playerDeathEvent.UnregisterListener(ForceDeactivateBoss);
    }

    private void Start()
    {
        navController = GetComponentInChildren<AI_NAV>();
        navigation = GetComponentInChildren<NavMeshAgent>();
        bossHealth = GetComponentInChildren<EnemyHealth>();
        initializer = GetComponentInChildren<EnemyInitializer>();
        weapnoCtrl = GetComponentInChildren<WeaponController>();

        initializer.Initialize(bossData);
        bossTransform = initializer.transform;
        bossTransform.parent = null;
        ActivateBoss(false);
    }

    public void ActivateBoss(bool active)
    {
        navigation.enabled = active;
        navController.enabled = active;
        weapnoCtrl.enabled = active;
        isActive = active;
        bossTransform.position = bossHomePosition.position;
        bossHealth.Initialize(new HealthData { maxHP = bossData.maxHealth });
    }

    private void OnBossDeath(DeathPayload payload)
    {
        if (payload.entity == initializer.gameObject)
        {
            Debug.Log("Boss Has died. Boss controller deactivating");
            gameObject.SetActive(false);
        }
        else if (payload.entity == null)
        {
            Debug.Log("payload entity is null");
        }
    }

    private void ForceDeactivateBoss(DeathPayload payload)
    {
        ActivateBoss(false);
    }
}
