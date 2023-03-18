using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
  [System.NonSerialized] public bool isOpen = false;
  private Animator anim;
  private SpriteRenderer spriteRenderer;

  void Start() {
    anim = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void Update() {
    anim.SetBool("isOpen", isOpen);
  }

  private void OnCollisionEnter2D(Collision2D col) {
    if (col.collider.name == "ChestOpener" && !isOpen) {
      isOpen = true;
    }
  }

  public void releaseTreasure() {
    Destroy(GetComponent<Rigidbody2D>());
    GetComponent<BoxCollider2D>().enabled = false;

    Vector2 itemOrigin = new Vector2(transform.position.x, transform.position.y + (spriteRenderer.bounds.size.y / 2));
    GameObject droppedItem = Instantiate(Objects.prefabs["droppable"], itemOrigin, Quaternion.identity);
    droppedItem.GetComponent<Droppable>().key = "moonlight-pendant"; // TODO: come up with a random system to add the item instead of hardcoding
    droppedItem.GetComponent<Droppable>().isDropped = true;
  }
}
