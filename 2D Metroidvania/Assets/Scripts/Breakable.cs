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
      GetComponent<Rigidbody2D>().gravityScale = 0;
      GetComponent<BoxCollider2D>().isTrigger = true;
    }

    if (col.gameObject.tag == "Breakable" && Helpers.IsValueInArray(Constants.stackableBreakables, col.gameObject.GetComponent<Breakable>().type) && !Helpers.IsValueInArray(Constants.stackableBreakables, type)) {
      throw new Exception("Breakable objects that are not Barrels or Boxes should not stack with anything");
    }

    if (col.gameObject.tag == "Item" || col.gameObject.name == "BossEntryCheck") {
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

      // TODO: define what items will Breakables drop
      GameObject.Find("UnityHelpers").gameObject.GetComponent<InGame>().InstantiatePrefab("droppable", "moonlight-pendant", transform, spriteRenderer);

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

  public void DestroyBreakable() {
    Destroy(gameObject);
  }
}
