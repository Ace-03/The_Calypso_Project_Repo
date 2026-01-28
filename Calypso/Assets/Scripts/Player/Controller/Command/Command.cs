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

public class AimCommand : Command
{
    private Vector3 aimDirection;
    public AimCommand(PlayerController targetController, Vector3 direction)
    {
        controller = targetController;
        aimDirection = direction;
    }
    public override void Execute()
    {
        controller.SetAimVector(aimDirection);
    }
}

public class PauseCommand : Command
{
    public PauseCommand(PlayerController targetController)
    {
        controller = targetController;
    }
    public override void Execute()
    {
        controller.PauseGame();
    }
}