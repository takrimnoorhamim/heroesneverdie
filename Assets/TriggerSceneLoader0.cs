using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneLoader : MonoBehaviour
{
    public GameObject winPanel;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is tagged with the correct tag
        if (other.CompareTag("Leader"))  // You can change "Player" to whatever tag you want
        {
            Time.timeScale = 0f;

            winPanel.SetActive(true);
        }
    }
}
