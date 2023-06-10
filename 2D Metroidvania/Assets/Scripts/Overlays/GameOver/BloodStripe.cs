using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodStripe : MonoBehaviour {
  [SerializeField] GameObject gameOverText;
  void Start() {}
  void Update() {}

  public void ShowGameOverText() {
    gameOverText.SetActive(true);
  }
}
