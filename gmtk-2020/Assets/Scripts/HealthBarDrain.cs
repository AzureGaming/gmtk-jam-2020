using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarDrain : MonoBehaviour
{
  public HealthBar healthBar;

  int drainDamage = 3;
  float drainRate = 1f;
  int waitCounter = 0;

  public void Initialize()
  {
    StartCoroutine(DrainHealth());
  }

  public void Stop()
  {
    StopAllCoroutines();
  }

  IEnumerator DrainHealth()
  {
    while (true)
    {
      if (waitCounter % 10 == 0)
      {
        drainDamage += 2;
      }
      healthBar.SubtractHealth(drainDamage);
      yield return new WaitForSeconds(drainRate);
      waitCounter++;
    }
  }
}
