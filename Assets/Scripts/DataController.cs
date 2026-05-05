using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    private RoundData[] allRoundData;
    private int currentRoundIndex = 0;

    public static DataController Instance;
    private string gameDataFileName = "data.json";

    // Use this for initialization
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadGameData();

        SceneManager.LoadScene("MenuScreen");
    }

    public void SetRound(int index)
    {
        currentRoundIndex = index;
    }

    public RoundData GetCurrentRoundData()
    {
        return allRoundData[currentRoundIndex];
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadGameData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            // Retrieve the allRoundData property of loadedData
            allRoundData = loadedData.allRoundData;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }
}
