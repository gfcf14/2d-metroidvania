using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
  GameObject hero;

  // Start is called before the first frame update
  void Start() {
    hero = GameObject.FindGameObjectWithTag("Hero");
  }

  // Update is called once per frame
  void Update() { }

  private void OnTriggerEnter2D(Collider2D col) {
    if (col.gameObject.tag == "Enemy") {
      Hero heroObject = hero.GetComponent<Hero>();
      float enemyTopBounds = col.transform.position.y + col.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;

      if (heroObject.isDropKicking) {
        float heroBottomBounds = GameObject.FindGameObjectWithTag("Hero").transform.position.y;

        if (Mathf.Abs(heroBottomBounds - enemyTopBounds) <= 0.1) {
          heroObject.Jump(true);
        }
      }
    }
  }
}
