using UnityEngine;

public class SimpleMenuInput : MonoBehaviour
{
    [SerializeField] private MenuController controller;

    private Invoker invoker;

    void Start()
    {
        if (!controller)
            controller = FindFirstObjectByType<MenuController>();

        invoker = gameObject.AddComponent<Invoker>();
    }

    void Update()
    {
        // Get Pause Input
        if (Input.GetKeyDown(KeyCode.Tab))
            invoker.ExecuteCommand(new PauseCommand(controller));
        if (Input.GetKeyDown(KeyCode.Escape))
            invoker.ExecuteCommand(new PauseCommand(controller));
    }

    private void OnDisable()
    {
        if (controller == null) return;

        controller.enabled = false;

        if (invoker != null)
            invoker.enabled = false;
    }

    private void OnEnable()
    {
        if (controller == null) return;

        controller.enabled = true;

        if (invoker != null)
            invoker.enabled = true;
    }
}
