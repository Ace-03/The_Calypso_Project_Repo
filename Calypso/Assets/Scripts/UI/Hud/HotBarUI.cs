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
            AddItem(bar, item.sprite);
        }
    }

    public void RefreshBar(HotBar bar, List<WeaponDefinitionSO> weapons)
    {
        clearBar(bar);

        foreach (WeaponDefinitionSO item in weapons)
        {
            AddItem(bar, item.icon);
        }
    }

    public void UnlockNewSlot(HotBar bar)
    {
        HotBarSlotController lockedSlot = bar.Slots.FirstOrDefault(m => m.IsLocked == true);
        
        if (lockedSlot == null)
        {
            Debug.LogWarning($"Could Not find locked slot in {bar}");
            return;
        }

        lockedSlot.UnlockSlot();
    }

    private void AddItem(HotBar bar, Sprite icon)
    {
        HotBarSlotController openSlot = bar.Slots.FirstOrDefault(m => m.IsFilled == false);

        if (openSlot == null)
        {
            Debug.LogWarning($"Could Not find locked slot in {bar}");
            return;
        }

        openSlot.FillSlot(icon);
    }

    public HotBarSlotController GetHotBarSlot(WeaponDefinitionSO weaponData)
    {
        return hotBarElements.WeaponBar.Slots.Find(m => m.GetWeapon() == weaponData);
    }

    public HotBarSlotController GetHotBarSlot(PassiveItemSO ItemData)
    {
        return hotBarElements.WeaponBar.Slots.Find(m => m.GetPassiveItem() == ItemData);
    }


    private void clearBar(HotBar bar)
    {
        foreach (HotBarSlotController slot in bar.Slots)
            slot.ClearSlot();
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
