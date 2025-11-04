using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HotBarUI : MonoBehaviour
{
    public HotBarUIElements hotBarElements;

    public void SetElements(HotBarUIElements elements)
    {
        hotBarElements = elements;
    }

    public void AddItem(HotBar bar, WeaponDefinitionSO weaponData)
    {
        HotBarSlot openSlot = bar.Slots.FirstOrDefault(m => m.isFilled == false);

        if (openSlot.isLocked == false)
        {
            openSlot.iconImage.sprite = weaponData.icon;
            openSlot.isFilled = true;
        }
    }

    public void AddItem(HotBar bar, PassiveItemSO passiveItemData)
    {
        HotBarSlot openSlot = bar.Slots.FirstOrDefault(m => m.isFilled == false);

        if (openSlot.isLocked == false)
        {
            openSlot.iconImage.sprite = passiveItemData.sprite;
            openSlot.isFilled = true;
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

    public void ToggleWeaponBar(bool isVisible)
    {
        hotBarElements.WeaponBar.BarParent.SetActive(isVisible);
    }

    public void TogglePassivesBar(bool isVisible)
    {
        hotBarElements.PassiveBar.BarParent.SetActive(isVisible);
    }
}
