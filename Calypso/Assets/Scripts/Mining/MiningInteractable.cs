using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningInteractable : MonoBehaviour, IInteractable
{
    [Header("Mining Config")]
    public ItemDrop resourceDrop;
    public Transform spawnPoint;
    public float mineTime = 2f;

    [Header("Particle Config")]
    public GameObject particlePrefab;
    public List<Transform> particlePositions = new List<Transform>();
    public Color particleColor = Color.grey;

    [Tooltip("Higher Rate means more particles")]
    public float particleRate = 20f;


    private float currentMineTimer = 0f;
    private bool mining = false;
    private MiningParticleController particleController;

    private void Awake()
    {
        if (!TryGetComponent<MiningParticleController>(out particleController))
        {
            particleController = gameObject.AddComponent<MiningParticleController>();
        }

        particleRate = Mathf.Clamp(particleRate, 0f, 5000f);
        particleController.SetPrefab(particlePrefab);
        particleController.SetColor(particleColor);
        particleController.AddPosition(particlePositions);
    }

    public void Interact()
    {
        if (mining) return;

        Debug.Log("Started Mining...");
        mining = true;
        StartCoroutine(MineResource());
        PlayerManager.Instance.ToggleMovement(false);
        PlayerManager.Instance.ToggleWeapons(false);
    }


    private IEnumerator MineResource()
    {
        while (mining)
        {
            particleController.RollForParticle(particleRate);

            currentMineTimer += Time.deltaTime;
            if (currentMineTimer >= mineTime)
            {
                Debug.Log("Resource Mined!");

                PickupSpawner.SpawnPickup(resourceDrop, spawnPoint.position);

                mining = false;
            }
            yield return new WaitForFixedUpdate();
        }

        PlayerManager.Instance.ToggleMovement(true);
        PlayerManager.Instance.ToggleWeapons(true);
        currentMineTimer = 0f;
    }
}
