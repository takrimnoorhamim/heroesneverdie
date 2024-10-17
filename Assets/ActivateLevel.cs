using UnityEngine;
using UnityEngine.UI;

public class ActivateGameObject : MonoBehaviour
{
    public GameObject toActivate; // The GameObject to activate
    private Button button;
    public GameObject toDeactivate;

    void Start()
    {
        // Get the Button component and add a listener to the button click event
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        // Activate the target GameObject if it exists
        if (toActivate != null)
        {
            toActivate.SetActive(true);
        }

        // Deactivate Logos if it exists
        if (toDeactivate != null)
        {
            toDeactivate.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}