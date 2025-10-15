using UnityEngine;

public class ResourceTracker : MonoBehaviour
{
    public static ResourceTracker Instance;

    [SerializeField]
    private int iron;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    public void AddIron(int amount)
    {
        iron += amount;
    }

    public void RemoveIron(int ammount)
    {
        iron -= ammount;
    }
}
