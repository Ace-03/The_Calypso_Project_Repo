using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] public Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Debug.Log($"{animator.name}");
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //animator.StopPlayback();
    }

    public void PlayAnimation()
    {
        animator.enabled = true;
        animator.Play("Bubble_Animation_02");
    }

    public void ChangeScene() 
    {
        SceneManager.LoadScene(1);
    }
}
