using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }
}
