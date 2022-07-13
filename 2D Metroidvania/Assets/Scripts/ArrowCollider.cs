using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollider : MonoBehaviour {
  void Start() {}

  void Update() {}

  private void OnTriggerEnter2D(Collider2D col) {
    string colliderTag = col.gameObject.tag;

    if (colliderTag == "Ground") {
      GameObject parentObject = transform.parent.gameObject;
      Arrow parentArrow = parentObject.GetComponent<Arrow>();

      if (!parentArrow.hasCollided) {
        parentArrow.hasCollided = true;
        parentArrow.collideTime = Time.time * 1000;
      }
    }
  }
}
