using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{
    public void SetDifficulty(int index)
    {
        DataController.Instance.SetRound(index);
        SceneManager.LoadScene("Game");
    }
}