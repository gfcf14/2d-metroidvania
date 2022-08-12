using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFadeOut : MonoBehaviour {
    void Start() {}

    void Update() {}

    void FadeOut() {
      gameObject.SetActive(false);
      Time.timeScale = 1;
    }
}
