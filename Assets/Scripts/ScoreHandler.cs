using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float pointsPerMeter = 0.03f;
    [SerializeField] TMP_Text scoreText;

    [SerializeField] TMP_Text bestScoreHeader;
    [SerializeField] TMP_Text bestScoreText;

    private bool canEarnScore = false;
    private float score = 0f;

    public bool CanEarnScore { get { return canEarnScore; } set { canEarnScore = value; } }
    public float Score { get { return score; } set { score = value; } }

    void Awake()
    {
        // Get Best score
        float bestScore = PlayerPrefs.GetFloat("bestScore");
        if (bestScore == 0)
        {
            bestScoreHeader.gameObject.SetActive(false);
            bestScoreText.gameObject.SetActive(false);
        }
        else
        {
            bestScoreText.text = Mathf.Round(bestScore).ToString();
        }
    }

    void Update()
    {
        GiveScore();
        SetScoreText();
    }

    void GiveScore()
    {
        if (canEarnScore)
        {
            score += pointsPerMeter;
        }

        if (score > 200)
        {
            // Simulate death
        }
    }

    void SetScoreText()
    {
        scoreText.text = Mathf.Round(score).ToString();
    }

    public void SaveScore()
    {
        CanEarnScore = false;

        // Save score to PlayerPrefs
        float bestScore = PlayerPrefs.GetFloat("bestScore");
        if (bestScore != 0)
        {
            if (score <= bestScore)
            {
                return;
            }
        }
        PlayerPrefs.SetFloat("bestScore", score);
        PlayerPrefs.Save();
    }
}
