using UnityEngine;

public class HotBarSlotController : MonoBehaviour
{
    [SerializeField] private SlotComponents components;
    [SerializeField] private Sprite emptySlot;
    [SerializeField] private Sprite unlockedSprite;

    public bool IsFilled;
    public bool IsLocked;

    public void ClearSlot()
    {
        components.assignedPassiveItem = null;
        components.assignedWeapon = null;
        components.iconImage.sprite = emptySlot;
        IsFilled = false;
    }

    public void UnlockSlot()
    {
        IsLocked = false;
        components.backgroundImage.sprite = unlockedSprite;
    }

    public void FillSlot(Sprite itemSprite)
    {
        if (IsFilled)
        {
            Debug.LogError($"Attempting to fill an occupied hotbar Slot with {itemSprite.name}");
            return;
        }

        components.iconImage.sprite = itemSprite;
        IsFilled = true;
    }

    public WeaponController GetWeapon()
    {
        return components.assignedWeapon;
    }

    public PassiveItemSO GetPassiveItem()
    {
        return components.assignedPassiveItem;
    }

    public void SetWeapon(WeaponController controller)
    {
        components.assignedWeapon = controller;
    }
    
    public void SetPassiveItem(PassiveItemSO data)
    {
        components.assignedPassiveItem = data;
    }
}
