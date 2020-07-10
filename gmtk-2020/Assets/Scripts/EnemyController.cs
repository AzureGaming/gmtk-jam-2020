using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public void Die()
  {
    Debug.LogWarning("Implement Enemy Death...");
    Destroy(this.gameObject);
  }
}
