using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public AudioSource deathSound;
  public SpriteRenderer spriteRenderer;
  public Rigidbody2D rb;

  public int bloodAmount = 10;

  HealthBar healthBar;

  private void Awake()
  {
    healthBar = FindObjectOfType<HealthBar>();
  }

  public void Die()
  {
    deathSound.Play();
    healthBar.IncrementHealth(bloodAmount);
    spriteRenderer.enabled = false;
    rb.isKinematic = true;
    Destroy(this.gameObject, deathSound.clip.length + 0.5f);
  }
}
