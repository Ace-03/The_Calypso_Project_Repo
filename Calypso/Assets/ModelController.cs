using UnityEngine;

public class ModelController : MonoBehaviour
{
    [SerializeField] private GameObject model;
    public GameObject GetModel()
    {
        return model;
    }
}
