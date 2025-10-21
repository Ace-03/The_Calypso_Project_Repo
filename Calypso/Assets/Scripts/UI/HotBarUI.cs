using UnityEngine;
using UnityEngine.UI;

public class HotBarUI : MonoBehaviour
{
    public HotBarUIElements hotBarElements;

    public void SetElements(HotBarUIElements elements)
    {
        hotBarElements = elements;
    }

    public void AddWeaponSlot(Sprite weaponIcon)
    {
        HotBarIcon slot = new HotBarIcon();
        GameObject slotObject = Instantiate(hotBarElements.hotBarSlotPrefab, hotBarElements.weaponBar.transform);
        
        slot.slotObject = slotObject;
        slot.backgroundImage = slotObject.transform.Find("Background").GetComponent<Image>();
        slot.iconImage = slotObject.transform.Find("Icon").GetComponent<Image>();

        UpdateWeaponSlot(slot, weaponIcon);
    }

    public void AddPassiveSlot(Sprite passiveIcon)
    {
        HotBarIcon slot = new HotBarIcon();
        GameObject slotObject = Instantiate(hotBarElements.hotBarSlotPrefab, hotBarElements.weaponBar.transform);

        slot.slotObject = slotObject;
        slot.backgroundImage = slotObject.transform.Find("Background").GetComponent<Image>();
        slot.iconImage = slotObject.transform.Find("Icon").GetComponent<Image>();

        UpdateWeaponSlot(slot, passiveIcon);
    }

    public bool RemoveWeaponSlot(HotBarIcon slot)
    {
        if (hotBarElements.weaponSlots.Contains(slot))
        {
            hotBarElements.weaponSlots.Remove(slot);
            Destroy(slot.slotObject);
            hotBarElements.weaponSlots.Remove(slot);

            return true;
        }
        return false;
    }

    public bool RemovePassiveSlot(HotBarIcon slot)
    {
        if (hotBarElements.passiveSlots.Contains(slot))
        {
            hotBarElements.passiveSlots.Remove(slot);
            Destroy(slot.slotObject);
            hotBarElements.passiveSlots.Remove(slot);
            return true;
        }
        return false;
    }

    public void UpdateWeaponSlot(int index, Sprite weaponIcon)
    {
        if (index < 0 || index >= hotBarElements.weaponSlots.Count)
        {
            Debug.LogError("Invalid weapon slot index");
            return;
        }
        var slotImage = hotBarElements.weaponSlots[index].iconImage;
        slotImage.sprite = weaponIcon;
    }

    public void UpdatePassiveSlot(int index, Sprite passiveIcon)
    {
        if (index < 0 || index >= hotBarElements.passiveSlots.Count)
        {
            Debug.LogError("Invalid passive slot index");
            return;
        }
        var slotImage = hotBarElements.passiveSlots[index].iconImage;
        slotImage.sprite = passiveIcon;
    }

    public void UpdateWeaponSlot(HotBarIcon slot, Sprite weaponIcon)
    {
        if (!hotBarElements.weaponSlots.Contains(slot))
        {
            Debug.LogError("Invalid Weapon Slot");
            return;
        }
        var slotImage = slot.iconImage;
        slotImage.sprite = weaponIcon;
    }

    public void UpdatePassiveSlot(HotBarIcon slot, Sprite passiveIcon)
    {
        if (!hotBarElements.weaponSlots.Contains(slot))
        {
            Debug.LogError("Invalid Passive Slot");
            return;
        }
        var slotImage = slot.iconImage;
        slotImage.sprite = passiveIcon;
    }

    public void ToggleWeaponBar(bool isVisible)
    {
        hotBarElements.weaponBar.SetActive(isVisible);
    }

    public void TogglePassivesBar(bool isVisible)
    {
        hotBarElements.passivesBar.SetActive(isVisible);
    }
}
