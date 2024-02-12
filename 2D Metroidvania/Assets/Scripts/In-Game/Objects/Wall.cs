using UnityEngine;
using UnityEngine.Tilemaps;

public class Wall : MonoBehaviour {
  public Hero hero;
  void Start() {
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
  }
  void Update() {}

  private void OnCollisionEnter2D(Collision2D col) {
    if (col.collider.name == "ProximityCheck") {
      Physics2D.IgnoreCollision(col.collider, GetComponent<TilemapCollider2D>());
    } else {
      Debug.Log("colliding with " + col.collider.name);
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    GameObject objectColliding = col.gameObject;
    string colName = objectColliding.name;

    if (colName == "DirectionCheck-Front" && !hero.isGrounded) { // implies a hero front collision with wall when active (jumping or falling)
      if (hero.airEdgeCheckScript.IntersectsWithWalls()) {
        if (hero.isJumping || hero.isFalling) {
          hero.airEdgeCheckScript.CheckStepOver(hero, hero.direction * -1);
        }
      } else {
        if (hero.isJumping || hero.isFalling) {
          Debug.Log("bump from front");
          // when bumping, finish the jump animation to ensure the player doesn't bump upward
          hero.isJumping = false;
          hero.isFalling = true;
          hero.body.velocity = Vector2.zero;
          hero.Bump(bumpX: (hero.heroWidth * -hero.direction) / 4, specificBlockDirection: hero.isFacingLeft ? "left" : "right");
        }
      }
    } else if (colName == "DirectionCheck-Back" && !hero.isGrounded) { // implies a hero back collision with wall
      Debug.Log("bump from back");
      hero.Bump(bumpX: (-hero.heroWidth * hero.direction) / 4, 0, specificBlockDirection: hero.isFacingLeft ? "right" : "left");
    } else {
      Debug.Log("wall collided with " + colName);
    }
  }
}
