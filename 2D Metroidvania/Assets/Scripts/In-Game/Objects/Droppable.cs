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
  [System.NonSerialized] public Flicker flickerEffect;

  private Animator anim;

  void Start() {
    flickerEffect = transform.Find("Image").gameObject.GetComponent<Flicker>();

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
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    string gameObjectTag = col.gameObject.tag;

    if (gameObjectTag == "Hero" && canBePicked) {
      DestroyDroppable(col.gameObject.GetComponent<Hero>());
    }

    if (gameObjectTag == "Ground" || gameObjectTag == "Breakable" && Helpers.IsValueInArray(Constants.stackableBreakables, col.gameObject.GetComponent<Breakable>().type)) {
      collisionCounter++;

      if (gameObjectTag == "Breakable") {
        col.gameObject.GetComponent<Breakable>().carriedDroppables.Add(gameObject);
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
      hero.infoCanvas.GetComponent<InfoCanvas>().Display(key.Contains("money") ? moneyItem.text : Objects.pauseItems[key].name);
    }

    Destroy(transform.parent.gameObject);
  }

  public void FinishAnim() {
    canBePicked = true;
    anim.enabled = false;
  }
}
