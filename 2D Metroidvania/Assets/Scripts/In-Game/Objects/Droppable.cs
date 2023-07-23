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
  [System.NonSerialized] SpriteRenderer droppableSprite;
  [System.NonSerialized] Rigidbody2D body;
  [System.NonSerialized] Sprite spriteHolder;

  private Animator anim;
  private AudioSource audioSource;
  private InGame inGame;

  void Start() {
    flickerEffect = GetComponent<Flicker>();
    droppableSprite = GetComponent<SpriteRenderer>();
    audioSource = GetComponent<AudioSource>();
    inGame = GameObject.Find("UnityHelpers").gameObject.GetComponent<InGame>();

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
    } else {
     droppableSprite.sprite = Sprites.droppableSprites[key];
     spriteHolder = Sprites.droppableSprites[key];
    }

    PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
    collider.autoTiling = true;

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
      if (canBePicked) {
        body.bodyType = RigidbodyType2D.Static;
      }

      if (timer == 0) {
        timer = Time.time * 1000;
        isIdle = true;
      }
    } else {
      isDropping = true;
      if (canBePicked) {
        body.bodyType = RigidbodyType2D.Dynamic;
      }
    }
  }

  void LateUpdate() {
    if (droppableSprite.sprite == null) {
      droppableSprite.sprite = spriteHolder;
    }
  }

  public void PlaySound(AudioClip droppableSound) {
    audioSource.PlayOneShot(droppableSound);
  }

  private void OnCollisionEnter2D(Collision2D col) {
    string gameObjectTag = col.gameObject.tag;

    if (gameObjectTag == "Ground" || gameObjectTag == "Breakable" && Helpers.IsValueInArray(Constants.stackableBreakables, col.gameObject.GetComponent<Breakable>().type)) {
      gameObject.layer = LayerMask.NameToLayer("Dropped");

      if (gameObjectTag == "Breakable") {
        col.gameObject.GetComponent<Breakable>().carriedDroppables.Add(gameObject);
        collisionCounter++;
      }

      if (gameObjectTag == "Ground" && inGame.IsInRoom(inGame.FindRoom(transform.parent))) {
        PlaySound(Sounds.droppableFallingSounds[inGame.GetTileMaterial(transform.position)]);
      }
    } else if (gameObjectTag == "Hero" && canBePicked) {
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
      bool displayMoney = key.Contains("money");
      hero.infoCanvas.GetComponent<InfoCanvas>().Display(displayMoney ? moneyItem.text : Objects.pauseItems[key].name);
    }

    Destroy(transform.parent.gameObject);
  }

  public void FinishAnim() {
    canBePicked = true;
    anim.enabled = false;
    body = gameObject.AddComponent<Rigidbody2D>();
    body.freezeRotation = true;
  }

  public void DecrementCollision() {
    collisionCounter--;
  }
}
