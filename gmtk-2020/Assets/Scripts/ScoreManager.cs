using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
  public TMPro.TextMeshProUGUI scoreText;
  public GameObject scoreUI;

  int score = 0;
  int multiplier = 1;

  public void ShowScore()
  {
    scoreUI.SetActive(true);
  }

  public void HideScore()
  {
    scoreUI.SetActive(false);
  }

  public void ResetMultiplier()
  {
    SetScoreText(score, 1);
  }

  public void IncrementScore(int scoreValue, int multiplierValue)
  {
    SetScore(score + scoreValue, multiplierValue);
  }

  public void SetScore(int scoreValue, int multiplierValue)
  {
    score = scoreValue;
    multiplier = multiplierValue;
    SetScoreText(score, multiplierValue);
  }

  public void SetScore()
  {
    SetScoreText(score, multiplier);
  }

  void SetScoreText(int value, int multiplier)
  {
    scoreText.text = "Score: " + value + " x " + multiplier;
  }
}
