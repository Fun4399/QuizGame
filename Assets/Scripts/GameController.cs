using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI questionDisplayText;
    public TextMeshProUGUI scoreDisplayText;
    public TextMeshProUGUI timeRemainingDisplayText;
    public Slider playerHPBar;
    public Slider enemyHPBar;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;

    private DataController dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;
    public CombatSystem combatSystem;

    private bool isRoundActive;
    private float timeRemaining;
    private int questionIndex;
    private int playerScore;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        dataController = FindAnyObjectByType<DataController>();
        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;
        ShuffleQuestions(questionPool);
        timeRemaining = currentRoundData.timeLimitInSeconds;
        UpdateTimeRemainingDisplay();

        playerScore = 0;
        questionIndex = 0;
        playerHPBar.maxValue = combatSystem.playerHP;
        enemyHPBar.maxValue = combatSystem.enemyHP;

        UpdateHPUI();
        ShowQuestion();
        isRoundActive = true;

    }

    private void ShowQuestion()
    {
        timeRemaining = currentRoundData.timeLimitInSeconds;
        UpdateTimeRemainingDisplay();

        RemoveAnswerButtons();
        QuestionData questionData = questionPool[questionIndex];
        questionDisplayText.text = questionData.questionText;

        for (int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.answers[i]);
        }
    }

    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            scoreDisplayText.text = "Score: " + playerScore.ToString();

            combatSystem.PlayerAttack();
        }
        else
        {
            combatSystem.EnemyAttack();
        }

        if (combatSystem.IsGameOver())
        {
            EndRound();
            return;
        }

        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            EndRound();
        }

    }

    public void EndRound()
    {
        isRoundActive = false;

        questionDisplay.SetActive(false);

        if (combatSystem.PlayerWon())
        {
            Invoke(nameof(LoadNextLevel), 2f);
        }
        else
        {
            roundEndDisplay.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    private void UpdateTimeRemainingDisplay()
    {
        timeRemainingDisplayText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRoundActive)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimeRemainingDisplay();

            if (timeRemaining <= 0f)
            {
                combatSystem.EnemyAttack();

                if (combatSystem.IsGameOver())
                {
                    EndRound();
                    return;
                }

                if (questionPool.Length > questionIndex + 1)
                {
                    questionIndex++;
                    ShowQuestion();
                }
                else
                {
                    EndRound();
                }
            }
        }

        UpdateHPUI();
    }

    private void ShuffleQuestions(QuestionData[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = Random.Range(i, array.Length);
            QuestionData temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    void UpdateHPUI()
    {
        playerHPBar.value = combatSystem.playerHP;
        enemyHPBar.value = combatSystem.enemyHP;
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("MenuScreen");
        }
    }
}