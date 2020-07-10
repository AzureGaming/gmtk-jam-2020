﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarDrain : MonoBehaviour
{
  public HealthBar healthBar;

  public int drainDamage = 1;
  public float drainRate = 1f;

  public void Initialize()
  {
    StartCoroutine(DrainHealth());
  }

  public void Stop()
  {
    StopCoroutine(DrainHealth());
  }

  IEnumerator DrainHealth()
  {
    while (true)
    {
      healthBar.SubtractHealth(drainDamage);
      yield return new WaitForSeconds(drainRate);
    }
  }
}
