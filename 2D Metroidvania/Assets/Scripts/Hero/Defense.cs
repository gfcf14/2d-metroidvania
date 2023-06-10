using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour {
  [System.NonSerialized] public bool isFacingLeft;

  void Start() {
    transform.localScale = new Vector3(isFacingLeft ? -1 : 1, 1, 1);
  }
  void Update() {}

  public void DestroyDefense() {
    Destroy(gameObject);
  }
}
