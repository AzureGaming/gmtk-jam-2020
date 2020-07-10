using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public int bloodAmount = 10;

  HealthBar healthBar;

  private void Awake()
  {
    healthBar = FindObjectOfType<HealthBar>();
  }

  public void Die()
  {
    Debug.LogWarning("Implement Enemy Death...");
    healthBar.IncrementHealth(bloodAmount);
    Destroy(this.gameObject);
  }
}
