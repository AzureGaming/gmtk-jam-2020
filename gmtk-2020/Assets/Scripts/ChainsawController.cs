using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawController : MonoBehaviour
{
  public Rigidbody2D rb;
  public Animator animator;
  public AudioSource chainsawIdle;
  public ChainsawCharge charge;

  float speed = 10f;
  bool charging = false;
  Coroutine idleAudio;
  Vector3 lastDirection;

  private void Start()
  {
    idleAudio = StartCoroutine(LoopIdleAudio());
  }

  public void Die()
  {
    chainsawIdle.time = 0;
    Destroy(this.gameObject);
  }

  private void FixedUpdate()
  {
    if (rb != null)
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
    if (other.tag == "Enemy")
    {
      StartCoroutine(HandleEnemyCollision(other));
    }
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
}
