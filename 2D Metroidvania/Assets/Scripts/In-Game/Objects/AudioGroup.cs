using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGroup : MonoBehaviour {
  void Start() {}

  void Update() {
    if (transform.childCount == 0) {
      Destroy(gameObject);
    }
  }
}
