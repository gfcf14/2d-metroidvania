using UnityEngine.UI;
using UnityEngine;

public class BarDecrement : MonoBehaviour {
  [SerializeField] public int width;
  [SerializeField] public string type;

  [System.NonSerialized] public RectTransform rect;
  void Start() {
    rect = GetComponent<RectTransform>();

    if (type == "hp") {
      GetComponent<Image>().color = Colors.hpDecrement;
    }
    rect.sizeDelta = new Vector2(width * Constants.hpMultiplier, 27);
  }

  void Update() {
    rect.sizeDelta = new Vector2(rect.sizeDelta.x - 1, 27);

    if (rect.sizeDelta.x < 0) {
      Destroy(gameObject);
    }
  }
}
