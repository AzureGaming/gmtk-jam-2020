using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  Vector3 origPos;
  // Start is called before the first frame update
  void Start()
  {
    origPos = transform.position;
  }

  public void Reset()
  {
    transform.position = origPos;
    GetComponent<CameraFollow>().chainsaw = FindObjectOfType<ChainsawController>().gameObject.transform;
  }
}
