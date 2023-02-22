using System.Collections;
using UnityEngine;

public class LevelUpCanvas : MonoBehaviour {
  [System.NonSerialized] float displaySeconds = 0.5f;
  void Start() {}

  void Update() {}

  public void ShowLevelUp() {
    StartCoroutine(DisplayLevelUp());
  }

  IEnumerator DisplayLevelUp() {
    yield return new WaitForSecondsRealtime(displaySeconds);
    GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().ClearPauseCase();
    gameObject.SetActive(false);
  }
}
