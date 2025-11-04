using UnityEngine;

public abstract class ItemEffectSO: ScriptableObject
{
    public abstract void OnAcquired(EquippedItemInstance itemInstance, PlayerContext context);
    public abstract void ExecuteEffect(PlayerContext context, GameEventPayload payload);
    public abstract void OnRemove(EquippedItemInstance itemInstance, PlayerContext context);
}