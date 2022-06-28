using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableCollider : MonoBehaviour {
    void Start() {}
    void Update() {}

    private void OnTriggerEnter2D(Collider2D col) {
      string colliderTag = col.gameObject.tag;

      if (colliderTag == "Ground") {
        GameObject parentObject = transform.parent.gameObject;
        string throwableWeapon = parentObject.GetComponent<SpriteRenderer>().sprite.name;

        if (throwableWeapon == "lance") {
          Throwable parentThrowable = parentObject.GetComponent<Throwable>();

          parentThrowable.hasCollided = true;
          parentThrowable.collideTime = Time.time * 1000;
        }
      }
    }
}
