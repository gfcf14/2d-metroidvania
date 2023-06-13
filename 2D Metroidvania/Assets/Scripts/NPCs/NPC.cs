using UnityEngine;

public class NPC : MonoBehaviour {
  [SerializeField] public bool isFacingLeft = false;
  [SerializeField] public string actionAvailable = "";
  void Start() {
    if (isFacingLeft) {
      FlipNPC();
    }
  }

  void Update() {
    
  }

  public void FlipNPC() {
    if (isFacingLeft) {
      transform.localScale = new Vector3(-1, 1, 1);
    } else {
      transform.localScale = Vector3.one;
    }
  }
}
