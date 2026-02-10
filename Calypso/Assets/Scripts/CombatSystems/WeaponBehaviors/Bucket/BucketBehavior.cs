using UnityEngine;

public class BucketBehavior : MonoBehaviour, IWeaponBehavior
{
    [Header("Components")]
    [SerializeField] private float TargetSizeModifier;
    [SerializeField] private GameObject waterPoolPrefab;

    private float maxSize;
    private float poolLifeTime;

    private void SpawnPool(float duration, float targetSize, WeaponController weapon)
    {
        GameObject newObject = Instantiate(waterPoolPrefab, transform.position + Vector3.down, Quaternion.identity);
        BulletTrigger bt = newObject.GetComponentInChildren<BulletTrigger>();
        PoolSizeController poolController = newObject.GetComponent<PoolSizeController>();
        Collider col = newObject.GetComponentInChildren<Collider>();

        bt.SetDamageSource(weapon.GetDamageSource());
        GeneralModifier.UpdateCollisionLayers(col, weapon.team);
        poolController.InitializePool(duration, targetSize);

        Debug.Log("Spawning New Pool");
        Debug.Log($"name of pool is {newObject.name}");
    }

    public void ApplyWeaponStats(WeaponController weapon)
    {
        maxSize = weapon.GetArea() * TargetSizeModifier;
        poolLifeTime = weapon.GetDuration();
    }

    public bool IsAimable() => false;

    public void Attack(WeaponController weapon) =>
        SpawnPool(poolLifeTime, maxSize, weapon);
}
