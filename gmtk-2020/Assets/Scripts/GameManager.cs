using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public HealthBar healthBar;
  public HealthBarDrain healthBarDrain;
  public EnemySpawnManager enemySpawnManager;
  public GameObject player;
  public CameraController cameraController;
  public GameObject loseScreen;
  public GameObject winScreen;
  public GameObject startScreen;

  public int maxHealth = 100;
  public int timeLimit = 5;

  ChainsawController playerController;
  Coroutine timer;

  void Start()
  {
    startScreen.SetActive(true);
    healthBar.gameObject.SetActive(false);
    enemySpawnManager.StopSpawning();
    if (FindObjectOfType<ChainsawController>() != null)
    {
      Destroy(FindObjectOfType<ChainsawController>().gameObject);
    }
  }

  public void LoseGame()
  {
    loseScreen.SetActive(true);
    healthBarDrain.Stop();
    enemySpawnManager.StopSpawning();
    playerController.Die();
    StopCoroutine(timer);
  }

  public void StartGame()
  {
    enemySpawnManager.CleanUpSpawns();
    if (FindObjectOfType<ChainsawController>())
    {
      Destroy(FindObjectOfType<ChainsawController>());
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
    cameraController.Reset();
    startScreen.SetActive(false);
    loseScreen.SetActive(false);
    winScreen.SetActive(false);
    timer = StartCoroutine(StartTimer(timeLimit));
  }

  IEnumerator StartTimer(int timeLimit)
  {
    float startTime = Time.time;
    float timeElapsed = 0;
    while (timeElapsed < timeLimit)
    {
      Debug.Log(timeElapsed);
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
