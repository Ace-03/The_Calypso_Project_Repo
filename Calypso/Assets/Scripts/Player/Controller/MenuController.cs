using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void PauseGame()
    {
        bool isPaused = PauseManager.Instance.isPaused;
        PauseManager.Instance.PauseGame(!isPaused);
    }
}
