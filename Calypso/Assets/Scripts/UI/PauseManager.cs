using UnityEngine;
public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [HideInInspector] public bool isPaused = false;

    public static PauseManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void PauseGame(bool toggle)
    {
        Time.timeScale = toggle ? 0 : 1;
        pauseUI.SetActive(toggle);
        isPaused = toggle;
    }
}
