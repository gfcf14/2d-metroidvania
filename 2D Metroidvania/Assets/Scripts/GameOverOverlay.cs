using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverOverlay : MonoBehaviour {
  [SerializeField] GameObject titleCanvas;
  void Start() {}
  void Update() {}

  public void TransitionToTitle() {
    titleCanvas.transform.Find("Prompt").gameObject.SetActive(true);
    titleCanvas.transform.Find("ButtonPanel").gameObject.SetActive(false);
    titleCanvas.SetActive(true);
  }
}
