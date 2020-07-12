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
  public GameObject startScreen;

  public int maxHealth = 100;
  public int timeLimit = 600;

  bool gloryKilling = false;

  ChainsawController playerController;
  Coroutine timer;

  int enemiesKilled = 0;
  int scoreMultiplier = 1;

  void Start()
  {
    // startScreen.SetActive(true);
    // scoreManager.HideScore();
    // healthBar.gameObject.SetActive(false);
    // enemySpawnManager.StopSpawning();
    // if (FindObjectOfType<ChainsawController>() != null)
    // {
    //   Destroy(FindObjectOfType<ChainsawController>().gameObject);
    // }
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
    if (FindObjectOfType<ChainsawController>())
    {
      playerController = FindObjectOfType<ChainsawController>().GetComponent<ChainsawController>();
    }
    else
    {
      GameObject playerInstantiation = Instantiate(player);
      playerController = playerInstantiation.GetComponent<ChainsawController>();
    }
    healthBar.gameObject.SetActive(true);
    healthBar.SetMaxHealth(maxHealth);
    healthBarDrain.Initialize();
    enemySpawnManager.StartSpawning();
    Camera.main.GetComponent<CameraController>().Reset();
    // startScreen.SetActive(false);
    loseScreen.SetActive(false);
    // winScreen.SetActive(false);
    // timer = StartCoroutine(StartTimer(timeLimit));
  }

  public void IncrementScore(int score)
  {
    enemiesKilled += 1;
    if (enemiesKilled % 5 == 0)
    {
      scoreMultiplier += 1;
    }
    scoreManager.IncrementScore(score * scoreMultiplier, scoreMultiplier);
  }

  public void ResetMultiplier()
  {
    enemiesKilled = 0;
    scoreManager.ResetMultiplier();
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
