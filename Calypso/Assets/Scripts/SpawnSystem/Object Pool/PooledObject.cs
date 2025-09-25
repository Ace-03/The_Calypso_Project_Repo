using UnityEngine;

public class PooledObject : MonoBehaviour
{
    // The name of the pool this object belongs to.
    [HideInInspector] public string poolName;

    public void DeactivateAndReturn()
    {
        PoolManager.Instance.ReturnToPool(poolName, this.gameObject);
    }
}