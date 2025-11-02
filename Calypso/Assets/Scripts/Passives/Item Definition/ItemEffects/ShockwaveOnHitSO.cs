using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewShockwaveOnHit", menuName = "Scriptable Objects/ShockwaveOnHitSO")]
public class ShockwaveOnHitSO : ScriptableObject, IItemEffect
{
    [SerializeField] private OnDamageDealtEventSO damageTakenEvent;

    private UnityAction<DamagePayload> damageListener;


    public void ExecuteEffect(PlayerContext context, GameEventPayload payload)
    {
        Debug.Log("Executing Shockwave on hit");
    }

    public void OnAquired(EquippedItemInstance itemInstance, PlayerContext context)
    {
        damageListener = (damagePayload) =>
        {
            ExecuteEffect(context, damagePayload);
        };

        damageTakenEvent.RegisterListener(damageListener);
    }

    public void OnRemove(EquippedItemInstance itemInstance, PlayerContext context)
    {
        damageTakenEvent.UnregisterListener(damageListener);
    }
}
