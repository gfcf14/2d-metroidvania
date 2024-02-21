using UnityEngine;

public class EditorElement : MonoBehaviour {
  void Start() {
    GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
  }
  void Update() {}
}
