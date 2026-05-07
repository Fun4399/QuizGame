using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public int num1;
    public int num2;
    private int correctAnswer;

    public void GenerateQuestion()
    {
        num1 = Random.Range(1, 20);
        num2 = Random.Range(1, 20);
        correctAnswer = num1 + num2;
    }

    public int GetCorrectAnswer()
    {
        return correctAnswer;
    }

    public int[] GenerateChoices()
    {
        int[] choices = new int[4];
        int correctIndex = Random.Range(0, 4);

        for (int i = 0; i < 4; i++)
        {
            if (i == correctIndex)
                choices[i] = correctAnswer;
            else
                choices[i] = correctAnswer + Random.Range(-5, 6);
        }

        return choices;
    }
}