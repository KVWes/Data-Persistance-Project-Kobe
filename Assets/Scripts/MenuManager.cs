using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using JetBrains.Annotations;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] TMP_InputField nameField;
    [SerializeField] TMP_Text bestScoreText;

    public string userName = "";
    public string bestName;
    public int bestScore;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameInfo();
    }

    // Start is called before the first frame update
    void Start()
    {
       if (bestName != "")
        {
            bestScoreText.text = "Best Score: " + bestName + " : " + bestScore;
        }
    }

    public void SetBestScore(int score)
    {
        if (score > bestScore)
        {
            bestScore = score;
            bestName = userName;
            SaveGameInfo();
            MainManager.Instance.bestScoreText.text = "Best Score : " + bestName + " : " + bestScore;
        }

        Debug.Log("Score: " + score + " Player: " + userName);
    }

    public void StartNew()
    {
        if (nameField.text != "")
        {
            userName = nameField.text;
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.LogWarning("Please enter a name!");
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    // Update is called once per frame
    [System.Serializable]
    class SaveData
    {
        public string userName;
        public int bestScore;
    }

    public void SaveGameInfo()
    {
        SaveData data = new SaveData();
        data.userName = bestName;
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadGameInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;
            bestName = data.userName;
        }
    }
}
