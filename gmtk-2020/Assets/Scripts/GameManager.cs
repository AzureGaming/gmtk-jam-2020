using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public HealthBar healthBar;
  public HealthBarDrain healthBarDrain;

  public int maxHealth = 100;

  void Start()
  {
    healthBar.SetMaxHealth(maxHealth);
    healthBarDrain.Initialize();
  }
}
