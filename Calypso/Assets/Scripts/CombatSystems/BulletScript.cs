using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

[RequireComponent(typeof(BulletTrigger))]
[RequireComponent(typeof(ParticleSystem))]
public class BulletScript : MonoBehaviour
{
    BulletTrigger bulletTrigger;
    protected ParticleSystem ps;
    public TEAM team;
    public bool pierce;
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        bulletTrigger = GetComponent<BulletTrigger>();
        UpdateTeam();

        if (transform.parent.CompareTag("Player"))
            SetTeam(TEAM.Player);
        else if (transform.parent.CompareTag("Enemy"))
            SetTeam(TEAM.Enemy);
        if(pierce)
            SetTeam(TEAM.PlayerPierce);
    }

    public void UpdateTeam()
    {
        var col = ps.collision;
        switch (team)
        {
            case TEAM.Player:
                col.collidesWith = LayerMask.GetMask("Enemy", "Environment");
                break;
            case TEAM.Enemy:
                col.collidesWith = LayerMask.GetMask("Player", "Environment");
                break;
            default:
                col.collidesWith = LayerMask.GetMask("Environment");
                break;
        }
    }
    public void SetTeam(TEAM t)
    {
        team = t;
        UpdateTeam();
    }
    public virtual void Attack(WeaponController weapon)
    {
        if (ps != null && !ps.isPlaying)
            ps.Play();
    }
    public virtual void ApplyWeaponStats(WeaponController weapon)
    {
        StopAttack();
        Debug.LogError("Base Bullet Script Can't Apply Weapon Stats to: " + weapon.gameObject.name);
    }
    public virtual bool IsAimable()
    {
        return true;
    }
    public virtual void StopAttack()
    {
        if (ps != null && ps.isPlaying)
            ps.Stop();
    }
}
public enum TEAM
{
    Player,
    Enemy,
    PlayerPierce
}
