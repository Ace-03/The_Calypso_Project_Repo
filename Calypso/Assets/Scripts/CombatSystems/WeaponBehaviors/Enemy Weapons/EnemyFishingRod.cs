using UnityEngine;
using UnityEngine.AI;

public class EnemyFishingRod : FishingRodBehavior
{
    protected override bool GetDirection()
    {
        NavMeshAgent agent = GetComponentInParent<NavMeshAgent>();

        if (agent != null)
            Debug.LogError("Could not find Nav Mesh Agent on fishing rod enemy");

        if (agent.velocity.x > 0)
            return true;
        else
            return false;
    }
}
