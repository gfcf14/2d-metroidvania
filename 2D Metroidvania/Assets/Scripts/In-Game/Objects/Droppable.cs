using UnityEngine;

public class Droppable : MonoBehaviour {
  [SerializeField] public string key;
  [SerializeField] public GameObject room;

  [SerializeField] public bool canBePicked = false;
  [SerializeField] public bool isDropping = false;
  [SerializeField] public bool isDropped = false;
  [SerializeField] public bool isIdle = false;
  [SerializeField] public bool isFlickering = false;
  [SerializeField] public int collisionCounter = 0;
  [System.NonSerialized] MoneyItem moneyItem;
  [System.NonSerialized] public float timer = 0;
  [System.NonSerialized] public float maxIdleTime = 10000;
  [System.NonSerialized] public float maxFlickerTime = 5000;
  [System.NonSerialized] public float collisionY = 0;
  [System.NonSerialized] public Flicker flickerEffect;
  [System.NonSerialized] SpriteRenderer droppableSprite;
  [System.NonSerialized] Sprite spriteHolder;

  private Animator anim;

  void Start() {
    flickerEffect = GetComponent<Flicker>();
    droppableSprite = GetComponent<SpriteRenderer>();

    // if a room has been assigned, put the droppable in it to be deleted on exit
    // if there is no room, the only way to delete it is to touch it
    if (room) {
      transform.parent.SetParent(room.transform);
    }

    anim = GetComponent<Animator>();

    if (key.Contains("money")) {
      moneyItem = Objects.moneyItems[key];

      droppableSprite.sprite = moneyItem.image;
      spriteHolder = moneyItem.image;

      if (moneyItem.increment >= 1000) {
        GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
      }

      GetComponent<CapsuleCollider2D>().size = moneyItem.size;
    } else {
     droppableSprite.sprite = Sprites.droppableSprites[key];
     spriteHolder = Sprites.droppableSprites[key];
    }

    if (isDropped) {
      anim.Play("droppable-rise");
    } else {
      FinishAnim();
      // GetComponent<SpriteRenderer>().sprite = null;
    }
  }

  void Update() {
    if (flickerEffect != null) {
      if (isIdle) {
        if (Helpers.ExceedsTime(timer, maxIdleTime)) {
          flickerEffect.enabled = true;
          timer = Time.time * 1000;
          isFlickering = true;
          isIdle = false;
        }
      } else if (isFlickering) {
        if (Helpers.ExceedsTime(timer, maxFlickerTime)) {
          Destroy(transform.parent.gameObject);
        }
      }
    }

    if (collisionCounter > 0) {
      isDropping = false;

      if (timer == 0) {
        timer = Time.time * 1000;
        isIdle = true;
      }
    } else {
      isDropping = true;
    }
  }

  void FixedUpdate() {
    if (isDropping) {
      transform.position = new Vector2(transform.position.x, transform.position.y - 0.2f);
    } else {
      if (collisionY != 0 && transform.position.y != collisionY) {
        transform.position = new Vector2(transform.position.x, collisionY);
      }
    }
  }

  void LateUpdate() {
    if (droppableSprite.sprite == null) {
      droppableSprite.sprite = spriteHolder;
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    string gameObjectTag = col.gameObject.tag;

    if (gameObjectTag == "Hero" && canBePicked) {
      DestroyDroppable(col.gameObject.GetComponent<Hero>());
    }

    if (canBePicked && gameObjectTag == "Ground" || gameObjectTag == "Breakable" && Helpers.IsValueInArray(Constants.stackableBreakables, col.gameObject.GetComponent<Breakable>().type)) {
      ContactPoint2D[] contacts = new ContactPoint2D[10];
      int contactCount = col.GetContacts(contacts);

      for (int i = 0; i < contactCount; i++) {
        ContactPoint2D contact = contacts[i];
        if (contact.point.y <= transform.position.y) { // Trigger happened at or below the current object's position (bottom contact)
          collisionCounter++;
          collisionY = col.ClosestPoint(transform.position).y + (droppableSprite.bounds.size.y / 2);

          if (gameObjectTag == "Breakable") {
            col.gameObject.GetComponent<Breakable>().carriedDroppables.Add(gameObject);
          }
          break;
        }
      }
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
      bool displayMoney = key.Contains("money");
      hero.infoCanvas.GetComponent<InfoCanvas>().Display(displayMoney ? moneyItem.text : Objects.pauseItems[key].name);
    }

    Destroy(transform.parent.gameObject);
  }

  public void FinishAnim() {
    canBePicked = true;
    anim.enabled = false;
  }

  public void DecrementCollision() {
    collisionCounter--;
    if (collisionCounter == 0) {
      collisionY = 0;
    }
  }
}
