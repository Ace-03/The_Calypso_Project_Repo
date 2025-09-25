using UnityEngine;

public static class BurstModifier
{
    public static void SetTime(ParticleSystem ps, int index, float time)
    {
        var emission = ps.emission;
        if (emission.burstCount <= 0 && index >= emission.burstCount)
        {
            Debug.LogError("Burst Index Out Of Range");
            return;
        }
        ParticleSystem.Burst burst = emission.GetBurst(index);
        burst.time = time;
        emission.SetBurst(index, burst);
    }
    public static void SetCount(ParticleSystem ps, int index, int count)
    {
        var emission = ps.emission;
        if (emission.burstCount <= 0 && index >= emission.burstCount)
        {
            Debug.LogError("Burst Index Out Of Range");
            return;
        }
        ParticleSystem.Burst burst = emission.GetBurst(index);
        burst.count = count;
        emission.SetBurst(index, burst);
    }
    public static void SetCycles(ParticleSystem ps, int index, int cycles)
    {
        var emission = ps.emission;
        if (emission.burstCount <= 0 && index >= emission.burstCount)
        {
            Debug.LogError("Burst Index Out Of Range");
            return;
        }
        ParticleSystem.Burst burst = emission.GetBurst(index);
        burst.cycleCount = cycles;
        emission.SetBurst(index, burst);
    }
    public static void SetInterval(ParticleSystem ps, int index, float interval) 
    {
        var emission = ps.emission;
        if (emission.burstCount <= 0 && index >= emission.burstCount)
        {
            Debug.LogError("Burst Index Out Of Range");
            return;
        }
        ParticleSystem.Burst burst = emission.GetBurst(index);
        burst.repeatInterval = interval;
        emission.SetBurst(index, burst);
    }
    public static void SetProbability(ParticleSystem ps, int index, float probability)
    {
        var emission = ps.emission;
        if (emission.burstCount <= 0 && index >= emission.burstCount)
        {
            Debug.LogError("Burst Index Out Of Range");
            return;
        }
        ParticleSystem.Burst burst = emission.GetBurst(index);
        burst.probability = probability;
        emission.SetBurst(index, burst);
    }
}
