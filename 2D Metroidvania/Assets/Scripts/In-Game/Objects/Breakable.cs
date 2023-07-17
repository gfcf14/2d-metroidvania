using System;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
  [SerializeField] public string type;
  [SerializeField] public string item;
  [SerializeField] public string material;

  [SerializeField] public bool isGrounded;

  [System.NonSerialized] public bool isBreaking = false;
  [System.NonSerialized] public List<GameObject> carriedDroppables = new List<GameObject>();

  private Animator anim;
  private SpriteRenderer spriteRenderer;
  private Rigidbody2D body;
  private BoxCollider2D breakableCollider;
  void Start() {
    body = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    breakableCollider = GetComponent<BoxCollider2D>();
    spriteRenderer.sprite = Sprites.breakableSprites[type];

    if (type == "vase") {
      spriteRenderer.color = Colors.vaseColors[material == "" ? "brass" : material];
    }

    breakableCollider.offset = Objects.breakableSizes[type].offset;
    breakableCollider.size = Objects.breakableSizes[type].size;

    gameObject.AddComponent<Animator>();
    anim = GetComponent<Animator>();
    anim.runtimeAnimatorController = Objects.animationControllers["breakable"];

    if (item == "") {
      throw new Exception("No item declared for breakable in " + transform.parent.gameObject.name + ". Please declare an item for proper use.");
    }
  }

  void Update() {
    // ensure that, if falling, the droppables on top move again
    if (body != null) {
      isGrounded = CheckIfGrounded();

      if (isGrounded) {
        if (gameObject.layer == LayerMask.NameToLayer("Dropping")) {
          gameObject.layer = LayerMask.NameToLayer("Objects");
        }
      } else {
        if (!isGrounded && gameObject.layer == LayerMask.NameToLayer("Objects")) {
          gameObject.layer = LayerMask.NameToLayer("Dropping");

          if (carriedDroppables.Count > 0) {
            RemoveCarriedDroppables();
          }
        }
      }
    }
  }

  bool CheckIfGrounded() {
    Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + (spriteRenderer.bounds.size.y / 2)), breakableCollider.size, 0f);

    foreach(Collider2D currentCollider in colliders) {
      if (currentCollider != breakableCollider) {
        if (currentCollider.gameObject.name == "Ground" || transform.position.y > currentCollider.gameObject.transform.position.y) {
          return true;
        }
      }
    }

    return false;
  }

  private void OnCollisionEnter2D(Collision2D col) {
    if (col.gameObject.tag == "Breakable" && Helpers.IsValueInArray(Constants.stackableBreakables, col.gameObject.GetComponent<Breakable>().type) && !Helpers.IsValueInArray(Constants.stackableBreakables, type)) {
      throw new Exception("Breakable objects that are not Barrels or Boxes should not stack with anything");
    }

    if (col.gameObject.tag == "Item" || col.gameObject.name == "ProximityCheck") {
      Physics2D.IgnoreCollision(col.gameObject.GetComponent<PolygonCollider2D>(), breakableCollider);
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    // since these objects are to go on top of other same objects, change the transform.position z value to render on top
    if (col.gameObject.tag == "Breakable" && !Helpers.IsValueInArray(Constants.stackableBreakables, type)) {
      Destroy(body);
      GetComponent<BoxCollider2D>().isTrigger = true;
    }

    if (col.gameObject.tag == "Weapon" && !isBreaking) {
      isBreaking = true;
      Destroy(body);
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
