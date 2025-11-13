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
        healthElements.healthText.text = $"{hp}";
    }

    public void UpdateBaseHealth(int hp, int maxHp)
    {
        healthElements.healthIcon.sprite = SetHealthSprite(hp, maxHp, healthElements.baseHealthSprites);
        healthElements.baseText.text = $"{hp}";

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
