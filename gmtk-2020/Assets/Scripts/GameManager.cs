using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public HealthBar healthBar;
  public HealthBarDrain healthBarDrain;
  public EnemySpawnManager enemySpawnManager;
  public ScoreManager scoreManager;
  public GameObject player;
  public GameObject loseScreen;
  public GameObject winScreen;
  public ChainsawController playerController;

  public int maxHealth = 100;
  public int timeLimit = 600;

  bool gloryKilling = false;

  Coroutine timer;

  int enemiesKilled = 0;
  int scoreMultiplier = 1;
  int score = 0;

  void Start()
  {
    StartGame();
  }

  public void LoseGame()
  {
    loseScreen.SetActive(true);
    healthBarDrain.Stop();
    enemySpawnManager.StopSpawning();
    StartCoroutine(playerController.Die());
  }

  public void StartGame()
  {
    enemiesKilled = 0;
    scoreManager.ShowScore();
    scoreManager.SetScore(0, 1);
    enemySpawnManager.CleanUpSpawns();
    healthBar.gameObject.SetActive(true);
    healthBar.SetMaxHealth(maxHealth);
    healthBarDrain.Initialize();
    enemySpawnManager.StartSpawning();
    Camera.main.GetComponent<CameraController>().Reset();
    loseScreen.SetActive(false);
    playerController.WakeUp();
  }

  public void IncrementScore(int value)
  {
    enemiesKilled += 1;
    if (enemiesKilled % 5 == 0)
    {
      scoreMultiplier += 1;
    }
    if (enemiesKilled % 10 == 0)
    {
      FindObjectOfType<ChainsawController>().GoBerserk();
    }
    score += value * scoreMultiplier;
    scoreManager.SetScore(score * scoreMultiplier, scoreMultiplier);
  }

  public void ResetMultiplier()
  {
    enemiesKilled = 0;
    scoreMultiplier = 1;
    scoreManager.SetScore(score, scoreMultiplier);
  }

  public void SetGloryKilling(bool value)
  {
    gloryKilling = value;
  }

  public bool IsGloryKilling()
  {
    return gloryKilling;
  }

  IEnumerator StartTimer(int timeLimit)
  {
    float startTime = Time.time;
    float timeElapsed = 0;
    while (timeElapsed < timeLimit)
    {
      yield return new WaitForSeconds(1f);
      timeElapsed = Time.time - startTime;
    }
    WinGame();
  }

  void WinGame()
  {
    winScreen.SetActive(true);
    healthBarDrain.Stop();
    enemySpawnManager.StopSpawning();
    playerController.Die();
  }
}
