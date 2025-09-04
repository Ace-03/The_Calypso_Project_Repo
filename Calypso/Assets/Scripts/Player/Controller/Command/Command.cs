using UnityEngine;
public abstract class Command
{
    public PlayerController controller;

    public abstract void Execute();
}

public class MoveCommand : Command
{
    private Vector3 direction;

    public MoveCommand(PlayerController targetController, Vector3 moveDirection)
    {
        controller = targetController;
        direction = moveDirection;
    }

    public override void Execute()
    {
        controller.SetMovementVector(direction);
    }
}

public class InteractCommand : Command
{
    public InteractCommand(PlayerController targetController)
    {
        controller = targetController;
    }

    public override void Execute()
    {
        controller.Interact();
    }
}