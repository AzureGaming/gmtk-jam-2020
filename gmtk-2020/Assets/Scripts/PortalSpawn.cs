using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawn : MonoBehaviour
{
  public SpriteRenderer spriteRenderer;
  public BoxCollider2D triggerCollider;
  bool donePortalAnimation = false;

  private void Start()
  {
    triggerCollider.enabled = false;
  }

  public IEnumerator PortalDone()
  {
    yield return new WaitUntil(() => donePortalAnimation == true);
    triggerCollider.enabled = true;
    spriteRenderer.enabled = false;
    Destroy(gameObject, 1.5f);
  }

  public void ShowEnemy()
  {
    donePortalAnimation = true;
  }
}
