using System.Collections.Generic;
using UnityEngine;

public class ResourceTracker : MonoBehaviour
{
    public static ResourceTracker Instance;

    [SerializeField]
    private List<string> resourceTypes;

    private Dictionary<string, int> resources = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;

        foreach (string type in resourceTypes)
        {
            resources[type] = 0;
        }
    }

    public void SetResource(string name, int value)
    {
        if (!resources.ContainsKey(name))
        {
            Debug.LogError("Unknown Resource Passed. Check Resource Type in Pickup Scriptable Object for " + name);
            return;
        }
        resources[name] += value;
        HudManager.Instance.resources.UpdateResourceText(name, resources[name]);
    }

    public int GetResource(string name)
    {
        return resources[name];
    }
}