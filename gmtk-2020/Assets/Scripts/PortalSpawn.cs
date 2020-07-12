using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawn : MonoBehaviour
{
  public SpriteRenderer spriteRenderer;
  bool donePortalAnimation = false;

  public IEnumerator PortalDone()
  {
    yield return new WaitUntil(() => donePortalAnimation == true);
    spriteRenderer.enabled = false;
    Destroy(gameObject, 1.5f);
  }

  public void ShowEnemy()
  {
    donePortalAnimation = true;
  }
}
