using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollider : MonoBehaviour {
  void Start() {}

  void Update() {}

  private void OnTriggerEnter2D(Collider2D col) {
    string colliderTag = col.gameObject.tag;
    GameObject parentObject = transform.parent.gameObject;

    if (colliderTag == "Ground") {
      Arrow parentArrow = parentObject.GetComponent<Arrow>();

      if (!parentArrow.hasCollided) {
        parentArrow.hasCollided = true;
        parentArrow.collideTime = Time.time * 1000;
      }
    } else if (colliderTag == "Enemy") {
      Vector2 collisionPoint = col.ClosestPoint(transform.position);

      Instantiate(parentObject.GetComponent<Arrow>().pierceObject, collisionPoint, Quaternion.identity);
    }
  }
}
