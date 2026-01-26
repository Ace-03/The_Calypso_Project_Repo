using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void LoadTitleScreen()
    {
        // assumes title screen will be zero
        SceneManager.LoadScene(0);
    }
}
