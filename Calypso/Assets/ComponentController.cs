using UnityEngine;

public class ComponentController : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private Collider trigger;
    public GameObject GetModel()
    {
        return model;
    }

    public Collider GetTrigger()
    {
        return trigger;
    }
}
