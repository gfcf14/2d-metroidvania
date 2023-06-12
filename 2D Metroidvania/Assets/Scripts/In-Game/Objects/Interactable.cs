using System;
using UnityEngine;

public class Interactable : MonoBehaviour {
  [SerializeField] public bool isFacingLeft = true;
  [SerializeField] public string item;
  [System.NonSerialized] public bool isOpen = false;
  private Animator anim;
  private SpriteRenderer spriteRenderer;

  void Start() {
    anim = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();

    if (!isFacingLeft) {
      transform.localScale = new Vector2(-1, 1);
    }

    if (item == "") {
      throw new Exception("No item declared for interactable in " + transform.parent.gameObject.name + ". Please declare an item for proper use.");
    }
  }

  void Update() {
    anim.SetBool("isOpen", isOpen);
  }

  private void OnCollisionEnter2D(Collision2D col) {
    if (col.collider.name == "Hero" && !isOpen) {
      isOpen = true;
    }
  }

  public void releaseTreasure() {
    Destroy(GetComponent<Rigidbody2D>());
    GetComponent<BoxCollider2D>().enabled = false;

    // TODO: define what items will Interactables drop
    GameObject.Find("UnityHelpers").gameObject.GetComponent<InGame>().InstantiatePrefab("droppable", item, transform.parent.gameObject, transform, spriteRenderer);
  }
}