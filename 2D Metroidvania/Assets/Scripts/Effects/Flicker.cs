using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour {
  public SpriteRenderer droppableRenderer;
  public bool alpha = false;

  void Start() {
    droppableRenderer = GetComponent<SpriteRenderer>();
  }

  void Update() {
    Color newColor = droppableRenderer.color;
    newColor.a = alpha ? 1 : 0;
    droppableRenderer.color = newColor;

    alpha = !alpha;
  }
}
