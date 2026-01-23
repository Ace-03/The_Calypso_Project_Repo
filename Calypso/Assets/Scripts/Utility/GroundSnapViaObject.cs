using UnityEngine;

public class GroundSnapViaObject : MonoBehaviour
{
    public Transform master;
    public Transform slave;
    public float maxDistance;
    public LayerMask groundLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slave.position = master.position;
    }

    // Update is called once per frame
    void Update()
    {
        slave.position = new Vector3(master.position.x, 0, master.position.z);
    }
}
