using UnityEngine;

public class AirEdgeCheck : MonoBehaviour {
  BoxCollider2D airEdgeCheckCollider;
  // using a single dimension since the collider is a square
  float colliderDimension = 0;
  float rayLength = 1;
  void Start() {
    airEdgeCheckCollider = GetComponent<BoxCollider2D>();
    colliderDimension = airEdgeCheckCollider.size.x;
    rayLength = colliderDimension - 0.01f;
  }
  void Update() {}

  public bool IntersectsWithGround() {
    Collider2D[] colliders = Physics2D.OverlapBoxAll(airEdgeCheckCollider.bounds.center, airEdgeCheckCollider.bounds.size, 0f);

    foreach (Collider2D col in colliders) {
      if (col.CompareTag("Ground")) {
        return true;
      }
    }

    return false;
  }

  public void CheckStepOver(Hero hero, int direction) {
    Vector2 rayOrigin = new Vector2(transform.position.x + (colliderDimension * 1.5f * direction) + (hero.heroWidth * direction * -1), transform.position.y + (colliderDimension / 2));
    Vector2 rayDirection = Vector2.down;

    RaycastHit2D differenceCast = Physics2D.Raycast(rayOrigin, rayDirection, rayLength);
    Debug.DrawRay(rayOrigin, rayDirection.normalized * rayLength, Colors.raycastColors["jump"]);

    // TODO: for now this assumes that if the cast collider is null, that the air edge check is intersecting a really tall wall, so bump is obligatory
    //       ensure a better check is made for when the collider is null if possible
    if (!hero.isFightingBoss && differenceCast.collider == null) {
      hero.Bump(bumpX: hero.heroWidth / 6);
    } else {
      if (differenceCast.collider != null && differenceCast.collider.tag == "Ground" && differenceCast.distance > 0) {
        float yDistance = Mathf.Abs(differenceCast.point.y - rayOrigin.y);

        // if there is a gap, then we can have the player step over
        if (yDistance > 0.01f) {
            float stepOverHeight = colliderDimension - yDistance;
            hero.StepOver(stepOverHeight);
            Debug.Log("step over");
        } else {
            // TODO: implement some bump logic here to avoid having the player stick to the "wall" and fall down slowly
            Debug.Log("bump (checked)");
            if (!hero.isFightingBoss) {
              hero.Bump(bumpX: hero.heroWidth / 6);
            }
        }
      }
    }
  }
}
