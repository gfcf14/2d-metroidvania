using System;
using UnityEngine;

public class Interactable : MonoBehaviour {
  [SerializeField] public bool isFacingLeft = true;
  [SerializeField] public string item;
  [SerializeField] public string itemRarity;
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

    string rarity = itemRarity != "" ? itemRarity : (Helpers.IsValueInArray(Constants.moneyItemKeys, item) ? "money" : "normal");
    inGame.InstantiatePrefab("droppable", item, rarity, transform.parent.gameObject, transform, spriteRenderer);
  }
}
