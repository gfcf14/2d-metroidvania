using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverText : MonoBehaviour {
  [SerializeField] GameObject gameOverOverlay;
  void Start() {}
  void Update() {}

  public void FadeOutGameOverCanvas() {
    gameOverOverlay.SetActive(true);
  }
}
