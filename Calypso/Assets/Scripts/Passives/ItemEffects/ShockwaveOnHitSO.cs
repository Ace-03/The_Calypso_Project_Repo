using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewShockwaveOnHit", menuName = "Item Effects/ShockwaveOnHit")]
public class ShockwaveOnHitSO : ItemEffectSO
{
    [SerializeField] private OnDamageDealtEventSO damageTakenEvent;

    private UnityAction<DamagePayload> damageListener;


    public override void ExecuteEffect(PlayerContext context, GameEventPayload payload)
    {
        Debug.Log("Executing Shockwave on hit");
    }

    public override void OnAcquired(ItemInstance itemInstance, PlayerContext context)
    {
        damageListener = (damagePayload) =>
        {
            ExecuteEffect(context, damagePayload);
        };

        damageTakenEvent.RegisterListener(damageListener);
    }

    public override void OnRemove(ItemInstance itemInstance, PlayerContext context)
    {
        damageTakenEvent.UnregisterListener(damageListener);
    }
}
