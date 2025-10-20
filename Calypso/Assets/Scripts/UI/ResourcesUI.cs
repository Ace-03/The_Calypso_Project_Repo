using UnityEngine;

public class ResourcesUI : MonoBehaviour
{
    public ResourceUIElements resourceElements;
    public void SetElements(ResourceUIElements elements)
    {
        resourceElements = elements;
    }
    public void UpdateResourceText(int resources)
    {
        resourceElements.resourceText.text = resources.ToString();
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
