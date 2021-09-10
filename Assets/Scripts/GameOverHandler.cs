using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private TMP_Text finalScoreText;

    public void EndGame()
    {
        int score = scoreSystem.GetScore();
        finalScoreText.text = $"Final Score: {score}";
        scoreSystem.gameObject.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
