using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HotBarUI : MonoBehaviour
{
    public HotBarUIElements hotBarElements;

    public void SetElements(HotBarUIElements elements)
    {
        hotBarElements = elements;
    }
    public void RefreshBar(HotBar bar, List<PassiveItemSO> passiveItems)
    {
        clearBar(bar);

        foreach (PassiveItemSO item in passiveItems)
        {
            AddItem(bar, item);
        }
    }

    public void RefreshBar(HotBar bar, List<WeaponDefinitionSO> passiveItems)
    {
        clearBar(bar);

        foreach (WeaponDefinitionSO item in passiveItems)
        {
            AddItem(bar, item);
        }
    }

    public void UnlockNewSlot(HotBar bar)
    {
        HotBarSlot lockedSlot = bar.Slots.FirstOrDefault(m => m.isLocked == true);

        lockedSlot.isLocked = false;
        lockedSlot.backgroundImage.sprite = hotBarElements.UnlockedBarSprite;
    }

    public HotBarSlot GetHotBarSlot(WeaponDefinitionSO weaponData)
    {
        return hotBarElements.WeaponBar.Slots.Find(m => m.assignedWeapon == weaponData);
    }

    public HotBarSlot GetHotBarSlot(PassiveItemSO ItemData)
    {
        return hotBarElements.WeaponBar.Slots.Find(m => m.assignedPassiveItem == ItemData);
    }

    private void AddItem(HotBar bar, WeaponDefinitionSO weaponData)
    {
        HotBarSlot openSlot = bar.Slots.FirstOrDefault(m => m.isFilled == false);

        if (openSlot.isLocked == false)
        {
            openSlot.iconImage.sprite = weaponData.icon;
            openSlot.isFilled = true;
        }
    }

    private void AddItem(HotBar bar, PassiveItemSO passiveItemData)
    {
        HotBarSlot openSlot = bar.Slots.FirstOrDefault(m => m.isFilled == false);

        if (openSlot.isLocked == false)
        {
            openSlot.iconImage.sprite = passiveItemData.sprite;
            openSlot.isFilled = true;
        }
    }

    private void removeItem(HotBarSlot slot)
    {
        slot.assignedPassiveItem = null;
        slot.assignedWeapon = null;
        slot.iconImage.sprite = hotBarElements.emptySlotSprite;
        slot.isFilled = false;
    }

    private void clearBar(HotBar bar)
    {
        foreach (HotBarSlot slot in bar.Slots)
        {
            removeItem(slot);
        }
    }

    public void ToggleWeaponBar(bool isVisible)
    {
        hotBarElements.WeaponBar.BarParent.SetActive(isVisible);
    }

    public void TogglePassivesBar(bool isVisible)
    {
        hotBarElements.PassiveBar.BarParent.SetActive(isVisible);
    }
}
