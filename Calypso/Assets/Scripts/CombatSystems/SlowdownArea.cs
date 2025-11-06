using UnityEngine;

public class SlowdownArea : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StatusSystem enemyStatusComponent = other.GetComponent<StatusSystem>();
            enemyStatusComponent.ApplySlowdown(10f * Time.deltaTime);
        }
    }
}
