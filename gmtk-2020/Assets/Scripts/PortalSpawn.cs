using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawn : MonoBehaviour
{
  public SpriteRenderer spriteRenderer;
  bool donePortalAnimation = false;

  public IEnumerator PortalDone()
  {
    yield return new WaitUntil(() => donePortalAnimation);
    spriteRenderer.enabled = false;
    Destroy(gameObject, 2f);
  }

  public void ShowEnemy()
  {
    donePortalAnimation = true;
  }
}
