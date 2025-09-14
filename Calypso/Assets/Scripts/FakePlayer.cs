using UnityEngine;

public class FakePlayer : MonoBehaviour
{
    public float speed;
    public KeyCode Move_Up = KeyCode.W;
    public KeyCode Move_Left = KeyCode.A;
    public KeyCode Move_Down = KeyCode.S;
    public KeyCode Move_Right = KeyCode.D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float curSpeed = speed * Time.deltaTime;
        if (Input.GetKey(Move_Up))
        {
            transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z + curSpeed);
        }
        if (Input.GetKey(Move_Left))
        {
            transform.position = new Vector3(transform.position.x - curSpeed, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(Move_Down))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - curSpeed);
        }
        if (Input.GetKey(Move_Right))
        {
            transform.position = new Vector3(transform.position.x + curSpeed, transform.position.y, transform.position.z);
        }
    }
}
