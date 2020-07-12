using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawHurt : MonoBehaviour
{
  public SpriteRenderer spriteRenderer;
  Color color;

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Wall")
    {
      StartCoroutine(FlashRed());
    }
  }

  IEnumerator FlashRed()
  {
    int duration = 2;
    for (int t = 0; t < duration; t++)
    {
      spriteRenderer.color = Color.red;
      yield return new WaitForSeconds(0.1f);
      spriteRenderer.color = Color.white;
      yield return new WaitForSeconds(0.1f);
    }
    spriteRenderer.color = color;
  }

  IEnumerator FadeOut()
  {
    float duration = 1f;
    for (float t = 0; t < duration; t += Time.deltaTime)
    {
      float alpha = Mathf.Lerp(spriteRenderer.color.a, 0, Mathf.Min(1, t / duration));
      if (alpha < 0.1)
      {
        yield break;
      }
      Color newColor = spriteRenderer.color;
      newColor.a = alpha;
      spriteRenderer.color = newColor;
      yield return null;
    }
  }
}
