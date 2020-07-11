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

  public int maxHealth = 100;

  ChainsawController playerController;

  void Start()
  {
    StartGame();
  }

  public void LoseGame()
  {
    loseScreen.SetActive(true);
    healthBarDrain.Stop();
    enemySpawnManager.StopSpawning();
    playerController.Die();
  }

  public void StartGame()
  {
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
    healthBar.SetMaxHealth(maxHealth);
    healthBarDrain.Initialize();
    enemySpawnManager.StartSpawning();
    cameraController.Reset();
    loseScreen.SetActive(false);
  }
}
