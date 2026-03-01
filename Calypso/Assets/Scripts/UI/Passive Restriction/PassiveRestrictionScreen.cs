using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassiveRestrictionScreen : MonoBehaviour
{
    [SerializeField] private OnDayStateChangeEventSO nightEndedEvent;
    [SerializeField] private OnGenericEventSO passiveRestrictionSelectedEvent;

    [Header("UI Components")]
    [SerializeField] private GameObject screenCanvas;
    [SerializeField] private TextMeshProUGUI nightClearedText;
    [SerializeField] private GameObject itemOptionPrefab;
    [SerializeField] private RectTransform newItemContainer;
    [SerializeField] private RectTransform ownedItemContainer;

    [SerializeField] private OptionInfoPanelComponents bigPanel;

    private List<GameObject> ownedList = new List<GameObject>();
    private List<GameObject> newList = new List<GameObject>();

    private void OnEnable()
    {
        nightEndedEvent.RegisterListener(DisplayPassiveRestrictionScreen);
        passiveRestrictionSelectedEvent.RegisterListener(HidePassiveRestrictionScreen);
    }

    private void OnDisable()
    {
        nightEndedEvent.UnregisterListener(DisplayPassiveRestrictionScreen);
        passiveRestrictionSelectedEvent.UnregisterListener(HidePassiveRestrictionScreen);
    }

    public void OnSkipSelected()
    {
        passiveRestrictionSelectedEvent.Raise(new GameEventPayload());
    }

    private void HidePassiveRestrictionScreen(GameEventPayload payload)
    {
        Time.timeScale = 1.0f;
        screenCanvas.SetActive(false);
        HudManager.Instance.ToggleHud(true);
        bigPanel.infoPanelParent.SetActive(false);
    }

    private void DisplayPassiveRestrictionScreen(DayStateChangePayload payload)
    {
        // check if its day or night
        if (!payload.isDayTime) return;

        InventoryManager invManager = ContextRegister.Instance.GetContext().inventoryManager;

        // check for any new items in inventory
        bool newItemGained = invManager.NewPassiveInPossession();
        if (!newItemGained) return;

        Time.timeScale = 0f;
        nightClearedText.text = $"Night {payload.dayCount - 1} Cleared";
        screenCanvas.SetActive(true);
        HudManager.Instance.ToggleHud(false);
        bigPanel.infoPanelParent.SetActive(false);


        List<ItemInstance> newItems = new List<ItemInstance>();
        List<ItemInstance> ownedItems = new List<ItemInstance>();
        List<ItemInstance> allItems = invManager.GetAllPassiveItems();

        foreach (ItemInstance item in allItems)
        {
            if (item.permanentlyOwned)
                ownedItems.Add(item);
            else
                newItems.Add(item);
        }

        ClearUIOptionsList(newList);
        ClearUIOptionsList(ownedList);

        newList = MakeUIOptionList(newItems, newItemContainer);
        ownedList = MakeUIOptionList(ownedItems, ownedItemContainer);
        
        // disable button component since this list is only for display
        foreach (GameObject item in ownedList)
            item.GetComponentInChildren<Button>().enabled = false;
    }

    private void ClearUIOptionsList(List<GameObject> list)
    {
        foreach (GameObject item in list)
            Destroy(item);
        
        list.Clear();
    }

    private List<GameObject> MakeUIOptionList(List<ItemInstance> items, RectTransform parent)
    {
        List<GameObject> result = new List<GameObject>();
        foreach (ItemInstance item in items)
        {
            GameObject newUIObject = Instantiate(itemOptionPrefab, parent);
            PassiveOptionController controller = newUIObject.GetComponent<PassiveOptionController>();
            controller.Item = item;
            controller.UpdateOptionUI();
            controller.SetNewInfoPanel(bigPanel);
            result.Add(newUIObject);
        }

        return result;
    }
}
