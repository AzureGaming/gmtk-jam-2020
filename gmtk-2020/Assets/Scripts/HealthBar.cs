using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  public GameManager gameManager;
  public Slider slider;
  float health;

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.T))
    {
      SubtractHealth(100);
    }
  }

  public void SubtractHealth(float value)
  {
    health -= value;
    SetHealth(health);
  }

  public void IncrementHealth(float value)
  {
    health += value;
    // Manage overflow?
    SetHealth(health);
  }

  public void SetMaxHealth(float value)
  {
    slider.maxValue = value;
    SetHealth(value);
  }

  void SetHealth(float value)
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
