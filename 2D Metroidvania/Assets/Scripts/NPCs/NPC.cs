using UnityEngine;

public class NPC : MonoBehaviour {
  [SerializeField] public bool isFacingLeft = false;
  [SerializeField] public string actionAvailable = "";
  void Start() {
    if (isFacingLeft) {
      FlipNPC();
    }
  }

  void Update() {}

  public void FlipNPC() {
    if (isFacingLeft) {
      transform.localScale = new Vector3(-1, 1, 1);
    } else {
      transform.localScale = Vector3.one;
    }
  }

  public void DecideFlip(Vector2 interactorPosition) {
    Vector2 currentPosition = transform.position;

    if ((isFacingLeft && interactorPosition.x > currentPosition.x) || (!isFacingLeft && interactorPosition.x < currentPosition.x)) {
      isFacingLeft = !isFacingLeft;
      FlipNPC();
    }
  }
}
