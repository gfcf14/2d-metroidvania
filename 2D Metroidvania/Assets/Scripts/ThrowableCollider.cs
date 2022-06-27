using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableCollider : MonoBehaviour {
    void Start() {}
    void Update() {}

    private void OnTriggerEnter2D(Collider2D col) {
      string colliderTag = col.gameObject.tag;

      if (colliderTag == "Ground") {
        GameObject throwableParent = transform.parent.gameObject;
        string throwableWeapon = throwableParent.GetComponent<SpriteRenderer>().sprite.name;

        if (throwableWeapon == "lance") {
          throwableParent.GetComponent<Throwable>().hasCollided = true;
          throwableParent.GetComponent<Throwable>().collideTime = Time.time * 1000;
        }
      }
    }
}
