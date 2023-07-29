using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string playerName = "Test Name";
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
}
