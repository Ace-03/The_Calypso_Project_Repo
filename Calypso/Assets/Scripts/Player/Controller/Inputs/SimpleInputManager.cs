using UnityEngine;

public class SimpleInputManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    private Invoker invoker;

    void Start()
    {
        if (!player)
            player = FindFirstObjectByType<PlayerController>();

        invoker = gameObject.AddComponent<Invoker>();
    }

    void Update()
    {
        Vector3 moveInput = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            moveInput += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            moveInput += Vector3.back;
        if (Input.GetKey(KeyCode.A))
            moveInput += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            moveInput += Vector3.right;

        if (Input.GetKey(KeyCode.Space))
            invoker.ExecuteCommand(new InteractCommand(player));

        moveInput.Normalize();

        invoker.ExecuteCommand(new MoveCommand(player, moveInput));
    }
}
