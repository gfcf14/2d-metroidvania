using UnityEngine;
using UnityEngine.UI;

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
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    if (col.gameObject.tag == "Hero") {
      Hero hero = col.gameObject.GetComponent<Hero>();

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

      hero.infoCanvas.SetActive(false);
      hero.infoCanvas.transform.Find("ItemContainer").gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text = key.Contains("money") ? moneyItem.text : Objects.pauseItems[key].name;
      hero.infoCanvas.SetActive(true);
      hero.infoCanvas.GetComponent<InfoCanvas>().startTime = Time.time * 1000;

      Destroy(gameObject);
    }
  }
}
