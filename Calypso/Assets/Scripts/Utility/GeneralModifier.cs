using UnityEngine;

public static class GeneralModifier
{
    public static void SetColor(ParticleSystem ps, Color color)
    {
 ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var Main = ps.main;
        Main.startColor = color;
        ps.Play();
    }

    public static void SetSprite(ParticleSystem ps, Material sprite)
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var renderer = ps.GetComponent<ParticleSystemRenderer>();
        renderer.material = sprite;
        ps.Play();
    }
    public static void SetDuration(ParticleSystem ps, float duration)
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var Main = ps.main;
        Main.duration = duration;
        ps.Play();
    }
    public static void SetLifetime(ParticleSystem ps, float lifetime)
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var Main = ps.main;
        Main.startLifetime = lifetime;
        ps.Play();
    }
    public static void SetSpeed(ParticleSystem ps, float speed)
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var Main = ps.main;
        Main.startSpeed = speed;
        ps.Play();
    }
    public static void SetSimulationSpeed(ParticleSystem ps, float speed)
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var Main = ps.main;
        Main.simulationSpeed = speed;
        ps.Play();
    }
    public static void SetStartSpeed(ParticleSystem ps, float speed)
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var Main = ps.main;
        Main.startSpeed = speed;
        ps.Play();
    }
    public static void SetRateOverTime(ParticleSystem ps, float rate)
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var emission = ps.emission;
        emission.rateOverTime = rate;
        ps.Play();
    }

    public static void SetCircleArc(ParticleSystem ps, float arcSize)
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var Shape = ps.shape;
        Shape.arc = arcSize;
        Shape.rotation = new Vector3(Shape.rotation.x, arcSize/2, Shape.rotation.z);
        ps.Play();
    }

    public static void UpdateCollisionLayers(ParticleSystem ps, TEAM team, DamageSource src = null)
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
                PierceHandler prc = ps.gameObject.AddComponent<PierceHandler>();
                prc.SetDamageSource(src);
                break;
        }
    }

    public static void UpdateCollisionLayers(Collider col, TEAM team)
    {
        switch (team)
        {
            case TEAM.Player:
                col.includeLayers = LayerMask.GetMask("Enemy", "Environment");
                col.excludeLayers = LayerMask.GetMask("Player");
                break;
            case TEAM.Enemy:
                col.includeLayers = LayerMask.GetMask("Player", "Environment");
                col.excludeLayers = LayerMask.GetMask("Enemy");
                break;
            default:
                col.includeLayers = LayerMask.GetMask("Enemy", "Player", "Environment");
                break;
        }
    }
}
