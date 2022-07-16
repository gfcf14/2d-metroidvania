using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pierce : MonoBehaviour {
  [System.NonSerialized] SpriteRenderer objectRenderer;
  [System.NonSerialized] public Color color;

  void Start() {
    objectRenderer = GetComponent<SpriteRenderer>();
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
