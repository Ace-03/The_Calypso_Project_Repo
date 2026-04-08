using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MiningParticleController))]
public class MiningInteractable : MonoBehaviour, IInteractable
{
    [Header("Events")]
    [SerializeField] private OnDeathEventSO playerDeathEvent;

    [Header("Mining Config")]
    [SerializeField] private ItemDrop resourceDrop;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float mineTime = 2f;
    private float activeMineTime;
    [SerializeField] private float Maxhp = 3;
    [SerializeField] private List<GameObject> models;

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

    private void OnEnable()
    {
        playerDeathEvent.RegisterListener(CancelMining);
    }

    private void OnDisable()
    {
        playerDeathEvent.UnregisterListener(CancelMining);
    }

    public void Interact()
    {
        if (mining)
        {
            Debug.Log("Mining skipped, already mining");
            return;
        }

        if (gameObject.activeSelf)
        {
            Debug.Log("Started Mining...");
            
            mining = true;

            float dexterity = ContextRegister.Instance.GetContext().statSystem.GetFinalValue(StatType.Dexterity);

            float t = Mathf.InverseLerp(1f, 5, dexterity);
            float biasedT = Mathf.Sqrt(t);
            activeMineTime = Mathf.Lerp(mineTime, 0.001f, biasedT);
            Debug.Log($"mine time is {activeMineTime}");

            StartCoroutine(MineResource());
            PlayerManager.Instance.ToggleMovement(false);
            PlayerManager.Instance.ToggleWeapons(false);
        }
    }


    private IEnumerator MineResource()
    {
        while (mining)
        {
            particleController.RollForParticle(particleRate);

            currentMineTimer += Time.deltaTime;
            if (currentMineTimer >= activeMineTime)
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
            DepleteNode();
        else
            UpdateModel();
    }

    private void UpdateModel()
    {
        float hpPercent = hp / Maxhp;
        int decayStage = models.Count - Mathf.RoundToInt(hpPercent * models.Count);

        SetModel(decayStage);
    }

    private void SetModel(int decayStage)
    {
        foreach (GameObject model in models)
            model.SetActive(false);

        models[decayStage].SetActive(true);  
    }

    private void DepleteNode()
    {
        Invoke(nameof(RaiseEvent), 0.06f);
        StopAllCoroutines();
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

    private void CancelMining(DeathPayload payload) => StopAllCoroutines();
}
