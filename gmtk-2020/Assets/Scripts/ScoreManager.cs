using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
  public GameObject scoreUI;
  public GameObject[] scorePlaceholders;
  public GameObject multiplierPlaceholder;
  public Sprite[] sprites;

  public void ShowScore()
  {
    scoreUI.SetActive(true);
  }

  public void HideScore()
  {
    scoreUI.SetActive(false);
  }

  // public void IncrementScore(int scoreValue, int multiplierValue)
  // {
  //   SetScore(score + scoreValue, multiplierValue);
  // }

  // public void DecrementScore(int scoreValue, int multiplierValue)
  // {
  //   SetScore(score - scoreValue, multiplierValue);
  // }

  public void SetScore(int scoreValue, int multiplierValue)
  {
    Debug.Log("Set score" + scoreValue);
    SetScoreText(scoreValue, multiplierValue);
  }

  void SetScoreText(int value, int multiplier)
  {
    foreach (GameObject placeholder in scorePlaceholders)
    {
      int digit = value % 10;
      Sprite sprite = sprites[digit];
      placeholder.GetComponent<Image>().sprite = sprite;
      value /= 10;
    }
    if (multiplier > 9)
    {
      return;
    }
    multiplierPlaceholder.GetComponent<Image>().sprite = sprites[multiplier];
  }
}
