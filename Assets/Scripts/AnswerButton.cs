using UnityEngine;
using System.Collections;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    public TextMeshProUGUI answerText;

    private AnswerData answerData;
    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        gameController = FindAnyObjectByType<GameController>();
    }

    public void Setup(AnswerData data)
    {
        answerData = data;
        answerText.text = answerData.answerText;
    }

    public void HandleClick()
    {
        gameController.AnswerButtonClicked(answerData.isCorrect);
    }
}
