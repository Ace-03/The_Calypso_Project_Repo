using UnityEngine;

public abstract class ItemEffectSO: ScriptableObject
{
    public abstract void OnAcquired(ItemInstance itemInstance, PlayerContext context);
    public abstract void ExecuteEffect(PlayerContext context, GameEventPayload payload);
    public abstract void OnRemove(ItemInstance itemInstance, PlayerContext context);
}