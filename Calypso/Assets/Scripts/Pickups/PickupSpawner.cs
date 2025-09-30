using UnityEngine;
using static EnemyDefinitionSO;

public class PickupSpawner : MonoBehaviour
{
    static public void SpawnPickup(ItemDrop Drop, Vector3 position)
    {
        Debug.Log(position);
        int count = Random.Range(Drop.minAmount, Drop.maxAmount + 1);

        for (int i = 0; i < count; i++)
        {
            GameObject newPickup= Instantiate(Drop.data.prefab, position, Quaternion.identity);
            
            Pickup pickupComponent = newPickup.GetComponent<Pickup>();
            pickupComponent.SetPickupData(Drop.data);
            pickupComponent.InitializeData();
        }
    }

    static public void RollForItemDrop(EnemyDefinitionSO enemyData, Vector3 position)
    {
        if (enemyData.possibleDrops != null)
        {
            foreach (var drop in enemyData.possibleDrops)
            {
                float roll = Random.Range(0f, 100f);
                if (roll <= drop.dropChance)
                {
                    PickupSpawner.SpawnPickup(drop, position);
                }
            }
        }
    }
}
