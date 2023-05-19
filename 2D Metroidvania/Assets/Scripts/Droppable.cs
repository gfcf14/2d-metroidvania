using System.Linq;
using UnityEngine;

public class Droppable : MonoBehaviour {
  [SerializeField] public string key;
  [SerializeField] public GameObject room;
  [System.NonSerialized] public bool isDropped = false;
  [System.NonSerialized] MoneyItem moneyItem;

  private Animator anim;

  void Start() {
    // if a room has been assigned, put the droppable in it to be deleted on exit
    // if there is no room, the only way to delete it is to touch it
    if (room) {
      transform.parent.SetParent(room.transform);
    }

    anim = GetComponent<Animator>();

    if (key.Contains("money")) {
      moneyItem = Objects.moneyItems[key];

      transform.Find("Image").gameObject.GetComponent<SpriteRenderer>().sprite = moneyItem.image;

      if (moneyItem.increment >= 1000) {
        GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
      }

      GetComponent<CapsuleCollider2D>().size = moneyItem.size;
    } else {
     transform.Find("Image").gameObject.GetComponent<SpriteRenderer>().sprite = Sprites.droppableSprites[key];
    }

    if (isDropped) {
      anim.Play("droppable-rise");
    } else {
      FinishAnim();
      GetComponent<SpriteRenderer>().sprite = null;
    }
  }

  void Update() {}

  private void OnCollisionEnter2D(Collision2D col) {
    if (col.gameObject.tag == "Ground") {
      Destroy(GetComponent<Rigidbody2D>());
      GetComponent<CapsuleCollider2D>().isTrigger = true;
    // } else if (col.gameObject.tag == "Hero") {
    //   DestroyDroppable(col.gameObject.GetComponent<Hero>());
    } else { // if (Helpers.IsValueInArray(Constants.droppableNonColliderTags, col.collider.tag) || Helpers.IsValueInArray(Constants.droppableNonColliderNames, col.collider.name)) {
      Physics2D.IgnoreCollision(col.collider, GetComponent<BoxCollider2D>());
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    if (col.gameObject.tag == "Hero") {
      DestroyDroppable(col.gameObject.GetComponent<Hero>());
    }
  }

  public void DestroyDroppable(Hero hero) {
    if (key.Contains("money")) {
      hero.gold += moneyItem.increment;
    } else {
      Item currItem = Helpers.GetItemFromList(hero.items, key);

      if (currItem == null) { // if not found, the item must be added
        hero.items.Add(new Item(key, 1));
      } else { // if found, the item is incremented
        currItem.amount++;
      }
    }

    if (Settings.showItemInfo) {
      hero.infoCanvas.GetComponent<InfoCanvas>().Display(key.Contains("money") ? moneyItem.text : Objects.pauseItems[key].name);
    }

    Destroy(transform.parent.gameObject);
  }

  public void FinishAnim() {
    anim.enabled = false;
  }
}
