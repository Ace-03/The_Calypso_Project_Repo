using UnityEngine;

public class ScaleViaHeight : MonoBehaviour
{
    [SerializeField] private float groundHeight = 0;
    [SerializeField] private float maxHeight = 25f;
    [SerializeField] private Vector3 startSize = Vector3.zero;
    [SerializeField] private Vector3 endSize = new Vector3(2,2,2);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = startSize;
    }

    // Update is called once per frame
    void Update()
    {
        float currentHeightNormalized = (transform.position.y - groundHeight) / maxHeight;
        if (currentHeightNormalized > 1)
        {
            currentHeightNormalized = 1;
        }
        if(currentHeightNormalized < 0)
        {
            currentHeightNormalized = 0;
        }
        float x = Mathf.Lerp(startSize.x, endSize.x, 1 - currentHeightNormalized);
        float y = Mathf.Lerp(startSize.y, endSize.y, 1 - currentHeightNormalized);
        float z = Mathf.Lerp(startSize.z, endSize.z, 1 - currentHeightNormalized);
        transform.localScale = new Vector3(x, 0.01f, z);
    }
}
