using UnityEngine;

public class ResourceTracker : MonoBehaviour
{
    public static ResourceTracker Instance;

    [SerializeField]
    private int resin;
    [SerializeField]
    private int stone;

    [SerializeField]
    private GameObject stonePrefab;
    [SerializeField]
    private GameObject resinPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddResin(int amount)
    {
        resin += amount;
        Debug.Log("Resin: " + resin);
    }

    public void AddStone(int amount)
    {
        stone += amount;
        Debug.Log("Stone: " + stone);
    }

    public void RemoveResin(int ammount)
    {
        resin -= ammount;
        Debug.Log("Resin: " + resin);
    }

    public void RemoveStone(int ammount)
    {
        stone -= ammount;
        Debug.Log("Stone: " + stone);
    }
}
