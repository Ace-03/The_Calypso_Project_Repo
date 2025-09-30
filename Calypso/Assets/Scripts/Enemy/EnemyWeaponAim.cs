using System;
using UnityEngine;

public class EnemyWeaponAim : MonoBehaviour
{
    [SerializeField]
    private WeaponController weapon;
    private float aimSpeed;

    private void FixedUpdate()
    {
        AimAtPlayer();
    }

    private void AimAtPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Could Not Find Player In Enemy Weapon Aimer");
            return;
        }
        if (weapon == null)
        {
            Debug.LogError("Cuold Not Find Weapon Transform In Enemy Weapon Aimer");
            return;
        }

        Transform weaponPivot = weapon.weaponPivot;

        Vector3 directionToPlayer = (player.transform.position - weaponPivot.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer) * Quaternion.Euler(0, -90, 0);
        weaponPivot.rotation = Quaternion.Slerp(weaponPivot.rotation, targetRotation, Time.deltaTime * aimSpeed);
    }

    internal void Initialize(WeaponController enemyWeapon, float newAimSpeed)
    {
        Debug.Log("In Weapon Aimer: " + enemyWeapon);
        weapon = enemyWeapon;
        Debug.Log("In Weapon Aimer the weapon is: " + weapon);
        aimSpeed = newAimSpeed;

    }
}
