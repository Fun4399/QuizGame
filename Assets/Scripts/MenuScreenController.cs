using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameEnum;

public class MenuScreenController : MonoBehaviour
{

    public void SetCourse(int index)
    {
        if (index == 0)
        {
            DataController.Instance.currentSubject = SubjectType.Math;
        }
        else
        {
            DataController.Instance.currentSubject = SubjectType.English;
        }
    }

    public void SetDifficulty(int index)
    {
        if (index == 0)
        {
            DataController.Instance.currentDifficulty = DifficultyType.Easy;
        }
        else if (index == 1)
        {
            DataController.Instance.currentDifficulty = DifficultyType.Medium;
        }
        else
        {
            DataController.Instance.currentDifficulty = DifficultyType.Hard;
        }
    }
}