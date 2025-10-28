public interface IItemEffect
{
    void OnAquired(EquippedItemInstance itemInstance, PlayerContext context);
    void ExecuteEffect(PlayerContext context, GameEventPayload payload);
    void OnRemove(EquippedItemInstance itemInstance, PlayerContext context);
}