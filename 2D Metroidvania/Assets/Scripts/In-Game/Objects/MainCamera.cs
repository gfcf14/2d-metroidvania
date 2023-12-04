using UnityEngine;

public class MainCamera : MonoBehaviour {
  void Awake() {
    ActivateCurrentCam();
  }

  void Update() {}

  // finds the camera which is at the room where the player is,
  // then activate it and set the main camera to center at the room where the player is
  public void ActivateCurrentCam() {
    BoxCollider2D heroCollider = GameObject.Find("Hero").GetComponent<BoxCollider2D>();
    Bounds bounds = heroCollider.bounds;

    Collider2D[] colliders = Physics2D.OverlapBoxAll(bounds.center, bounds.size, 0f);
    foreach (Collider2D col in colliders) {
      if (col.gameObject.name == "VCam") {
        col.gameObject.SetActive(true);
        Transform camParentTransform = col.gameObject.transform.parent;
        // TODO: this assumes we'll always start the game in a room with default dimensions (16 x 9). Consider how to do this for rooms with non-default dimensions if needed
        Camera.main.transform.position = new Vector3(camParentTransform.position.x + (Constants.defaultRoomWidth / 2), camParentTransform.position.y - (Constants.defaultRoomHeight / 2), -10);
        break;
      }
    }
  }
}
