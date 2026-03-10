using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void PauseGame()
    {
        bool isPaused = PauseManager.instance.isPaused;
        PauseManager.instance.PauseGame(!isPaused);
    }
}
