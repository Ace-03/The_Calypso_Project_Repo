using UnityEngine;

public class Teleporter : MonoBehaviour
{
    Rigidbody rb;
    public KeyCode teleportHomeButton = KeyCode.H;
    public Transform homeWaypoint;
    public float verticleOffset;
    public float teleportDelay = 1f;
    float teleportTimer = 0f;
    public bool teleporting = false;
    public ParticleSystem teleParticles;
    Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = Vector3.zero;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(teleportHomeButton) && !teleporting)
        {
            StartTimer();
        }
        UpdateTimer();
    }
    void StartTimer()
    {
        teleportTimer = teleportDelay;
        teleporting = true;
        if (teleParticles != null) teleParticles.Play();
    }
    void UpdateTimer()
    {
        if (teleporting && teleportTimer > 0)
        {
            teleportTimer -= Time.deltaTime;
            if (teleportTimer <= 0)
            {
                TeleportHome();
                if(teleParticles  != null) teleParticles.Pause();
                teleporting = false;
            }
        }
        if(teleportDelay == 0f && teleporting)
        {
            teleportTimer = 0.25f;
        }
    }

    public void TeleportHome()
    {
        offset.y = verticleOffset;
        if (homeWaypoint != null)
        {
            rb.position = homeWaypoint.position;
        }
        else
        {
            Debug.LogError("Player does not have a Home Waypoint set");
        }
    }
}
