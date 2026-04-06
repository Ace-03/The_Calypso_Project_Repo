using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public HealthUIElements healthElements;

    public void SetElements(HealthUIElements elements)
    {
        healthElements = elements;
    }

    public void UpdatePlayerHealth(float hp, float maxHp)
    {
        healthElements.healthIcon.sprite = SetHealthSprite(hp, maxHp, healthElements.playerHealthSprites);

        string roundingValue = "";

        if (hp % 1 == 0)
            roundingValue = "f0";
        else
            roundingValue = "f1";

            healthElements.healthText.text = hp.ToString(roundingValue);
        healthElements.backgroundHealthText.text = hp.ToString(roundingValue);
    }

    public void UpdateBaseHealth(float hp, float maxHp)
    {
        healthElements.baseIcon.sprite = SetHealthSprite(hp, maxHp, healthElements.baseHealthSprites);
        healthElements.baseText.text = hp.ToString("f0");

    }

    private Sprite SetHealthSprite(float hp, float maxHp, List<Sprite> SpriteList)
    {
        float healthPercentage = (hp / maxHp);
        float healthSpriteCount = SpriteList.Count;
        int hpToSpriteRatio = (int)(healthSpriteCount - Mathf.Round(healthSpriteCount * healthPercentage));

        hpToSpriteRatio = (int)Mathf.Clamp(hpToSpriteRatio, 0, healthSpriteCount - 1);

        return SpriteList[hpToSpriteRatio];
    }
}
