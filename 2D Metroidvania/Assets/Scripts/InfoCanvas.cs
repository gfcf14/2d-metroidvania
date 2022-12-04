using UnityEngine;

public class InfoCanvas : MonoBehaviour {
  [System.NonSerialized] float maxDisplayTime = 2000;
  [System.NonSerialized] public float startTime = 0;
  void Start() {}

  void Update() {
    float currentTime = Time.time * 1000;

    if (currentTime > startTime + maxDisplayTime) {
      gameObject.SetActive(false);
    }
  }
}