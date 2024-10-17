using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockSystem : MonoBehaviour
{
    public Button[] levelButtons;

    private void Start()
    {
        InitializeLevels();
        UpdateLevelButtons();
    }

    private void InitializeLevels()
    {
        // Ensure the first level is always unlocked
        PlayerPrefs.SetInt("Level1Unlocked", 1);
        PlayerPrefs.Save();
    }

    private void UpdateLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelNumber = i + 1;
            bool isUnlocked = PlayerPrefs.GetInt($"Level{levelNumber}Unlocked", 0) == 1;
            levelButtons[i].interactable = isUnlocked;
            UpdateButtonAppearance(levelButtons[i], isUnlocked);
        }
    }

    private void UpdateButtonAppearance(Button button, bool isUnlocked)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            if (!isUnlocked)
            {
                Color imageColor = buttonImage.color;
                imageColor.a = 0.8f; // Set opacity to 80% for locked levels
                buttonImage.color = imageColor;
            }
            // Unlocked levels remain unchanged
        }

        // Optional: Change text color for locked levels
        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText != null && !isUnlocked)
        {
            buttonText.color = Color.gray;
        }

        // Update interactability
        button.interactable = isUnlocked;
    }

    public void UnlockNextLevel(int completedLevelNumber)
    {
        int nextLevel = completedLevelNumber + 1;
        PlayerPrefs.SetInt($"Level{nextLevel}Unlocked", 1);
        PlayerPrefs.Save();
        UpdateLevelButtons();
    }

    public void LoadLevel(int levelNumber)
    {
        Debug.Log($"Loading Level {levelNumber}");
        // Example: SceneManagement.LoadScene($"Level{levelNumber}");
    }
}