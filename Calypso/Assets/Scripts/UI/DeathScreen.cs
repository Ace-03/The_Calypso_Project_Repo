using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private OnRespawnPlayerEventSO respawnEvent;
    [SerializeField] private OnDeathEventSO playerDeathEvent;

    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject gambleDescriptionPanel;
    [SerializeField] private BaseHealth baseHealth;

    private void OnEnable()
    {
        playerDeathEvent.RegisterListener(ShowDeathScreen);
    }

    private void OnDisable()
    {
        playerDeathEvent.UnregisterListener(ShowDeathScreen);
    }

    public void OnRespawnButton()
    {
        RaiseEvent(false);
    }

    public void OnGambleButton()
    {
        RaiseEvent(true);
    }

    public void OnGambleHoverEnter()
    {
        gambleDescriptionPanel.SetActive(true);
    }

    public void OnGambleHoverExit()
    {
        gambleDescriptionPanel.SetActive(false);
    }

    private void RaiseEvent(bool gambling)
    {
        RespawnScenePayload payload = new RespawnScenePayload();

        payload.gambleResources = gambling;
        payload.calculatedBaseDamage = (int)RespawnHandler.Instance.CalculateBaseDamage();


        gambleDescriptionPanel.SetActive(false);
        HudManager.Instance.ToggleHud(true);
        deathScreen.SetActive(false);
        respawnEvent.Raise(payload);
    }

    private void ShowDeathScreen(DeathPayload payload)
    {
        HudManager.Instance.ToggleHud(false);

        if (baseHealth.projectBaseDamage((int)RespawnHandler.Instance.CalculateBaseDamage()))
        {
            HudManager.Instance.StartGameOver();
            return;
        }

        deathScreen.SetActive(true);
    }
}
