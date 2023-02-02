using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour {
  Enemy enemy;

  void Start() {
    enemy = gameObject.transform.parent.gameObject.GetComponent<Enemy>();
  }

  void Update() {}

  private void OnCollisionEnter2D(Collision2D col) {
    if (col.gameObject.name == "EnemyCollider") {
      Physics2D.IgnoreCollision(col.gameObject.GetComponent<CapsuleCollider2D>(), GetComponent<CapsuleCollider2D>());
    } else {
      if (col.gameObject.tag != "Ground") {
        Debug.Log(col.gameObject.tag);
      }
      enemy.Collision(col);
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    enemy.Trigger(col);
  }
}
