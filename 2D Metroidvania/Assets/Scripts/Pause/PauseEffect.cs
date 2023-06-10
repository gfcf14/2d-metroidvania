using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseEffect : MonoBehaviour {
  void Start() {}
  void Update() {}

  public void Disable() {
    gameObject.SetActive(false);
  }
}
