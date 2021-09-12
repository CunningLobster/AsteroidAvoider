using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject player;
    [SerializeField] private Button continueButton;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private AsteroidSpawner asteroidSpawner;

    public void EndGame()
    {
        int score = scoreSystem.EndTimer();
        finalScoreText.text = $"Final Score: {score}";
        //scoreSystem.gameObject.SetActive(false);
        asteroidSpawner.gameObject.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void ContinueButton()
    {
        AdManager.Instance.ShowAd(this);

        continueButton.interactable = false;
    }

    public void ContinueGame()
    {
        scoreSystem.StartTimer();
        
        player.transform.position = Vector3.zero;
        player.SetActive(true);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        gameOverMenu.SetActive(false);
        asteroidSpawner.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
