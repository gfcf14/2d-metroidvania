using System;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
  [SerializeField] public string type;
  [SerializeField] public string item;
  [SerializeField] public string material;

  [System.NonSerialized] public bool isBreaking = false;
  [System.NonSerialized] public List<GameObject> carriedDroppables = new List<GameObject>();

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

    if (item == "") {
      throw new Exception("No item declared for breakable in " + transform.parent.gameObject.name + ". Please declare an item for proper use.");
    }
  }

  void Update() {}

  private void OnCollisionEnter2D(Collision2D col) {
    if (col.gameObject.tag == "Ground" && !Helpers.IsValueInArray(Constants.stackableBreakables, type)) {
      GetComponent<Rigidbody2D>().gravityScale = 0;
      GetComponent<BoxCollider2D>().isTrigger = true;
    }

    if (col.gameObject.tag == "Breakable" && Helpers.IsValueInArray(Constants.stackableBreakables, col.gameObject.GetComponent<Breakable>().type) && !Helpers.IsValueInArray(Constants.stackableBreakables, type)) {
      throw new Exception("Breakable objects that are not Barrels or Boxes should not stack with anything");
    }

    if (col.gameObject.tag == "Item" || col.gameObject.name == "ProximityCheck") {
      Physics2D.IgnoreCollision(col.gameObject.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>());
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

      GameObject.Find("UnityHelpers").gameObject.GetComponent<InGame>().InstantiatePrefab("droppable", item, transform.parent.gameObject, transform, spriteRenderer);

      GameObject parentObject = col.transform.parent.gameObject;
      if (parentObject.name.Contains("Throwable")) {
        Throwable parentThrowable = parentObject.GetComponent<Throwable>();
        string weaponWielded = parentThrowable.type;
        Transform parentTransform = parentObject.GetComponent<Transform>();

        if(Helpers.IsNonBouncingThrowable(weaponWielded)) {
          parentThrowable.bounceX = parentTransform.position.x;
          parentThrowable.bounceY = parentTransform.position.y;
          parentThrowable.mustBounce = true;
          parentThrowable.transitionIncrement = 0;
        }

        parentThrowable.collideTime = Time.time * 1000;
        parentThrowable.hasCollided = true;
        parentThrowable.maxEllapsedCollideTime = 1000f;
      }

      anim.Play("breakable-" + type);
    }
  }

  public void RemoveCarriedDroppables() {
    foreach (GameObject droppable in carriedDroppables) {
      droppable.GetComponent<Droppable>().DecrementCollision();
    }
  }

  public void DestroyBreakable() {
    Destroy(gameObject);
  }
}
