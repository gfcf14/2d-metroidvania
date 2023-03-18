using System.Linq;
using UnityEngine;

public class Droppable : MonoBehaviour {
  [SerializeField] public string key;
  [System.NonSerialized] public bool isDropped = false;
  [System.NonSerialized] float originTime = 0;
  [System.NonSerialized] float maxRiseTime = 350;
  [System.NonSerialized] MoneyItem moneyItem;

  void Start() {
    if (key.Contains("money")) {
      moneyItem = Objects.moneyItems[key];

      GetComponent<SpriteRenderer>().sprite = moneyItem.image;

      if (moneyItem.increment >= 1000) {
        GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
      }

      GetComponent<CapsuleCollider2D>().size = moneyItem.size;
    } else {
      GetComponent<SpriteRenderer>().sprite = Sprites.droppableSprites[key];
    }

    if (isDropped) {
      originTime = Time.time * 1000;
      GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }
  }

  void Update() {
    if (isDropped) {
      float currentTime = Time.time * 1000;

      if (currentTime > originTime + maxRiseTime) {
        if (GetComponent<CapsuleCollider2D>().isTrigger == false) {
          GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
      } else {
        // raises the item for a bit and slows down as the currentTime approaches the limit
        transform.position = new Vector2(transform.position.x, transform.position.y + (((maxRiseTime + originTime) - currentTime) / 15000));
      }
    }
  }

  private void OnCollisionEnter2D(Collision2D col) {
    if (col.gameObject.tag == "Ground") {
      Destroy(GetComponent<Rigidbody2D>());
      GetComponent<CapsuleCollider2D>().isTrigger = true;
    // } else if (col.gameObject.tag == "Hero") {
    //   DestroyDroppable(col.gameObject.GetComponent<Hero>());
    } else if (Helpers.IsValueInArray(Constants.droppableNonColliderTags, col.collider.tag) || Helpers.IsValueInArray(Constants.droppableNonColliderNames, col.collider.name)) {
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

    Destroy(gameObject);
  }
}
