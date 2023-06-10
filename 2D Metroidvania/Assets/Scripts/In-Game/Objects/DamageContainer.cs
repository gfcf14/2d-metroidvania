using TMPro;
using UnityEngine;

public class DamageContainer : MonoBehaviour {
  [System.NonSerialized] public int damage;
  [System.NonSerialized] public bool isCritical;
  void Start() {
    GetComponent<Animator>().enabled = true;

    TextMeshPro textElement = transform.Find("DamageText").gameObject.GetComponent<TextMeshPro>();
    textElement.text = damage.ToString() + (isCritical ? "!" : "");
    if (isCritical) {
      transform.Find("DamageText").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1.31f, 0.5645f);
      textElement.font = Objects.fonts["levi-rebrushed"];
      textElement.UpdateFontAsset();
    }

    if (isCritical) {
      textElement.colorGradient = new VertexGradient(Colors.criticalColorTop, Colors.criticalColorTop, Colors.criticalColorBottom, Colors.criticalColorBottom);
      textElement.fontSize = 8;
      textElement.outlineWidth = 0.2f;
      textElement.outlineColor = Colors.criticalColorOutline;
    }
  }

  void Update() {}

  public void DestroyDamageContainer() {
    Destroy(gameObject);
  }
}
