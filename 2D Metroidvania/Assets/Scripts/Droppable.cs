using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droppable : MonoBehaviour {
  [SerializeField] public string key;

  void Start() {
    GetComponent<SpriteRenderer>().sprite = Sprites.droppableSprites[key];
  }

  void Update() {}

  private void OnCollisionEnter2D(Collision2D col) {
    if (col.gameObject.tag == "Ground") {
      Destroy(GetComponent<Rigidbody2D>());
      GetComponent<CapsuleCollider2D>().isTrigger = true;
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    if (col.gameObject.tag == "Hero") {
      Hero hero = col.gameObject.GetComponent<Hero>();
      Item currItem = Helpers.GetItemFromList(hero.items, key);

      if (currItem == null) { // if not found, the item must be added
        hero.items.Add(new Item(key, 1));
      } else { // if found, the item is incremented
        currItem.amount++;
      }

      // TODO: activate UI for a few seconds with the name

      Destroy(gameObject);
    }
  }
}
