using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI mainMenuBestScoreText;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.LoadBestScore();
        mainMenuBestScoreText.text = $"Best Score: Name: {GameManager.Instance.playerNameB} Score: {GameManager.Instance.playerScoreB}";
    }
}
