using UnityEngine;

public class PassiveOptionController : MonoBehaviour
{
    [HideInInspector] public ItemInstance Item;
    [SerializeField] private OnGenericEventSO itemSelectedEvent;

    [Header("Components")]
    public OptionUIComponents OptionUI;

    public void UpdateOptionUI()
    {
        string upgradeInfo = GetStatInfo(Item);

        OptionUI.iconImage.sprite = Item.itemData.sprite;
        SpriteNormalizer.NormalizeImage(OptionUI.iconImage.gameObject);
        OptionUI.InfoPanel.itemName.text = Item.itemData.itemName;
        OptionUI.InfoPanel.itemDescription.text = Item.itemData.description;
        OptionUI.InfoPanel.statInfo.text = upgradeInfo;
    }

    public void OnOptionSelected()
    {
        Item.permanentlyOwned = true;
        itemSelectedEvent.Raise(new GameEventPayload());
    }

    public void OnDisplayInfoPanel()
    {
        UpdateOptionUI();
        OptionUI.InfoPanel.infoPanelParent.SetActive(true);
    }

    public void OnHideInfoPanel()
    {
        OptionUI.InfoPanel.infoPanelParent.SetActive(false);
    }

    public void SetNewInfoPanel(OptionInfoPanelComponents newPanel)
    {
        OptionUI.InfoPanel = newPanel;
    }

    private string GetStatInfo(ItemInstance item)
    {
        string upgradeInfo = "";

        foreach (StatModifier stat in item.modifiers)
        {
            string applicationType = "+";
            string unitType = "";

            if (stat.ModType == StatModifierType.MultPercentage ||
                stat.ModType == StatModifierType.AdditivePercentage)
            {
                unitType = "%";

                if (stat.ModType == StatModifierType.MultPercentage)
                {
                    applicationType = "X";
                }
            }

            string value = $"{applicationType}{stat.Value.ToString("F1")}{unitType}";

            upgradeInfo += $"{stat.StatType}: {value}\n";
        }

        return upgradeInfo;
    }
}
