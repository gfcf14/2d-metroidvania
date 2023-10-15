using UnityEngine;

public class Droppable : MonoBehaviour {
  [SerializeField] public string key;
  [SerializeField] public string rarity;
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
  [System.NonSerialized] PolygonCollider2D droppableCollider;

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

    droppableCollider = gameObject.AddComponent<PolygonCollider2D>();
    droppableCollider.autoTiling = true;

    if (isDropped) {
      anim.Play("droppable-rise");
    } else {
      FinishAnim();
      // GetComponent<SpriteRenderer>().sprite = null;
    }
  }

  void Update() {

    // Algorithm to detect if the droppable is headed beyond a roof due to animation transition
    if (!canBePicked) {
      Vector2 roofCast = transform.position + new Vector3(0, droppableSprite.sprite.bounds.size.y / 2, 0);
      Vector2 roofCastDirection = transform.TransformDirection(new Vector2(0, 1));

      RaycastHit2D roofRayCast = Physics2D.Raycast(roofCast, roofCastDirection, 0.25f);
      Debug.DrawRay(roofCast, roofCastDirection.normalized * 0.25f, Colors.raycastColors["edge"]);

      if (roofRayCast && roofRayCast.collider.tag == "Ground") {
        anim.speed = 0;
        FinishAnim();
      }
    }

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

    if (gameObjectTag == "Ground" || gameObjectTag == "Interactable") {
      if (inGame.IsInRoom(inGame.FindRoom(transform.parent))) {
        string materialFallingOn = inGame.GetTileMaterial(transform.position);
        if (materialFallingOn == null) {
          // TODO: find a better way to get the location
          materialFallingOn = Helpers.GetMaterial(GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().location);
        }
        PlaySound(Sounds.droppableFallingSounds[gameObjectTag == "Interactable" ? "interactable" : materialFallingOn]);
      }

      // destroys the rigid body and makes the collider a trigger so that
      // if the player is overlapping no movement is caused (usually pushing the player up)
      Destroy(body);
      droppableCollider.isTrigger = true;

      gameObject.layer = LayerMask.NameToLayer("Dropped");

      if (GetComponent<Flicker>() != null) {
        timer = Time.time * 1000;
        isIdle = true;
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    string gameObjectTag = col.gameObject.tag;
    if (gameObjectTag == "Hero" && canBePicked) {
      string itemPickSoundIndex = rarity == "" ? (Helpers.IsValueInArray(Constants.moneyItemKeys, key) ? "money" : "normal") : rarity;

      inGame.PlaySound(Sounds.itemPickSounds[itemPickSoundIndex], transform.position);
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
}
