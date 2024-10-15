using UnityEngine;
using UnityEngine.UI;

public class RevivalButton : MonoBehaviour
{
    private StudentRally studentRally;
    private Button button;

    void Start()
    {
        studentRally = FindObjectOfType<StudentRally>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnRevivalButtonClick);
    }

    void OnRevivalButtonClick()
    {
        studentRally.ReviveStudents();
    }
}
