using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloryKill : MonoBehaviour
{
  public AudioSource death;
  public SpriteRenderer spriteRenderer;

  bool doneAnimation = false;

  public void PlayDeathSound()
  {
    death.Play();
  }

  public void DoneAnimation()
  {
    doneAnimation = true;
    spriteRenderer.enabled = false;
    Destroy(gameObject, 1.5f);
  }

  public bool IsAnimationDone()
  {
    return doneAnimation;
  }
}
