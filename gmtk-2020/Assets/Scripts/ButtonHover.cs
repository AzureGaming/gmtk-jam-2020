using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHover : MonoBehaviour
{
  public GameObject skull;

  public void OnHover()
  {
    skull.SetActive(true);
  }

  public void OnHoverExit()
  {
    skull.SetActive(false);
  }
}
