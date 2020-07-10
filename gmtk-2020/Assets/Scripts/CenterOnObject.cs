using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  public Transform chainsaw;

  // Update is called once per frame
  private void FixedUpdate()
  {
    transform.position = new Vector3(chainsaw.position.x, chainsaw.position.y, transform.position.z);
  }
}
