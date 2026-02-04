using System.Collections.Generic;
using UnityEngine;

public class MiningParticleController : MonoBehaviour
{
    [SerializeField] private Color m_Color;
    [SerializeField] private GameObject particlePrefab;
    private ParticleSystem ps;

    private List<Vector3> positions = new List<Vector3>();

    private void Start()
    {
        SetPrefab(particlePrefab, m_Color);
    }

    public void RollForParticle(float rate)
    {
        float roll = Random.Range(0f, 5000f);

        if (roll <= rate)
        {
            SpawnParticle();
        }
    }

    private void SpawnParticle()
    {
        Destroy(Instantiate(particlePrefab, GetPosition(), Quaternion.identity), 10f);
    }
    
    public void SetPrefab(GameObject prefab, Color color)
    {
        TryGetParticleSystem(prefab);
        var main = ps.GetComponent<ParticleSystem>().main;
        main.startColor = color;
    }

    public void SetColor(Color color)
    {
        if (ps == null)
        {
            TryGetParticleSystem(particlePrefab);
        }
        GeneralModifier.SetColor(ps, color);
    }

    public void AddPosition(Vector3 position)
    {
        positions.Add(position);
    }

    public void AddPosition(List<Vector3> positions)
    {
        this.positions.AddRange(positions);
    }

    public void AddPosition(Transform transform)
    {
        positions.Add(transform.position);
    }

    public void AddPosition(List<Transform> transforms)
    {
        foreach (Transform t in transforms)
        {
            positions.Add(t.position);
        }
    }

    private Vector3 GetPosition()
    {
        return positions[Random.Range(0, positions.Count)];
    }

    private void TryGetParticleSystem(GameObject prefab)
    {
        if (!prefab.TryGetComponent<ParticleSystem>(out ps))
        {
            ps = prefab.GetComponentInChildren<ParticleSystem>();
        }
    }
}
