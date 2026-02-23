using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewTimedProjectileSpawn", menuName = "Item Effects/TimedProjectileSpawn")]
public class TimedProjectileSpawnSO : ItemEffectSO
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float timerLength;
    private OnGenericEventSO timerEvent;
    private GenericTimer timer;

    private UnityAction<GameEventPayload> timerListener;

    public override void ExecuteEffect(PlayerContext context, GameEventPayload payload)
    {
        SpawnNet(context);
    }

    public override void OnAcquired(ItemInstance itemInstance, PlayerContext context)
    {
        timerEvent = CreateInstance<OnGenericEventSO>();
        timer = context.playerManager.AddComponent<GenericTimer>();
        timer.Initialize(timerEvent, timerLength);

        timerListener = (TimerPayload) =>
        {
            ExecuteEffect(context, TimerPayload);
        };

        timerEvent.RegisterListener(timerListener);
        SpawnNet(context);
    }

    public override void OnRemove(ItemInstance itemInstance, PlayerContext context)
    {
        timerEvent.UnregisterListener(timerListener);
    }


    private void SpawnNet(PlayerContext context)
    {
        Vector3 spawnPosition = new Vector3(context.playerTransform.position.x, 0.2f, context.playerTransform.position.z);

        GameObject net = Instantiate(projectilePrefab);
        net.transform.position = spawnPosition;

        Destroy(net, 15f);
    }
}
