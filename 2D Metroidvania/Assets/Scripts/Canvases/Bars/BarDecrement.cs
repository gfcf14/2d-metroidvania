using UnityEngine.UI;
using UnityEngine;

public class BarDecrement : MonoBehaviour {
  [SerializeField] public int width;
  [SerializeField] public string type;

  [System.NonSerialized] public RectTransform rect;
  [System.NonSerialized] public float barHeight;
  void Start() {
    rect = GetComponent<RectTransform>();

    if (type == "hp") {
      GetComponent<Image>().color = Colors.hpDecrement;
      barHeight = 27;
    } else if (type == "mp") {
      barHeight = 9;
    }
    rect.sizeDelta = new Vector2(width - Objects.decrementBarMultipliers[type], barHeight);
  }

  void Update() {
    rect.sizeDelta = new Vector2(rect.sizeDelta.x - 1, barHeight);

    if (rect.sizeDelta.x < 0) {
      Destroy(gameObject);
    }
  }
}
