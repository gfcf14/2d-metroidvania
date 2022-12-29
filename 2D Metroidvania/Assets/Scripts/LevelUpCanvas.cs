using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpCanvas : MonoBehaviour {
  [System.NonSerialized] float displaySeconds = 0.5f;
  void Start() {
    StartCoroutine(DisplayLevelUp());
  }

  void Update() {}

  IEnumerator DisplayLevelUp() {
    yield return new WaitForSecondsRealtime(displaySeconds);
    GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().ClearPauseCase();
    gameObject.SetActive(false);
  }
}
