using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [HideInInspector] public bool isPaused = false;

    public static PauseManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void PauseGame(bool toggle)
    {
        Time.timeScale = toggle ? 0 : 1;
        pauseUI.SetActive(toggle);
        isPaused = toggle;

        PlayerManager.Instance.ToggleMovement(!toggle);
    }

    public void GoToTitle()
    {
        Application.Quit();
    }
}
