using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : EnemyController
{
  public Animator animator;
  public Rigidbody2D rb;

  public override void Start()
  {
    base.Start();
    StartCoroutine(MovementRoutine());
  }

  IEnumerator MovementRoutine()
  {
    while (true)
    {
      yield return new WaitForSeconds(1f);
      Vector3 origPos = transform.position;
      int randomDirection = Random.Range(0, 2);
      float offset = 1f;
      float duration = 1f;
      if (randomDirection == 0)
      {
        // move right
        animator.SetFloat("Horizontal", 1f);
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
          Vector3 pos = transform.position;
          pos.x = Mathf.Lerp(origPos.x, origPos.x + offset, Mathf.Min(1, t / duration));
          rb.MovePosition(new Vector2(pos.x, pos.y));
          yield return null;
        }
      }
      else
      {
        // move left
        animator.SetFloat("Horizontal", 0f);
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
          Vector3 pos = transform.position;
          pos.x = Mathf.Lerp(origPos.x, origPos.x - offset, Mathf.Min(1, t / duration));
          rb.MovePosition(new Vector2(pos.x, pos.y));
          yield return null;
        }
      }
    }
  }
}
