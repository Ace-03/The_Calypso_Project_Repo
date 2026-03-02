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

    public void UpdateBoatIcons(Sprite icon)
    {
        Debug.Log("Making New Boat Icon");
        GameObject newIcon = Instantiate(resourceElements.boatIconPrefab, resourceElements.boatIconContainer);
        newIcon.GetComponent<Image>().sprite = icon;
    }
}
