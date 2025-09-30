using System.Collections.Generic;
using UnityEngine;

public class PropGenerator : MonoBehaviour
{
    [Header("Generation Settings")]
    [Tooltip("Seed for repeatable generation. Change this value to get a new layout.")]
    public int seed = 1000;
    public int numberOfProps = 50;
    public bool giveRandomRotation = false;
    public bool useExclusionZones = false;

    [Header("Generation Bounds (Coordinates)")]
    public Vector3 minBounds = new Vector3(-10f, 0f, -10f);
    public Vector3 maxBounds = new Vector3(10f, 0f, 10f);

    [Header("Prop Settings")]
    public GameObject propPrefab;
    public Transform propContainer;

    public List<Collider> ExclusionZones;

    private Vector3 randomPosition = Vector3.zero;
    private Quaternion givenRotation = Quaternion.identity;

    void Start()
    {
        RegenerateProps();
    }

    private void GenerateProps()
    {
        Random.InitState(seed);

        if (propPrefab == null)
        {
            Debug.LogError("Prop Prefab is not assigned in the Inspector!");
            return;
        }

        for (int i = 0; i < numberOfProps; i++)
        {
            float randomX = Random.Range(minBounds.x, maxBounds.x);
            float randomY = Random.Range(minBounds.y, maxBounds.y);
            float randomZ = Random.Range(minBounds.z, maxBounds.z);

            randomPosition = new Vector3(randomX, randomY, randomZ);
            
            if (giveRandomRotation)
                givenRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            else 
                givenRotation = Quaternion.identity;

            if (useExclusionZones)
            {
                foreach (Collider coll in ExclusionZones)
                {
                    if (!coll.bounds.Contains(randomPosition))
                    {
                        SpawnProp();
                    }
                }

            }
            else
            {
                SpawnProp();
            }
        }

        Debug.Log($"Finished generating {numberOfProps} props with seed: {seed}.");
    }

    [ContextMenu("Regenerate Props")]
    public void RegenerateProps()
    {
        ClearProps();
        GenerateProps();
    }

    [ContextMenu("Clear Props In Editor")]
    public void ClearProps()
    {
        int childCount = propContainer.childCount;
        for (int i = childCount - 1; i >= 0; i--)
            DestroyImmediate(propContainer.GetChild(i).gameObject);
    }

    private void SpawnProp()
    {
        Instantiate(propPrefab, randomPosition, givenRotation, propContainer);

    }
}