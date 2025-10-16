using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    static public void SpawnPickup(ItemDrop drop, Vector3 position)
    {
        int count = Random.Range(drop.minAmount, drop.maxAmount + 1);

        for (int i = 0; i < count; i++)
        {
            GameObject newPickup= Instantiate(drop.data.prefab, position, Quaternion.identity);
            
            Pickup pickupComponent = newPickup.GetComponent<Pickup>();
            pickupComponent.SetPickupData(drop.data);
            pickupComponent.InitializeData();
            pickupComponent.LaunchPickup();
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
                    SpawnPickup(drop, position);
                }
            }
        }
    }

    static public void RollForItemDrop(ItemDrop drop, Vector3 position)
    {
        float roll = Random.Range(0f, 100f);
        if (roll <= drop.dropChance)
        {
            SpawnPickup(drop, position);
        }
    }
}
