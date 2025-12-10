using System;
using UnityEngine;

public class EnemyWeaponAim : MonoBehaviour
{
    [SerializeField]
    private WeaponController weapon;
    private Transform playerTransform;
    private float aimSpeed;

    private void FixedUpdate()
    {
        AimAtPlayer();
    }

    private void AimAtPlayer()
    {
        if (weapon == null)
        {
            Debug.LogError("Cuold Not Find Weapon Transform In Enemy Weapon Aimer");
            return;
        }

        Vector3 directionToPlayer = (playerTransform.position - weapon.weaponPivot.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer) * Quaternion.Euler(0, -90, 0);
        weapon.weaponPivot.rotation = Quaternion.Slerp(weapon.weaponPivot.rotation, targetRotation, Time.deltaTime * aimSpeed);
    }

    internal void Initialize(WeaponController enemyWeapon, float newAimSpeed)
    {
        weapon = enemyWeapon;
        aimSpeed = newAimSpeed;
        playerTransform = ContextRegister.Instance.GetContext().playerTransform;
    }
}
