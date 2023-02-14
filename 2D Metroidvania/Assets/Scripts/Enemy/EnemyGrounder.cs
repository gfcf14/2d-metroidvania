using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrounder : MonoBehaviour {
  void Start() {}
  void Update(){}

  public void OnCollisionEnter2D(Collision2D col) {
    if (Helpers.IsValueInArray(Constants.enemyNonColliderNames, col.gameObject.name.Replace("(Clone)", ""))) {
      Physics2D.IgnoreCollision(col.collider, GetComponent<BoxCollider2D>());
    }
  }
}
