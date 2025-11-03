using UnityEngine;

[CreateAssetMenu(fileName = "NewIncreaseStat", menuName = "Scriptable Objects/Increasestat")]
public class IncreaseStatOnAquireSO : IItemEffectSO
{
    [Header("Stat Modifier Config")]
    public float valueToIncrease = 0.25f; // e.g., +25% Movement Speed
    public StatModifierType ModifierType = StatModifierType.AdditivePercentage; // Enum for stat system
    public StatType type = StatType.MaxSpeed;

    public override void OnAquired(EquippedItemInstance itemInstance, PlayerContext context)
    {
        string modifierSourceID = $"{itemInstance.instanceID}_{type}";

        //query reward generator to make new stat modifier.
    }

    public override void OnRemove(EquippedItemInstance itemInstance, PlayerContext context)
    {
        string modifierSourceID = $"{itemInstance.instanceID}_{type}";

        context.statSystem.RemoveAllModifiersBySource(type, modifierSourceID);
    }

    public override void ExecuteEffect(PlayerContext context, GameEventPayload payload)
    {
    }
}
