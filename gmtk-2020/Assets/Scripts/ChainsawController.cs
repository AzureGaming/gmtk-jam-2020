using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawController : MonoBehaviour
{
  public Rigidbody2D rb;
  public float speed = 1f;

  private void FixedUpdate()
  {
    if (rb != null)
    {
      ApplyInput();
    }
  }

  void ApplyInput()
  {
    rb.AddForce(transform.up * speed);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Enemy")
    {
      other.GetComponent<Enemy>().Die();
    }
  }
}
