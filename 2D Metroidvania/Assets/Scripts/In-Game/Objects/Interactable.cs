using System;
using UnityEngine;

public class Interactable : MonoBehaviour {
  [SerializeField] public bool isFacingLeft = true;
  [SerializeField] public string item;
  [System.NonSerialized] public bool isOpen = false;
  private Animator anim;
  private Chest chest;
  private SpriteRenderer spriteRenderer;
  private InGame inGame;
  private AudioSource audioSource;

  void Start() {
    anim = GetComponent<Animator>();
    chest = GetComponent<Chest>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    inGame = GameObject.Find("UnityHelpers").gameObject.GetComponent<InGame>();
    audioSource = GetComponent<AudioSource>();

    if (!isFacingLeft) {
      transform.localScale = new Vector2(-1, 1);
    }

    if (item == "") {
      throw new Exception("No item declared for interactable in " + transform.parent.gameObject.name + ". Please declare an item for proper use.");
    }
  }

  void Update() {}

  private void OnCollisionEnter2D(Collision2D col) {
    if (col.collider.name == "Hero" && !isOpen) {
      isOpen = true;
      anim.Play(chest.type + "-open");
    }
  }

  public void PlaySound() {
    audioSource.PlayOneShot(Sounds.chestSounds[chest.type]);
  }

  public void releaseTreasure() {
    Destroy(GetComponent<Rigidbody2D>());
    GetComponent<BoxCollider2D>().enabled = false;

    // TODO: define what items will Interactables drop
    // TODO: define a property that would allow to use different rarities
    inGame.InstantiatePrefab("droppable", item, Helpers.IsValueInArray(Constants.moneyItemKeys, item) ? "money" : "normal", transform.parent.gameObject, transform, spriteRenderer);
  }
}
