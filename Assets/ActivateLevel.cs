using UnityEngine;
using UnityEngine.UI;

public class ActivateGameObject : MonoBehaviour
{
    public GameObject targetObject; // The GameObject to activate
    private Button button;

    void Start()
    {
        // Get the Button component and add a listener to the button click event
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Activate the target GameObject
        targetObject.SetActive(true);
    }
}
