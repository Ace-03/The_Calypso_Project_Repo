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

    public void SetIron(int amount)
    {
        iron += amount;
        HudManager.Instance.resources.UpdateResourceText(iron);
    }
}
