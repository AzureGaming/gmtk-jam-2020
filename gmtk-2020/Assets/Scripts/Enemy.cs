using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public EnemyController controller;
  public void Die()
  {
    controller.Die();
  }

  public IEnumerator GloryDeath()
  {
    yield return StartCoroutine(controller.GloryDeath());
  }
}
