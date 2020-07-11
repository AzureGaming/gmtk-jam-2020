using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  GameManager gameManager;

  public AudioSource deathSound;
  public SpriteRenderer spriteRenderer;
  public Rigidbody2D rb;

  public int bloodAmount = 10;
  public int scoreValue = 100;

  HealthBar healthBar;

  private void Awake()
  {
    healthBar = FindObjectOfType<HealthBar>();
    gameManager = FindObjectOfType<GameManager>();
  }

  public void Die()
  {
    gameManager.IncrementScore(scoreValue);
    deathSound.Play();
    healthBar.IncrementHealth(bloodAmount);
    spriteRenderer.enabled = false;
    rb.isKinematic = true;
    Destroy(this.gameObject, deathSound.clip.length + 0.5f);
  }
}
