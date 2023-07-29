using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string playerName = "Test Name";
    public int playerScore = 0;
    public string playerNameB = "No Data";
    public int playerScoreB = 0;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayGame()
    {
        TMP_InputField playerNameInput = GameObject.Find("Name Input").GetComponent<TMP_InputField>();
        if (playerNameInput.text != "")
        {
            playerName = playerNameInput.text;
        }
        else 
        {
            playerName = "Anonymous";
        }

        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
#if (UNITY_EDITOR)
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    [System.Serializable]
    class Score
    {
        public string playerName;
        public int playerScore;

        public Score()
        {

        }

        public Score(string name, int score)
        {
            playerName = name;
            playerScore = score;
        }
    }

    [System.Serializable]
    class ScoreTable
    {
        public List<Score> scores = new List<Score>();

        public ScoreTable() 
        {
            
        }

        public ScoreTable([NotNull] List<Score> tScores)
        {
            if (scores == null)
            {
                throw new ArgumentNullException(nameof(scores));
            }

            scores = tScores;
        }
    }

    public void SavePlayerScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        Score score = new Score(playerName, playerScore);
        if(!File.Exists(path))
        {
            var table = new ScoreTable();
            table.scores.Add(score);
            var jsonSave = JsonUtility.ToJson(table, true);
            File.WriteAllText(path, jsonSave);
        }
        else
        {
            var json = File.ReadAllText(path);
            var fromJson = JsonUtility.FromJson<ScoreTable>(json);
            fromJson.scores.Add(score);
            var jsonSave = JsonUtility.ToJson(fromJson, true);
            File.WriteAllText(path, jsonSave);
        }
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var fromJson = JsonUtility.FromJson<ScoreTable>(json);
            var bestScore = fromJson.scores.OrderByDescending(s => s.playerScore).Take(1);
            playerNameB = bestScore.ToArray()[0].playerName;
            playerScoreB = bestScore.ToArray()[0].playerScore;
        }
    }
}
