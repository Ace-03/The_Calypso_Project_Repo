using UnityEngine;

public static class GeneralModifier
{
    public static void SetDuration(ParticleSystem ps, float duration)
    {
        var Main = ps.main;
        Main.duration = duration;
    }
    public static void SetLifetime(ParticleSystem ps, float lifetime)
    {
        var Main = ps.main;
        Main.startLifetime = lifetime;
    }
    public static void SetSpeed(ParticleSystem ps, float speed)
    {
        var Main = ps.main;
        Main.startSpeed = speed;
    }
    public static void SetSimulationSpeed(ParticleSystem ps, float speed)
    {
        var Main = ps.main;
        Main.simulationSpeed = speed;
    }
    public static void SetStartSpeed(ParticleSystem ps, float speed)
    {
        var Main = ps.main;
        Main.startSpeed = speed;
    }
    public static void SetRateOverTime(ParticleSystem ps, float rate)
    {
        var emission = ps.emission;
        emission.rateOverTime = rate;
    }

    public static void SetCircleArc(ParticleSystem ps, float arcSize)
    {
        var Shape = ps.shape;
        Shape.arc = arcSize;
        Shape.rotation = new Vector3(Shape.rotation.x, arcSize/2, Shape.rotation.z);
    }
}
