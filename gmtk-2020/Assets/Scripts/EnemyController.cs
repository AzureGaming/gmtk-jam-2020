using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

  public AudioSource deathSound;
  public SpriteRenderer spriteRenderer;
  public Rigidbody2D rb;
  public GameObject blood;
  public PortalSpawn portalSpawn;

  public int bloodAmount = 10;
  public int scoreValue = 100;

  protected GameManager gameManager;
  protected HealthBar healthBar;

  private void Awake()
  {
    healthBar = FindObjectOfType<HealthBar>();
    gameManager = FindObjectOfType<GameManager>();
  }

  private void Start()
  {
    StartCoroutine(Spawn());
  }

  public virtual void Die()
  {
    gameManager.IncrementScore(scoreValue);
    deathSound.Play();
    healthBar.IncrementHealth(bloodAmount);
    spriteRenderer.enabled = false;
    rb.isKinematic = true;
    Instantiate(blood, transform.position, Quaternion.identity);
    Destroy(this.gameObject, deathSound.clip.length + 0.5f);
  }

  public IEnumerator Spawn()
  {
    spriteRenderer.enabled = false;
    yield return StartCoroutine(portalSpawn.PortalDone());
    spriteRenderer.enabled = true;
  }
}
