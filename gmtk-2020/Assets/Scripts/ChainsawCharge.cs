using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawCharge : MonoBehaviour
{
  public Rigidbody2D rb;
  public Animator animator;
  public AudioSource revSound;

  float speed = 20f;
  bool audioPlaying = false;

  public void Attack(Vector3 direction)
  {
    if (direction != null)
    {
      if (!audioPlaying)
      {
        StartCoroutine(RevSound());
      }
      rb.AddForce(direction * speed);
    }
  }

  public void UnRev()
  {
    revSound.Stop();
    audioPlaying = false;
    StopAllCoroutines();
  }

  IEnumerator RevSound()
  {
    while (true)
    {
      revSound.time = 46;
      revSound.Play();
      audioPlaying = true;
      revSound.SetScheduledEndTime(AudioSettings.dspTime + (47.8f - 46f));
      yield return new WaitUntil(() => !revSound.isPlaying);
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

  }
}
