using UnityEngine;

public class SimpleInputManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private AimType aimMethod;

    private Vector3 persistentAim = new Vector3();

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

        Vector3 dir = new Vector3();

        switch (aimMethod) 
        {
            case AimType.mouse:
                dir = Input.mousePosition - Camera.main.WorldToScreenPoint(player.transform.position);
                break;
            case AimType.arrows:
                if (Input.GetKey(KeyCode.UpArrow))
                    dir += Vector3.up;
                if (Input.GetKey(KeyCode.DownArrow))
                    dir += Vector3.down;
                if (Input.GetKey(KeyCode.LeftArrow))
                    dir += Vector3.left;
                if (Input.GetKey(KeyCode.RightArrow))
                    dir += Vector3.right;
                break;
            case AimType.withMovement:
                dir = new Vector3(moveInput.x, moveInput.z, 0);
                break;
        }

        if (dir.magnitude <= 0.1f)
            dir = persistentAim;
        else
            persistentAim = dir;

        invoker.ExecuteCommand(new AimCommand(player, dir.normalized));
    }

    private enum AimType
    {
        mouse,
        arrows,
        withMovement,
    }

    private void OnDisable()
    {
        if (player == null) return;

        player.enabled = false;
    }

    private void OnEnable()
    {
        if (player == null) return;
        
        player.enabled = true;
    }
}
