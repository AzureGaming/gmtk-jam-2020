using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : EnemyController
{
  public override void Die()
  {
    gameManager.ResetMultiplier();
    deathSound.Play();
    healthBar.SubtractHealth(bloodAmount);
    spriteRenderer.enabled = false;
    GetComponent<BoxCollider2D>().enabled = false;
    Instantiate(blood, transform.position, Quaternion.identity);
    Destroy(this.gameObject, deathSound.clip.length + 0.5f);
  }
}
