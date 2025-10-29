using UnityEngine;

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

    public void UpdateBoatIcon(int index, Sprite sprite)
    {
        resourceElements.boatIcons[index].sprite = sprite;
    }

    public void ToggleIconVisibility(int index, bool toggle)
    {
        resourceElements.boatIcons[index].gameObject.SetActive(toggle);
    }
}
