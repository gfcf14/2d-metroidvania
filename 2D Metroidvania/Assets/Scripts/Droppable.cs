using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droppable : MonoBehaviour {
  [SerializeField] public string type;

  void Start() {
    GetComponent<SpriteRenderer>().sprite = Sprites.droppableSprites[type];
  }

  void Update() {}

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "Ground") {
      Destroy(GetComponent<Rigidbody2D>());
      GetComponent<CapsuleCollider2D>().isTrigger = true;
    }
  }
}
