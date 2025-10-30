using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    [SerializeField]
    private LevelProgressionSO progressionData; 
    private int currentExperience;
    private int toNextLevel;

    [HideInInspector]
    public bool maxLevel = false;

    public int currentLevel = 1;

    public static PlayerLevelManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        currentLevel = 1;
        currentExperience = 0;
        toNextLevel = progressionData.LevelRequirements[0];
    }

    public void AddExperience(int value)
    {
        currentExperience += value;

        if (currentExperience >=  toNextLevel && maxLevel == false) LevelUp();
    }

    private void LevelUp()
    {
        toNextLevel = progressionData.LevelRequirements[currentLevel];
        currentLevel++;

        if (currentLevel >= progressionData.LevelRequirements.Count)
            maxLevel = true;

        Debug.Log("Leveled Up! now level " + currentLevel);

        updateUI();
    }

    private void updateUI()
    {
        HudManager.Instance.level.UpdateLevelText(currentLevel);
    }
}
