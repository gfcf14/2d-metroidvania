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
      if (parentThrowable.type == "lance" || parentThrowable.type == "knife" || parentThrowable.type == "kunai" || parentThrowable.type == "shuriken-4" || parentThrowable.type == "shuriken-6" || parentThrowable.type == "hatchet") {
        parentThrowable.hasCollided = true;
        parentThrowable.collideTime = Time.time * 1000;
        parentThrowable.StopAndFade();
      } else if (parentThrowable.type == "bomb") {
        if (!parentThrowable.isExploding) {
          parentThrowable.bounceX = col.ClosestPoint(transform.position).x;
          parentThrowable.bounceY = col.ClosestPoint(transform.position).y;
          parentThrowable.Explode();
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
