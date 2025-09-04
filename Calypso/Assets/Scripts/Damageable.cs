using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    public int hp = 0;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("DIED");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }
}
