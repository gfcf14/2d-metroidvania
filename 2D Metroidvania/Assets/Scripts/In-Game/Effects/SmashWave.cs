using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashWave : MonoBehaviour {
  [System.NonSerialized] public float width;
  [System.NonSerialized] public int damage;
  [System.NonSerialized] public bool hitPlayer = false;

  private InGame inGame;
  void Start() {
    GetComponent<SpriteRenderer>().size = new Vector2(width, width * 0.8f);
    inGame = GameObject.Find("InGame").gameObject.GetComponent<InGame>();
  }

  void Update() {}

  public void DestroySmashWave() {
    Destroy(gameObject);
  }

  private void OnTriggerEnter2D(Collider2D col) {
    if (col.gameObject.tag == "Hero" && !hitPlayer) {
      hitPlayer = true;
      Hero heroInstance = col.gameObject.GetComponent<Hero>();
      // ensures the hero isn't damaged after being damaged
      if (!heroInstance.isInvulnerable) {
        heroInstance.ReceiveSmashWave(damage, col.ClosestPoint(transform.position));
      }
    }
  }

  public void PlaySmash() {
    inGame.PlaySound(Sounds.meleeSounds["smash"], transform.position);
  }
}
