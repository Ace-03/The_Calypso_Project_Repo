using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class DeathScreen : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private OnRespawnPlayerEventSO respawnEvent;
    [SerializeField] private OnDeathEventSO playerDeathEvent;

    [Header("Components")]
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private Image background;
    [SerializeField] private GameObject gambleDescriptionPanel;
    [SerializeField] private TextMeshProUGUI panelText;
    [SerializeField] private BaseHealth baseHealth;
    [SerializeField] private Button gambleButton;

    [Header("Resources/Parameters")]
    [SerializeField] private Sprite defaultBG;
    [SerializeField] private Sprite purpleBG;
    [SerializeField] private string gambleDescriptionMessage = "Wager your resources and bet to keep an item?";
    [SerializeField] private string winMessage = "LUCKY!\nEnjoy Your Reward!";
    [SerializeField] private string loseMessage = "UNLUCKY!\nThanks For All Your Stuff!";
    [SerializeField] private string noItemMessage = "You Can't Gamble if you have no items to keep";
    [SerializeField] private float textAnimWaitInterval;
    [SerializeField] private float postResultWaitPeriod;

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
        StartCoroutine(GambleSequence());
    }

    public void OnGambleHoverEnter()
    {
        gambleDescriptionPanel.SetActive(true);
    }

    public void OnGambleHoverExit()
    {
        gambleDescriptionPanel.SetActive(false);
    }

    private IEnumerator GambleSequence()
    {
        // setup screen gamble state
        background.sprite = purpleBG;
        panelText.text = "";
        ToggleDeathScreenInput(false);
        
        yield return new WaitForSeconds(0.2f);

        gambleDescriptionPanel.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        // Do text Anim
        for (int i = 0; i < 8; i++)
        {
            panelText.text += "O";
            yield return new WaitForSeconds(textAnimWaitInterval);
        }

        // run logic and display success flag
        bool success = RespawnHandler.Instance.RollToKeepItem();

        if (success)
            panelText.text = winMessage;
        else
            panelText.text = loseMessage;

        // wait
        yield return new WaitForSeconds(postResultWaitPeriod);

        // raise flag
        RaiseEvent(success);

        // reset screen to default
        background.sprite = defaultBG;
        ToggleDeathScreenInput(true);
        panelText.text = gambleDescriptionMessage;
    }

    private void ToggleDeathScreenInput(bool toggle)
    {
        CanvasGroup group = deathScreen.GetComponent<CanvasGroup>();
        if (group != null)
        {
            group.interactable = toggle;
            group.blocksRaycasts = toggle;
        }
    }

    private void RaiseEvent(bool success)
    {
        RespawnScenePayload payload = new RespawnScenePayload();

        payload.gambleSuccess = success;
        payload.calculatedBaseDamage = (int)RespawnHandler.Instance.CalculateBaseDamage();

        gambleButton.enabled = true;
        gambleDescriptionPanel.SetActive(false);
        HudManager.Instance.ToggleHud(true);
        deathScreen.SetActive(false);
        respawnEvent.Raise(payload);
    }

    private void ShowDeathScreen(DeathPayload payload)
    {
        HudManager.Instance.ToggleHud(false);

        bool hasNewItems = ContextRegister.Instance.GetContext().inventoryManager.NewPassiveInPossession(); 

        if (!hasNewItems)
        {
            gambleButton.interactable = false;
            panelText.text = noItemMessage;
        }
        else
        {
            panelText.text = gambleDescriptionMessage;
        }


        if (baseHealth.projectBaseDamage((int)RespawnHandler.Instance.CalculateBaseDamage()))
        {
            HudManager.Instance.StartGameOver();
            return;
        }

        deathScreen.SetActive(true);
    }
}
