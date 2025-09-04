using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public void Spawn()
    {
        Instantiate(prefab);
    }
}
