using UnityEngine;

public class ThrowableCollider : MonoBehaviour {
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
    GameObject parentObject = transform.parent.gameObject;
    Throwable parentThrowable = parentObject.GetComponent<Throwable>();

    if (colliderTag == "Ground") {
      bool isBottomCollision = IsBottomCollision(col);

      if (Helpers.IsGroundThrowable(parentThrowable.type) && !parentThrowable.hasCollided) {
        if (parentThrowable.type == "bomb") {
          parentThrowable.anim.speed = 0;

          if (!isBottomCollision) {
            parentThrowable.mustFall = true;
          }
        }

        if (Helpers.IsNonBouncingThrowable(parentThrowable.type) || (parentThrowable.type == "bomb" && isBottomCollision)) {
          parentThrowable.hasCollided = true;
          parentThrowable.collideTime = Time.time * 1000;
        }
      }
    } else if (colliderTag == "Hero" && gameObject.tag == "EnemyWeapon") {
      GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().ReceiveThrowable(parentObject, col.ClosestPoint(transform.position));
      parentThrowable.hasCollided = true;
      parentThrowable.collideTime = Time.time * 1000;
      parentThrowable.mustBounce = true;
      parentThrowable.bounceX = parentThrowable.transform.position.x;
      parentThrowable.bounceY = parentThrowable.transform.position.y;
      parentThrowable.transitionIncrement = 0;
    }
  }
}
