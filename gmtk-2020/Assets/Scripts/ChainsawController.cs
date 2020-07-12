using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawController : MonoBehaviour
{
  public Rigidbody2D rb;
  public Animator animator;
  public AudioSource chainsawIdle;
  public ChainsawCharge charge;
  public SpriteRenderer spriteRenderer;
  public GameObject bladeSparks;
  public GameObject gloryKill;
  public BoxCollider2D boxCollider;
  public GameObject berserkOverlay;
  public GameManager gameManager;

  Coroutine idleAudio;
  Coroutine invincibleCooldown;
  Vector3 lastDirection;
  Color color;

  float speed = 10f;
  bool charging = false;
  bool isDead = false;
  bool invincible = false;
  bool berserk = false;
  bool gloryKilling = false;

  private void Awake()
  {
    color = spriteRenderer.color;
    gameManager = FindObjectOfType<GameManager>();
  }

  private void Start()
  {
    idleAudio = StartCoroutine(LoopIdleAudio());
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Wall")
    {
      if (!invincible)
      {
        FindObjectOfType<HealthBar>().SubtractHealth(5);
        StartCoroutine(SetInvincible());
      }
    }
  }

  private void OnCollisionStay2D(Collision2D other)
  {
    if (other.gameObject.tag == "Wall")
    {
      FindObjectOfType<HealthBar>().SubtractHealth(0.5f);
    }
  }

  private void FixedUpdate()
  {
    if (!gloryKilling && !isDead)
    {
      if (Input.GetMouseButton(0))
      {
        charging = true;
        charge.Attack(lastDirection);
      }
      else if (charging)
      {
        charging = false;
        charge.UnRev();
      }
      else
      {
        ConvertMousePosToDirection();
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (gloryKilling)
    {
      return;
    }

    if (other.tag == "Demon")
    {
      if (berserk)
      {
        StartCoroutine(HandleGloryKill(other));
      }
      else
      {
        StartCoroutine(HandleEnemyCollision(other));
      }
    }
    else if (other.tag == "Enemy")
    {
      StartCoroutine(HandleEnemyCollision(other));
    }
  }

  public IEnumerator Die()
  {
    isDead = true;
    if (idleAudio != null)
    {
      StopCoroutine(idleAudio);
    }
    chainsawIdle.time = 0;
    chainsawIdle.Stop();
    charge.StopAllCoroutines();
    yield return StartCoroutine(FadeOut());
    spriteRenderer.enabled = false;
    rb.velocity.Set(0, 0);
  }

  public void GoBerserk()
  {
    berserk = true;
    berserkOverlay.SetActive(true);
    StartCoroutine(StopBerserk(10f));
  }

  IEnumerator StopBerserk(float delay = 0f)
  {
    yield return new WaitForSeconds(delay);
    if (delay > 3f)
    {
      yield return StartCoroutine(FlashRed());
    }
    berserk = false;
    berserkOverlay.SetActive(false);
  }

  IEnumerator SetInvincible()
  {
    invincible = true;
    yield return new WaitForSeconds(1f);
    invincible = false;
  }

  void ConvertMousePosToDirection()
  {
    if (idleAudio == null)
    {
      idleAudio = StartCoroutine(LoopIdleAudio());
    }
    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    pos.z = transform.position.z;

    Vector3 deltaPos = pos - transform.position;
    // animator.SetFloat("Magnitude", deltaPos.magnitude);

    Vector3 direction = deltaPos.normalized;
    animator.SetFloat("Horizontal", direction.x);
    animator.SetFloat("Vertical", direction.y);

    rb.AddForce(direction * speed);
    lastDirection = direction;
  }

  IEnumerator LoopIdleAudio()
  {
    while (true)
    {
      chainsawIdle.time = 36;
      chainsawIdle.Play();
      chainsawIdle.SetScheduledEndTime(AudioSettings.dspTime + (42f - 36f));
      yield return new WaitUntil(() => !chainsawIdle.isPlaying);
      // chainsawIdle.time = 61;
      // chainsawIdle.Play();
      // chainsawIdle.SetScheduledEndTime(AudioSettings.dspTime + (78 - 61f));
      // yield return new WaitUntil(() => !chainsawIdle.isPlaying);
    }
  }

  void StopIdleAudio()
  {
    if (idleAudio != null)
    {
      StopCoroutine(idleAudio);
    }
    chainsawIdle.Stop();
  }

  IEnumerator HandleEnemyCollision(Collider2D other)
  {
    StopIdleAudio();
    other.GetComponent<Enemy>().Die();
    yield return new WaitForSeconds(0.5f);
    idleAudio = StartCoroutine(LoopIdleAudio());
  }

  IEnumerator HandleGloryKill(Collider2D other)
  {
    gloryKilling = true;
    StopIdleAudio();
    spriteRenderer.enabled = false;
    rb.velocity = new Vector2(0, 0);
    Instantiate(gloryKill, transform);
    yield return StartCoroutine(other.GetComponent<Enemy>().GloryDeath());
    spriteRenderer.enabled = true;
    gloryKilling = false;
  }

  IEnumerator FlashRed()
  {
    int duration = 2;
    for (int t = 0; t < duration; t++)
    {
      spriteRenderer.color = Color.red;
      yield return new WaitForSeconds(0.1f);
      spriteRenderer.color = Color.white;
      yield return new WaitForSeconds(0.1f);
    }
    spriteRenderer.color = color;
  }

  IEnumerator FadeOut()
  {
    float duration = 1f;
    for (float t = 0; t < duration; t += Time.deltaTime)
    {
      float alpha = Mathf.Lerp(spriteRenderer.color.a, 0, Mathf.Min(1, t / duration));
      if (alpha < 0.1)
      {
        yield break;
      }
      Color newColor = spriteRenderer.color;
      newColor.a = alpha;
      spriteRenderer.color = newColor;
      yield return null;
    }
  }
}
