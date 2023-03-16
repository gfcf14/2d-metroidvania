using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {
  [SerializeField] string frame;
  [SerializeField] string color;
  void Start() {
    transform.Find("Frame").gameObject.GetComponent<SpriteRenderer>().sprite = Sprites.frameSprites[frame];
    transform.Find("Color").gameObject.GetComponent<SpriteRenderer>().color = Colors.chestColors[color];
  }
  void Update() {}
}
