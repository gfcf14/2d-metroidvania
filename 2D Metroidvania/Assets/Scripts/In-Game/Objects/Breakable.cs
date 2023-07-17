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
    Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + breakableCollider.offset.y), breakableCollider.size, 0f);

    // if (gameObject.name == "Breakable (3)") {
    //   DrawRectangle(new Vector2(transform.position.x, transform.position.y + breakableCollider.offset.y), breakableCollider.size);
    // }

    foreach(Collider2D currentCollider in colliders) {
      if (currentCollider != breakableCollider) {
        if (currentCollider.gameObject.name == "Ground" || transform.position.y > currentCollider.gameObject.transform.position.y) {
          return true;
        }
      }
    }

    return false;
  }

  // TODO: find a better place for this rectangle debug
  // void DrawRectangle(Vector2 center, Vector2 size) {
  //   Vector2 halfSize = size / 2f;

  //   Vector3 topLeft = new Vector3(center.x - halfSize.x, center.y + halfSize.y, 0f);
  //   Vector3 topRight = new Vector3(center.x + halfSize.x, center.y + halfSize.y, 0f);
  //   Vector3 bottomRight = new Vector3(center.x + halfSize.x, center.y - halfSize.y, 0f);
  //   Vector3 bottomLeft = new Vector3(center.x - halfSize.x, center.y - halfSize.y, 0f);

  //   Debug.DrawLine(topLeft, topRight, Color.red);
  //   Debug.DrawLine(topRight, bottomRight, Color.red);
  //   Debug.DrawLine(bottomRight, bottomLeft, Color.red);
  //   Debug.DrawLine(bottomLeft, topLeft, Color.red);
  // }

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
      if (droppable != null) {
        droppable.GetComponent<Droppable>().DecrementCollision();
      }
    }
  }

  public void DestroyBreakable() {
    Destroy(gameObject);
  }
}
