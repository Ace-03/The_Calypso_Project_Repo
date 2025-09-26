using UnityEngine;
using UnityEngine.AI;

public class AI_NAV : MonoBehaviour, IEnemyMovement
{
    public Transform target;   // The object to move toward
    private float maxSpeed;
    public float speed = 3.5f;
    public float stopDistance = 3f;
    public bool stopAtDistance = true;
    private NavMeshAgent agent;

    void Awake()
    {
        maxSpeed = speed;
        // Get the NavMeshAgent attached to this object
        agent = GetComponent<NavMeshAgent>();
        target = FindAnyObjectByType<PlayerController>().transform;

        agent.updateRotation = false;
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
            target = FindAnyObjectByType<PlayerController>().transform;
        }
    }

    public void SetSpeed(float newSpeed)
    {
        maxSpeed = newSpeed;
        agent.speed = newSpeed;
    }
    
    public void ResetSpeed()
    {
        speed = maxSpeed;

    }
    
    public float GetMaxSpeed()
    {
        return maxSpeed;
    }
}
