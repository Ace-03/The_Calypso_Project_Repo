using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public LevelUIElements levelElements;

    public void SetElements(LevelUIElements elements)
    {
        levelElements = elements;
    }

    public void UpdateLevelText(int level)
    {
        levelElements.levelText.text = level.ToString();
    }
}
