using UnityEngine;

public class Chest : MonoBehaviour {

  [Header("Frame Color (bronze, silver, or gold)")]
  [SerializeField] string frame;
  [Header("Main Color (refer to Colors.cs)")]
  [SerializeField] string color;
  void Start() {
    if (frame != "silver") {
      GetComponent<SpriteRenderer>().color = Colors.chestFrameColors[frame];
    }
    transform.Find("Color").gameObject.GetComponent<SpriteRenderer>().color = Colors.chestColors[color];
  }
  void Update() {}
}
