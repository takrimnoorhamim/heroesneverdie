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

            // Optional: Update button appearance based on unlock status
            UpdateButtonAppearance(levelButtons[i], isUnlocked);
        }
    }

    private void UpdateButtonAppearance(Button button, bool isUnlocked)
    {
        // You can customize this method to change the button's appearance
        // For example, changing the color or adding a lock icon
        ColorBlock colors = button.colors;
        colors.normalColor = isUnlocked ? Color.white : Color.gray;
        button.colors = colors;
    }

    // Call this method when a level is completed to unlock the next level
    public void UnlockNextLevel(int completedLevelNumber)
    {
        int nextLevel = completedLevelNumber + 1;
        PlayerPrefs.SetInt($"Level{nextLevel}Unlocked", 1);
        PlayerPrefs.Save();
        UpdateLevelButtons();
    }

    // Method to load a level when its button is clicked
    public void LoadLevel(int levelNumber)
    {
        // Implement your level loading logic here
        Debug.Log($"Loading Level {levelNumber}");
        // Example: SceneManagement.LoadScene($"Level{levelNumber}");
    }
}