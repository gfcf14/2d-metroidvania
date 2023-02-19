using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntryCheck : MonoBehaviour {
  void Start() {}
  void Update() {}

  private void OnCollisionEnter2D(Collision2D other) {
    if (other.collider.tag == "Ground") {
      Physics2D.IgnoreCollision(other.collider, GetComponent<BoxCollider2D>());
    }
  }
}
