using UnityEngine;

public class OneTimeSpawn : MonoBehaviour
{
    [SerializeField]
    private ItemDrop itemToSpawn;

    void Start()
    {
        PickupSpawner.SpawnPickup(itemToSpawn, transform.position);        
    }
}
