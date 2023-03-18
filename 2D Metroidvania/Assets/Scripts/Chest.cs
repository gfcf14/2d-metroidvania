using UnityEngine;

public class Chest : MonoBehaviour {
  [SerializeField] string frame;
  [SerializeField] string color;
  void Start() {
    if (frame != "silver") {
      GetComponent<SpriteRenderer>().color = Colors.chestFrameColors[frame];
    }
    transform.Find("Color").gameObject.GetComponent<SpriteRenderer>().color = Colors.chestColors[color];
  }
  void Update() {}
}
