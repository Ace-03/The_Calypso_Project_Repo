using UnityEngine;

public class BucketBehavior : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private float TargetSizeModifier;
    [SerializeField] private GameObject waterPoolPrefab;

    private float maxSize;
    private float poolLifeTime;

    private void SpawnPool(float duration, float targetSize, WeaponController weapon)
    {
        GameObject newObject = Instantiate(waterPoolPrefab, transform.position, Quaternion.identity);
        BulletTrigger bt = newObject.GetComponentInChildren<BulletTrigger>();
        PoolSizeController poolController = GetComponent<PoolSizeController>();

        bt.SetDamageSource(weapon.GetDamageSource());
        poolController.InitializePool(duration, targetSize);
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
