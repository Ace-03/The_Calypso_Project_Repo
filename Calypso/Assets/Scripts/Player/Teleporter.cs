using UnityEngine;

public class Teleporter : MonoBehaviour
{
    Rigidbody rb;
    public KeyCode teleportHomeButton = KeyCode.H;
    public Transform homeWaypoint;
    public float verticleOffset;
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
        if (Input.GetKeyUp(teleportHomeButton))
        {
            TeleportHome();
        }
    }

    void TeleportHome()
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
