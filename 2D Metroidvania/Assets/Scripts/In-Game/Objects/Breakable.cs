using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
  [SerializeField] public string type;
  [SerializeField] public string item;
  [SerializeField] public string itemRarity;
  [SerializeField] public string material;

  [SerializeField] public bool isGrounded;
  [SerializeField] public bool isFalling = false;
  [System.NonSerialized] public bool isBreaking = false;

  private float soundLength = 0;
  private Animator anim;
  private SpriteRenderer spriteRenderer;
  private Rigidbody2D body;
  private BoxCollider2D breakableCollider;
  private AudioSource audioSource;
  private InGame inGame;
  void Start() {
    body = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    breakableCollider = GetComponent<BoxCollider2D>();
    audioSource = GetComponent<AudioSource>();
    inGame = GameObject.Find("UnityHelpers").gameObject.GetComponent<InGame>();
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

  void Update() {}

  bool CheckIfGrounded() {
    Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + breakableCollider.offset.y), breakableCollider.size, 0f);

    // if (gameObject.name == "Breakable (3)") {
    //   DrawRectangle(new Vector2(transform.position.x, transform.position.y + breakableCollider.offset.y), breakableCollider.size);
    // }

    foreach(Collider2D currentCollider in colliders) {
      if (currentCollider != breakableCollider) {
        if (currentCollider.gameObject.name == "Floor" || transform.position.y > currentCollider.gameObject.transform.position.y) {
          return true;
        }
      }
    }

    return false;
  }

  private void OnCollisionEnter2D(Collision2D col) {
    string colTag = col.gameObject.tag;

    if (colTag == "Breakable" && Helpers.IsValueInArray(Constants.stackableBreakables, col.gameObject.GetComponent<Breakable>().type) && !Helpers.IsValueInArray(Constants.stackableBreakables, type)) {
      throw new Exception("Breakable objects that are not Barrels or Boxes should not stack with anything");
    }

    if (col.gameObject.name == "ProximityCheck") {
      Physics2D.IgnoreCollision(col.gameObject.GetComponent<PolygonCollider2D>(), breakableCollider);
    }

    if (inGame.IsInRoom(inGame.FindRoom(transform.parent))) {
      isFalling = true;
      StartCoroutine(PrepareFalling(col.gameObject));
    }
  }

  IEnumerator PrepareFalling(GameObject gameObjectUnder) {
    yield return new WaitForSeconds(0.01f);
    PlayFalling(gameObjectUnder);
  }

  private void OnTriggerEnter2D(Collider2D col) {
    string colTag = col.gameObject.tag;

    // since these objects are to go on top of other same objects, change the transform.position z value to render on top
    if (colTag == "Breakable" && !Helpers.IsValueInArray(Constants.stackableBreakables, type)) {
      Destroy(body);
      GetComponent<BoxCollider2D>().isTrigger = true;
    }

    if (colTag == "Weapon" && !isBreaking) {
      isBreaking = true;
      Destroy(body);
      GetComponent<BoxCollider2D>().isTrigger = true;

      string rarity = itemRarity != "" ? itemRarity : (Helpers.IsValueInArray(Constants.moneyItemKeys, item) ? "money" : "normal");

      inGame.InstantiatePrefab("droppable", item, rarity, GetItemSpawnedParent(), transform.position, spriteRenderer);

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
          // ensures that the throwables start their bounce back at the point of contact
          col.transform.parent.parent.position = parentTransform.position;
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

  public void PlayBreaking() {
    AudioClip[] breakableClips = Sounds.breakableSounds[type];
    PlaySound(Helpers.GetRandomClipFromGroup(breakableClips));
  }

  public void PlayFalling(GameObject objectUnder) {
    switch (objectUnder.tag) {
      case "Breakable":
        AudioClip[] breakableClips = Sounds.fallingSounds[type][objectUnder.GetComponent<Breakable>().type];
        PlaySound(Helpers.GetRandomClipFromGroup(breakableClips));
      break;
      case "Floor":
        string materialFallingOn = inGame.GetTileMaterial(transform.position);

        // if there is no tile material, falling sound will be assumed from location
        if (materialFallingOn == null) {
          // TODO: find a better way to get the location
          materialFallingOn = Helpers.GetMaterial(GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().location);
        }
        AudioClip[] groundClips = Sounds.fallingSounds[type][materialFallingOn];
        PlaySound(Helpers.GetRandomClipFromGroup(groundClips));
      break;
      case "Item":
        AudioClip[] itemClips = Sounds.fallingSounds[type][objectUnder.tag.ToLower()];
        PlaySound(Helpers.GetRandomClipFromGroup(itemClips));
      break;
      default:
        Debug.Log("Consider adding sound for when falling on " + objectUnder.name + "(tagged " + objectUnder.tag + ")");
      break;
    }
  }

  public void PlaySound(AudioClip breakableSound) {
    if (Helpers.IsPastPlayElapsedTime(inGame) && Settings.playSFX) {
      // lower volume to aggregate to 1 depending on the breakable siblings
      float audioVolume = 1 / BreakableCount();
      audioSource.volume = audioVolume;

      soundLength = breakableSound.length;

      audioSource.PlayOneShot(breakableSound);
      StartCoroutine(ActionAfterSound());
    }
  }

  IEnumerator ActionAfterSound() {
    yield return new WaitForSeconds(soundLength);
    audioSource.volume = 1;
    isFalling = false;
  }

  private float BreakableCount() {
    if (transform.parent.gameObject.name != "AudioGroup") {
      return 1;
    }

    return transform.parent.childCount;
  }

  private GameObject GetItemSpawnedParent() {
    Transform immediateParent = transform.parent;
    if (immediateParent.gameObject.name == "AudioGroup") {
      return immediateParent.parent.gameObject;
    }

    return immediateParent.gameObject;
  }
}