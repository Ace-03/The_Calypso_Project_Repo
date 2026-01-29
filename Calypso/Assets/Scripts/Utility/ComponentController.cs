using UnityEngine;

public class ComponentController : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private Collider trigger;
    [SerializeField] private GameObject shadow;
    public GameObject GetModel()
    {
        return model;
    }

    public Collider GetTrigger()
    {
        return trigger;
    }
    public GameObject GetShadow()
    {
        return shadow;
    }
}
