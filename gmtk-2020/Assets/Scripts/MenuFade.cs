using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFade : MonoBehaviour
{

  public Image image;
  Color origColor;

  private void Start()
  {
    origColor = image.color;
  }

  void Update()
  {
    float alpha = Mathf.PingPong(Time.time, 1);
    Color newColor = image.color;
    newColor.a = alpha;
    image.color = newColor;
  }
}
