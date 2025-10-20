using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public HealthUIElements healthElements;

    public void SetElements(HealthUIElements elements)
    {
        healthElements = elements;
    }

    public void UpdatePlayerHealth(int hp, int maxHp)
    {
        float healthPercentage = hp / maxHp;
        int hpToSpriteRatio = (int)Mathf.Round((healthElements.playerHealthSprites.Count) / maxHp);
        Sprite newHPSprite = healthElements.playerHealthSprites[hpToSpriteRatio];

        healthElements.healthIcon.sprite = newHPSprite;
    }

    public void UpdateBaseHealth(int hp, int maxHp)
    {
        float healthPercentage = hp / maxHp;
        int hpToSpriteRatio = (int)Mathf.Round((healthElements.playerHealthSprites.Count) / maxHp);
        Sprite newHPSprite = healthElements.playerHealthSprites[hpToSpriteRatio];

        healthElements.healthIcon.sprite = newHPSprite;
    }
}
