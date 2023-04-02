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
      if (parentThrowable.type == "lance" || parentThrowable.type == "knife") {
        parentThrowable.hasCollided = true;
        parentThrowable.collideTime = Time.time * 1000;
        parentThrowable.StopAndFade();
      } else if (parentThrowable.type == "bomb") {
        if (!parentThrowable.isExploding) {
          parentThrowable.bounceX = col.ClosestPoint(transform.position).x;
          parentThrowable.bounceY = col.ClosestPoint(transform.position).y;
          parentThrowable.Explode();
        }
      } else {
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
            // ensures the rigidbody, which enables throwable-to-throwable collision, is removed so the object sticks to the ground
            Destroy(parentObject.GetComponent<Rigidbody2D>());
          }
        }
      }
    } else if ((gameObject.tag == "EnemyWeapon" && Helpers.IsValueInArray(Constants.enemyThrowableBouncers, colliderTag)) || (gameObject.tag == "Weapon" && colliderTag == "EnemyWeapon")) {
      if (colliderTag == "Hero") {
        GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().ReceiveThrowable(parentObject, col.ClosestPoint(transform.position));
      }

      parentThrowable.hasCollided = true;
      parentThrowable.collideTime = Time.time * 1000;
      parentThrowable.mustBounce = true;
      parentThrowable.bounceX = parentThrowable.transform.position.x;
      parentThrowable.bounceY = parentThrowable.transform.position.y;
      parentThrowable.transitionIncrement = 0;

      // ensures the animation starts at the impact point
      if (gameObject.tag == "EnemyWeapon") {
        parentObject.transform.parent.position = new Vector2(parentThrowable.transform.position.x, parentThrowable.transform.position.y);
        parentObject.transform.position = Vector2.zero;
      }
    }
  }
}
