using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  public Slider slider;
  int health;

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
    Debug.Log("Set health" + value);
    slider.value = value;
    health = value;
    if (health <= 0)
    {
      Debug.LogWarning("Implement LOSE state...");
      GetComponent<HealthBarDrain>().Stop();
    }
    else if (health > 100)
    {
      health = 100;
      Debug.LogWarning("Overflow...");
    }
  }
}
