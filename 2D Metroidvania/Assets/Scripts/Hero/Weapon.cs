using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
  Hero hero;
  private AudioSource audioSource;
  [System.NonSerialized] public List<GameObject> triggeredObjects = new List<GameObject>();

  void Start() {
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
    audioSource = transform.parent.gameObject.GetComponent<AudioSource>();
  }

  void Update() {}

  private void OnTriggerEnter2D(Collider2D col) {
    if (col.gameObject.tag == "Enemy") {
      GameObject parentObject = col.gameObject.transform.parent.gameObject;
      float enemyTopBounds = col.transform.position.y + parentObject.GetComponent<SpriteRenderer>().bounds.size.y;

      if (hero.isDropKicking) {
        float heroBottomBounds = GameObject.FindGameObjectWithTag("Hero").transform.position.y;

        if (Mathf.Abs(heroBottomBounds - enemyTopBounds) <= 1) {
          parentObject.GetComponent<Enemy>().Trigger(GetComponent<CapsuleCollider2D>());
          hero.Jump(true);
        }
      }
    }
  }

  private void OnTriggerExit2D(Collider2D col) {
    if (col.gameObject.tag == "Enemy") {
      triggeredObjects.Remove(col.gameObject);
    }
  }

  private string GetThrowableSound(string type, string key) {
    return type + (
      type.Contains("double") ? "-large" : (
        Helpers.IsValueInArray(Constants.smallThrowables, key) ? "-small" : "-middle"
      )
    );
  }

  public void PlaySound(string type, string key = "") {
    audioSource.PlayOneShot(Sounds.attackSounds[type.Contains("throwable") ? GetThrowableSound(type, key) : type]);
  }
}
