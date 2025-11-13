using UnityEngine;

public class DropOnX : MonoBehaviour
{
    public float dropTime = 10f;
    float timer;
    public float size = 5f;
    public GameObject instantiatable;

    private void Update()
    {
        if (timer <= 0f)
        {
            timer = 10f;
            Drop();
        }
        timer -= Time.deltaTime;
    }
    private void Drop()
    {
        GameObject droppedThing = Instantiate(instantiatable, transform.position, transform.rotation);
        droppedThing.transform.localScale = Vector3.one * size;
    }
}
