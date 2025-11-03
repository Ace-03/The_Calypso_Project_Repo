using UnityEngine;

public abstract class IItemEffectSO: ScriptableObject
{
    public abstract void OnAquired(EquippedItemInstance itemInstance, PlayerContext context);
    public abstract void ExecuteEffect(PlayerContext context, GameEventPayload payload);
    public abstract void OnRemove(EquippedItemInstance itemInstance, PlayerContext context);
}