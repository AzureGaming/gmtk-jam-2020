using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : EnemyController
{
  public Animator animator;
  public override void Start()
  {
    base.Start();
    StartCoroutine(MovementRoutine());
  }

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

  IEnumerator MovementRoutine()
  {
    while (true)
    {
      yield return new WaitForSeconds(2f);
      Vector3 origPos = transform.position;
      int randomDirection = Random.Range(0, 2);
      float offset = 0.05f;
      if (randomDirection == 0)
      {
        animator.SetFloat("Blend", 1f);
        for (float t = 0; t < 0.5f; t += Time.deltaTime)
        {
          // move left
          Vector3 pos = transform.position;
          pos.x = Mathf.Lerp(origPos.x, origPos.x + offset, Mathf.Min(1, t / 0.5f));
          transform.position = pos;
          yield return null;
        }
        animator.SetFloat("Blend", 0.7f);
      }
      else
      {
        animator.SetFloat("Blend", 0f);
        for (float t = 0; t < 0.5f; t += Time.deltaTime)
        {
          // move left
          Vector3 pos = transform.position;
          pos.x = Mathf.Lerp(origPos.x, origPos.x - offset, Mathf.Min(1, t / 0.5f));
          transform.position = pos;
          yield return null;
        }
        animator.SetFloat("Blend", 0.4f);
      }
    }
  }
}
