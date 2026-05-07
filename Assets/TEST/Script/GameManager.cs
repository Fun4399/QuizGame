using UnityEngine;

public class GameManager : MonoBehaviour
{
    public QuestionManager questionManager;

    void Start()
    {
        questionManager.GenerateQuestion();
    }
}