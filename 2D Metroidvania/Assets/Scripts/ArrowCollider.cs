using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollider : MonoBehaviour {
  [System.NonSerialized] GameObject parentObject;
  [System.NonSerialized] Arrow parentArrow;

  void Start() {
    parentObject = transform.parent.gameObject;
    parentArrow = parentObject.GetComponent<Arrow>();
  }

  void Update() {}

  private void OnTriggerEnter2D(Collider2D col) {
    string colliderTag = col.gameObject.tag;

    if (colliderTag == "Ground") {
      if (!parentArrow.hasCollided) {
        parentArrow.hasCollided = true;
        parentArrow.collideTime = Time.time * 1000;
      }
    } else if (colliderTag == "Enemy") {
      Vector2 collisionPoint = col.ClosestPoint(transform.position);

      if (parentArrow.type != "arrow-fire") {
        GameObject pierceEffect = Instantiate(Objects.prefabs["pierce"], collisionPoint, Quaternion.identity);

        if (parentArrow.type == "arrow-poison") {
          pierceEffect.GetComponent<Pierce>().color = Colors.statusColors["poisoned"];
        }
      } else {
        GameObject arrowExplosion = Instantiate(Objects.prefabs["arrow-explosion"], collisionPoint, Quaternion.identity);
      }
    }
  }
}
