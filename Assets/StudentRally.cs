using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentRally : MonoBehaviour
{
    public float followSpeed = 2f;
    public float verticalSpacing = 1.5f;
    public float horizontalSpacing = 2f;
    public float playerGap = 2f;
    public float gapMultiplier = 1f;

    public int studentsInALine = 5;

    private List<Transform> activeStudents = new List<Transform>();
    private List<Transform> inactiveStudents = new List<Transform>();
    private int acquiredCount = 0;
    private const int maxStudents = 150;

    public GameObject revivalButton;

    void Start()
    {
        revivalButton.SetActive(false);
    }

    void Update()
    {
        MoveStudents();
        UpdateRevivalButton();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Student"))
        {
            if (activeStudents.Count < maxStudents)
            {
                AddStudent(collision.transform);
            }
            else
            {
                StartCoroutine(MoveAndDeactivateStudent(collision.transform));
            }
        }
    }

    void AddStudent(Transform student)
    {
        activeStudents.Add(student);
        student.GetComponent<Collider2D>().enabled = true;
        student.gameObject.tag = "AcquiredStudent";
        acquiredCount++;
    }

    public void DeactivateStudent(Transform student)
    {
        activeStudents.Remove(student);
        inactiveStudents.Add(student);
        acquiredCount--; // Decrease the acquired count when a student is deactivated
        StartCoroutine(FadeOutStudent(student));
        RearrangeStudents();
    }

    IEnumerator FadeOutStudent(Transform student)
    {
        SpriteRenderer spriteRenderer = student.GetComponent<SpriteRenderer>();
        float fadeDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        student.gameObject.SetActive(false);
    }

    void RearrangeStudents()
    {
        for (int i = 0; i < activeStudents.Count; i++)
        {
            StartCoroutine(MoveStudentToPosition(activeStudents[i], i));
        }
    }

    IEnumerator MoveStudentToPosition(Transform student, int index)
    {
        Vector3 startPos = student.position;
        Vector3 targetPos = GetTargetPosition(index);
        float moveDuration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            student.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        student.position = targetPos;
    }

    void MoveStudents()
    {
        for (int i = 0; i < activeStudents.Count; i++)
        {
            Vector3 targetPosition = GetTargetPosition(i);
            activeStudents[i].position = Vector3.Lerp(activeStudents[i].position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    Vector3 GetTargetPosition(int index)
    {
        float xOffset = -playerGap;
        float yOffset;

        int lineNumber = index / studentsInALine;
        int positionInLine = index % studentsInALine;

        xOffset -= lineNumber * horizontalSpacing * gapMultiplier;

        if (positionInLine % 2 == 0)
        {
            yOffset = -(positionInLine / 2) * verticalSpacing * gapMultiplier;
        }
        else
        {
            yOffset = (positionInLine / 2 + 1) * verticalSpacing * gapMultiplier;
        }

        Vector3 targetPosition = transform.position + new Vector3(xOffset, yOffset, 0);
        return targetPosition;
    }

    private IEnumerator MoveAndDeactivateStudent(Transform student)
    {
        Vector3 startPosition = student.position;
        Vector3 targetPosition = startPosition + new Vector3(-20f, 0f, 0f);
        float moveDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            student.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        DeactivateStudent(student);
    }

    public int GetAcquiredStudentCount()
    {
        return acquiredCount;
    }

    void UpdateRevivalButton()
    {
        if (inactiveStudents.Count > 0)
        {
            revivalButton.SetActive(true);
            Image buttonImage = revivalButton.GetComponent<Image>();
            float opacity = Mathf.Min(1f, inactiveStudents.Count * 0.1f);
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, opacity);

            // Button ke disable kore rakha hocche jotokhon na opacity 1 hocche
            Button button = revivalButton.GetComponent<Button>();
            button.interactable = (opacity >= 1f);
        }
        else
        {
            revivalButton.SetActive(false);
        }
    }

    public void ReviveStudents()
    {
        Button button = revivalButton.GetComponent<Button>();
        if (button.interactable)
        {
            int studentsToRevive = inactiveStudents.Count * 2;
            for (int i = 0; i < studentsToRevive; i++)
            {
                if (activeStudents.Count < maxStudents)
                {
                    Transform newStudent = Instantiate(activeStudents[0], transform.position, Quaternion.identity);
                    AddStudent(newStudent);
                }
            }
            inactiveStudents.Clear();
            RearrangeStudents();
        }
    }
}