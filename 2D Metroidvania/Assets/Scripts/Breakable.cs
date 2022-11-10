using System;
using UnityEngine;

public class Breakable : MonoBehaviour {
  [SerializeField] public string type;
  [SerializeField] public string item;
  [SerializeField] public string material;

  [System.NonSerialized] public bool isBreaking = false;

  private Animator anim;
  private SpriteRenderer spriteRenderer;
  void Start() {
    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = Sprites.breakableSprites[type];

    if (type == "vase") {
      spriteRenderer.color = Colors.vaseColors[material == "" ? "brass" : material];
    }

    GetComponent<BoxCollider2D>().offset = Objects.breakableSizes[type].offset;
    GetComponent<BoxCollider2D>().size = Objects.breakableSizes[type].size;

    gameObject.AddComponent<Animator>();
    anim = GetComponent<Animator>();
    anim.runtimeAnimatorController = Objects.animationControllers["breakable"];
  }

  void Update() {}

  private void OnCollisionEnter2D(Collision2D col) {
    if (col.gameObject.tag == "Ground" && !Helpers.IsValueInArray(Constants.stackableBreakables, type)) {
      Destroy(GetComponent<Rigidbody2D>());
      GetComponent<BoxCollider2D>().isTrigger = true;
    }

    if (col.gameObject.tag == "Breakable" && Helpers.IsValueInArray(Constants.stackableBreakables, col.gameObject.GetComponent<Breakable>().type) && !Helpers.IsValueInArray(Constants.stackableBreakables, type)) {
      throw new Exception("Breakable objects that are not Barrels or Boxes should not stack with anything");
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    // since these objects are to go on top of other same objects, change the transform.position z value to render on top
    if (col.gameObject.tag == "Breakable" && !Helpers.IsValueInArray(Constants.stackableBreakables, type)) {
      Destroy(GetComponent<Rigidbody2D>());
      GetComponent<BoxCollider2D>().isTrigger = true;
    }

    if (col.gameObject.tag == "Weapon" && !isBreaking) {
      isBreaking = true;
      Destroy(GetComponent<Rigidbody2D>());
      GetComponent<BoxCollider2D>().isTrigger = true;

      Vector2 itemOrigin = new Vector2(transform.position.x, transform.position.y + (spriteRenderer.bounds.size.y / 2));
      GameObject droppedItem = Instantiate(Objects.prefabs["droppable"], itemOrigin, Quaternion.identity);
      droppedItem.GetComponent<Droppable>().key = "moonlight-pendant"; // TODO: come up with a random system to add the item instead of hardcoding
      droppedItem.GetComponent<Droppable>().isDropped = true;

      anim.Play("breakable-" + type);
    }
  }

  public void DestroyBreakable() {
    Destroy(gameObject);
  }
}
