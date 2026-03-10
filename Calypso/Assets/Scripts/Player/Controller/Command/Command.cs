using UnityEngine;
public abstract class Command
{
    public abstract void Execute();
}

public abstract class PlayerCommand : Command
{
    public PlayerController controller;
}

public abstract class MenuCommand : Command
{
    public MenuController controller;
}

public class MoveCommand : PlayerCommand
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

public class InteractCommand : PlayerCommand
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

public class AimCommand : PlayerCommand
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

public class PauseCommand : MenuCommand
{
    public PauseCommand(MenuController targetController)
    {
        controller = targetController;
    }
    public override void Execute()
    {
        controller.PauseGame();
    }
}