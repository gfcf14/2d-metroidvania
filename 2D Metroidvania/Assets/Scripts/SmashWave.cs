using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashWave : MonoBehaviour {
  [System.NonSerialized] public float width;
  [System.NonSerialized] public int damage;
  void Start() {
    GetComponent<SpriteRenderer>().size = new Vector2(width, width * 0.8f);
  }

  void Update() {}

  public void DestroySmashWave() {
    Destroy(gameObject);
  }

  private void OnTriggerEnter2D(Collider2D col) {
    if (col.gameObject.tag == "Hero") {
      col.gameObject.GetComponent<Hero>().ReceiveSmashWave(damage, col.ClosestPoint(transform.position));
    }
  }
}
