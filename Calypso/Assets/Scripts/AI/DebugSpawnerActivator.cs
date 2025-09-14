using UnityEngine;

public class DebugSpawnerActivator : MonoBehaviour
{
    public KeyCode key;
    public Spawner spawner;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawner != null)
        {
            if (Input.GetKeyDown(key))
            {
                spawner.Spawn();
            }
        }
    }
}
