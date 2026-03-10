using UnityEngine;

public class BaseInteractable : MonoBehaviour, IInteractable
{
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Interact()
    {
        BaseUI baseMenu = BaseUI.Instance;

        if (baseMenu.isOpen == false)
            baseMenu.ToggleBaseMenu(true);
    }

}
