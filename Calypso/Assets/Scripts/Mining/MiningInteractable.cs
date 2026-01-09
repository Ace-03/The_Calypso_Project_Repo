using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningInteractable : MonoBehaviour, IInteractable
{
    [Header("Mining Config")]
    [SerializeField] private ItemDrop resourceDrop;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float mineTime = 2f;
    [SerializeField] private int hp = 3;

    [Header("Particle Config")]
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private List<Transform> particlePositions = new List<Transform>();

    [Tooltip("Higher Rate means more particles")]
    public float particleRate = 20f;

    [Header("Event Object")]
    [SerializeField] private OnGenericEventSO clearPlayerInteractableEvent;


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
        --hp;

        if (hp <= 0)
        {
            DepleteNode();
        }
    }

    private void DepleteNode()
    {
        clearPlayerInteractableEvent.Raise(new GameEventPayload());
        gameObject.SetActive(false);
    }
}
