using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    public ResourceUIElements resourceElements;
    public void SetElements(ResourceUIElements elements)
    {
        resourceElements = elements;
    }
    public void UpdateResourceText(string name, int value)
    {
        if (name == "stone")
        {
            resourceElements.stoneText.text = $"{name}: " + value.ToString();
        }
        else if (name == "iron")
        {
            resourceElements.ironText.text = $"{name}: " + value.ToString();
        }
    }

    public void UpdateBoatIcons(int count)
    {
        ClearBoats();

        for (int i = 0; i < count; i++)
        {
            Debug.Log("Updating Sprite on index " + i + " for:" + resourceElements.boatIcons[i].name);
            resourceElements.boatIcons[i].sprite = resourceElements.aquiredBoatSprite;
        }
    }

    public void ClearBoats()
    {
        foreach (Image icon in resourceElements.boatIcons)
        {
            icon.sprite = resourceElements.EmptyBoatSprite;
        }
    }

    public void ToggleIconVisibility(int index, bool toggle)
    {
        resourceElements.boatIcons[index].gameObject.SetActive(toggle);
    }
}
