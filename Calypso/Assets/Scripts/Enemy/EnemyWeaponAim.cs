using System;
using UnityEngine;

public class EnemyWeaponAim : MonoBehaviour
{
    [SerializeField]
    private Transform weapon;
    private float aimSpeed;

    private void FixedUpdate()
    {
        AimAtPlayer();
    }

    private void AimAtPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        if (weapon == null) return;

        Vector3 directionToPlayer = (player.transform.position - weapon.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        weapon.rotation = Quaternion.Slerp(weapon.rotation, targetRotation, Time.deltaTime * aimSpeed);
    }

    public void Initialize(Transform weapon, float aimSpeed)
    {
        this.weapon = weapon;
        this.aimSpeed = aimSpeed;
    }

    internal void Initialize(GameObject gameObject, float newAimSpeed)
    {
        weapon = gameObject.transform;
        aimSpeed = newAimSpeed;
    }
}
