using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{
    private int selectedCourse = 0;

    public void SetCourse(int courseIndex)
    {
        selectedCourse = courseIndex;

        Debug.Log("Selected Course: " + selectedCourse);
    }

    public void SetDifficulty(int difficultyIndex)
    {
        int roundIndex = (selectedCourse * 3) + difficultyIndex;

        DataController.Instance.SetRound(roundIndex);

        SceneManager.LoadScene("Level 1");
    }
}