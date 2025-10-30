using UnityEngine;

public class BaseInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private BaseUI baseMenu;
    public void Interact()
    {
        if (baseMenu.isOpen == false)
            baseMenu.ToggleBaseMenu(true);
    }
}
