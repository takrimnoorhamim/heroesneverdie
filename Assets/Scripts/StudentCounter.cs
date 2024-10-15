// StudentCounter script
using UnityEngine;
using UnityEngine.UI;

public class StudentCounter : MonoBehaviour
{
    public Text studentCountText;
    private StudentRally studentRally;

    void Start()
    {
        studentRally = FindObjectOfType<StudentRally>();
    }

    void Update()
    {
        int count = studentRally.GetAcquiredStudentCount();
        studentCountText.text = "Acquired Students: " + count;
    }
}