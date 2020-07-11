using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry : MonoBehaviour
{
  public GameManager gameManager;
  public void ReloadGame()
  {
    gameManager.StartGame();
  }
}
