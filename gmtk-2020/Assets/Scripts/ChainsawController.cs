using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawController : MonoBehaviour
{
  public Rigidbody2D rb;
  public Animator animator;

  float speed = 10f;
  bool isDead = false;

  public void Die()
  {
    Destroy(this.gameObject);
  }

  private void FixedUpdate()
  {
    if (rb != null)
    {
      ConvertMousePosToDirection();
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Enemy")
    {
      other.GetComponent<Enemy>().Die();
    }
  }

  void ConvertMousePosToDirection()
  {
    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    pos.z = transform.position.z;

    Vector3 deltaPos = pos - transform.position;
    animator.SetFloat("Magnitude", deltaPos.magnitude);

    Vector3 direction = deltaPos.normalized;
    animator.SetFloat("Horizontal", direction.x);
    animator.SetFloat("Vertical", direction.y);

    rb.AddForce(direction * speed);
  }
}
