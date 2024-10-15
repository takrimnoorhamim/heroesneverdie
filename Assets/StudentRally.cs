using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentRally : MonoBehaviour
{
    public float followSpeed = 2f;
    public float verticalSpacing = 1.5f;
    public float horizontalSpacing = 2f;
    public float playerGap = 2f;
    public float gapMultiplier = 1f;
    private List<Transform> students = new List<Transform>();
    private int acquiredCount = 0;  // Total count of acquired students, regardless of list size
    private const int maxStudents = 150;  // Maximum number of students to retain in the list

    void Update()
    {
        MoveStudents();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Student"))
        {
            if (students.Count < maxStudents) // Only add to the list if under the limit
            {
                students.Add(collision.transform);
                collision.enabled = true;  // Keep the collider enabled
                collision.gameObject.tag = "AcquiredStudent"; // Change tag to AcquiredStudent
            }
            else
            {
                // If the limit is reached, acquire the student without adding to the list
                StartCoroutine(MoveAndDestroyStudent(collision.transform));
            }

            acquiredCount++; // Always increment the total count
        }
    }

    void MoveStudents()
    {
        for (int i = 0; i < students.Count; i++)
        {
            Vector3 targetPosition = GetTargetPosition(i);
            students[i].position = Vector3.Lerp(students[i].position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    Vector3 GetTargetPosition(int index)
    {
        float xOffset = -playerGap;
        float yOffset;

        int lineNumber = index / 10;
        int positionInLine = index % 10;

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

    private IEnumerator MoveAndDestroyStudent(Transform student)
    {
        // Move the student 20 units to the left
        Vector3 startPosition = student.position;
        Vector3 targetPosition = startPosition + new Vector3(-20f, 0f, 0f);
        float moveDuration = 1f; // Duration of the move
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            student.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Finally, destroy the student after moving
        Destroy(student.gameObject);
    }

    public void MultiplyStudents(float multiplier)
    {
        // Multiply the number of students by the given multiplier
        int additionalStudents = Mathf.RoundToInt(students.Count * (multiplier - 1));

        for (int i = 0; i < additionalStudents; i++)
        {
            // Destroy new students if the limit has been reached
            if (acquiredCount < maxStudents)
            {
                GameObject newStudent = Instantiate(students[0].gameObject, students[students.Count - 1].position, Quaternion.identity);
                students.Add(newStudent.transform);
            }
            acquiredCount++; // Increment total count for each additional student
        }
    }

    public int GetAcquiredStudentCount()
    {
        return acquiredCount;  // Return the total count of acquired students
    }
}
