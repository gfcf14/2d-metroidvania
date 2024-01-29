using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
  public Hero hero;
  void Start() {
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
  }
  void Update() {}

  private void OnTriggerEnter2D(Collider2D col) {
    GameObject objectColliding = col.gameObject;
    string colName = objectColliding.name;

    if (colName == "DirectionCheck-Front") { // implies a hero front collision with wall when active (jumping or falling)
      if (hero.airEdgeCheckScript.IntersectsWithWalls()) {
        if (hero.isJumping || hero.isFalling) {
          hero.airEdgeCheckScript.CheckStepOver(hero, hero.direction * -1);
        }
      } else {
        if (hero.isJumping || hero.isFalling) {
          hero.Bump(bumpX: hero.heroWidth / 6);
        }
      }
    } else if (colName == "DirectionCheck-Back") { // implies a hero back collision with wall
      hero.Bump(bumpX: -hero.heroWidth / 6, 0, specificBlockDirection: hero.isFacingLeft ? "right" : "left");
    } else {
      Debug.Log("wall collided with " + colName);
    }
  }
}
