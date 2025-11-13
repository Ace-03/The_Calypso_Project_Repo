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
        Instantiate(projectilePrefab, context.playerManager.transform);
    }

    public override void OnAcquired(EquippedItemInstance itemInstance, PlayerContext context)
    {
        timerEvent = CreateInstance<OnGenericEventSO>();
        timer = context.playerManager.AddComponent<GenericTimer>();
        timer.Initialize(timerEvent, timerLength);

        timerListener = (TimerPayload) =>
        {
            ExecuteEffect(context, TimerPayload);
        };

        timerEvent.RegisterListener(timerListener);
    }

    public override void OnRemove(EquippedItemInstance itemInstance, PlayerContext context)
    {
        timerEvent.UnregisterListener(timerListener);
    }
}
