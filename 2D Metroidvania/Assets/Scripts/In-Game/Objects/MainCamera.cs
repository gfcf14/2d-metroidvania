using UnityEngine;

public class MainCamera : MonoBehaviour {
  void Awake() {
    Camera.main.transform.position = new Vector3(GameData.mainCameraX, GameData.mainCameraY, -10);
  }

  void Update() {}
}
