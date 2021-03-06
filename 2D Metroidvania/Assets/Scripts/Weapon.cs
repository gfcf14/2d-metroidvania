using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
  Hero hero;

  void Start() {
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
  }

  void Update() { }

  private void OnTriggerEnter2D(Collider2D col) {
    if (col.gameObject.tag == "Enemy") {
      Patroller enemyCollided = col.gameObject.GetComponent<Patroller>();

      float enemyTopBounds = col.transform.position.y + col.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;

      if (hero.isDropKicking) {
        float heroBottomBounds = GameObject.FindGameObjectWithTag("Hero").transform.position.y;

        if (Mathf.Abs(heroBottomBounds - enemyTopBounds) <= 0.1) {
          hero.Jump(true);
        }
      }
    }
  }
}
