using UnityEngine;
using UnityEngine.AI;

public class AI_NAV : MonoBehaviour
{
    public Transform target;   // The object to move toward
    public float speed = 3.5f;
    public float stopDistance = 3f;
    public bool stopAtDistance = true;
    private NavMeshAgent agent;

    void Start()
    {
        // Get the NavMeshAgent attached to this object
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            agent.stoppingDistance = stopDistance;
            agent.speed = speed;
            float distance = Vector3.Distance(transform.position, target.position);
            agent.SetDestination(target.position);
        }
        else
        {
            target = FindAnyObjectByType<Target>().transform;
        }
    }
}
