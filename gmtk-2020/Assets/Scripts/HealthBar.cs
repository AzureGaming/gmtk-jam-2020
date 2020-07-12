using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  public GameManager gameManager;
  public Slider slider;
  int health;

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.T))
    {
      SubtractHealth(100);
    }
  }

  public void SubtractHealth(int value)
  {
    health -= value;
    SetHealth(health);
  }

  public void IncrementHealth(int value)
  {
    health += value;
    // Manage overflow?
    SetHealth(health);
  }

  public void SetMaxHealth(int value)
  {
    slider.maxValue = value;
    SetHealth(value);
  }

  void SetHealth(int value)
  {
    slider.value = value;
    health = value;
    if (health <= 0)
    {
      gameManager.LoseGame();
    }
    else if (health > 100)
    {
      health = 100;
    }
  }
}
