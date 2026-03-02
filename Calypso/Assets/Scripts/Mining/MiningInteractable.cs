using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MiningParticleController))]
public class MiningInteractable : MonoBehaviour, IInteractable
{
    [Header("Mining Config")]
    [SerializeField] private ItemDrop resourceDrop;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float mineTime = 2f;
    [SerializeField] private float Maxhp = 3;
    [SerializeField] private GameObject model;

    [Header("Particle Config")]
    [SerializeField] private List<Transform> particlePositions = new List<Transform>();

    [Tooltip("Higher Rate means more particles")]
    public float particleRate = 20f;

    [Header("Event Object")]
    [SerializeField] private OnGenericEventSO clearPlayerInteractableEvent;

    private float hp = 1;
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
        particleController.AddPosition(particlePositions);
        hp = Maxhp;
    }

    public void Interact()
    {
        if (mining)
        {
            Debug.Log("attempting to mine");
            return;
        }

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
        else
        {
            model.transform.localScale = Vector3.one * (hp / Maxhp);
        }
    }

    private void DepleteNode()
    {
        Invoke(nameof(RaiseEvent), 0.06f);

        gameObject.SetActive(false);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    private void RaiseEvent()
    {
        clearPlayerInteractableEvent.Raise(new GameEventPayload());
    }
}
