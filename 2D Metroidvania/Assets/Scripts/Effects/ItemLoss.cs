using TMPro;
using UnityEngine;

public class ItemLoss : MonoBehaviour {
  public bool isItem = false;
  public string multiplierText = "";
  public Sprite itemLossImage;
  void Start() {
    Transform wrapper = transform.Find("Wrapper");

    if (isItem) {
      wrapper.Find("Image").GetComponent<SpriteRenderer>().sprite = itemLossImage;
    }
    wrapper.Find("Multiplier").GetComponent<TextMeshPro>().text = multiplierText;
  }

  void Update() {}

  public void DestroyItemLoss() {
    Destroy(gameObject);
  }
}
