using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int scoreMultiplier;

    private float score;
    private bool shouldCount = true;
    void Update()
    {
        if (!shouldCount)
        {
            return;
        }
        
        score += Time.deltaTime * scoreMultiplier;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public int GetScore()
    {
        return Mathf.FloorToInt(score);
    }

    public int EndTimer()
    {
        shouldCount = false;
        scoreText.text = string.Empty;
        return Mathf.FloorToInt(score);
    }

    public void StartTimer()
    {
        score = 0;
        shouldCount = true;
    }
}
