using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntryCheck : MonoBehaviour {
  void Start() {}
  void Update() {}

  private void OnCollisionEnter2D(Collision2D other) {
    if (Helpers.IsValueInArray(Constants.bossEntryCheckNonColliderTags, other.collider.tag) || other.collider.name == "Grounder") {
      Physics2D.IgnoreCollision(other.collider, GetComponent<BoxCollider2D>());
    }
  }
}
