using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableCollider : MonoBehaviour {
  string[] groundColliders = { "lance", "bomb" };


  void Start() {}

  void Update() {}

  bool IsBottomCollision(Collider2D groundCollider) {
    float groundColliderY = groundCollider.gameObject.GetComponent<Transform>().position.y;

    bool acceptableCollisionYDepth = transform.position.y - groundColliderY > -0.2;
    bool withinGroundBoundaries = transform.position.x >= groundCollider.bounds.min.x && transform.position.x <= groundCollider.bounds.max.x;

    return acceptableCollisionYDepth && withinGroundBoundaries;
  }

  private void OnTriggerEnter2D(Collider2D col) {
    string colliderTag = col.gameObject.tag;

    if (colliderTag == "Ground") {
      Transform groundTransform = col.gameObject.GetComponent<Transform>();
      bool isBottomCollision = IsBottomCollision(col);

      GameObject parentObject = transform.parent.gameObject;
      Throwable parentThrowable = parentObject.GetComponent<Throwable>();

      if (System.Array.IndexOf(groundColliders, parentThrowable.type) != -1 && !parentThrowable.hasCollided) {
        if (parentThrowable.type == "bomb") {
          parentThrowable.anim.speed = 0;

          if (!isBottomCollision) {
            parentThrowable.mustFall = true;
          }
        }

        if (parentThrowable.type == "lance" || (parentThrowable.type == "bomb" && isBottomCollision)) {
          parentThrowable.hasCollided = true;
          parentThrowable.collideTime = Time.time * 1000;
        }
      }
    }
  }
}
