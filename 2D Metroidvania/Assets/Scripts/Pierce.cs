using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pierce : MonoBehaviour {
  [System.NonSerialized] SpriteRenderer objectRenderer;
  [System.NonSerialized] public Color color;
  [System.NonSerialized] public bool isFacingLeft;

  void Start() {
    objectRenderer = GetComponent<SpriteRenderer>();
    transform.localScale = new Vector3(isFacingLeft ? -1 : 1, 1, 1);
  }
  void Update() {
    if (color != null) {
      objectRenderer.color = color;
    }
  }

  public void DestroyPierce() {
    Destroy(gameObject);
  }
}
