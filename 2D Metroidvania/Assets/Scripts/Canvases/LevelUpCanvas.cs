using System.Collections;
using UnityEngine;

public class LevelUpCanvas : MonoBehaviour {
  [System.NonSerialized] float displaySeconds = 1.25f;
  void Start() {}

  void Update() {}

  public void ShowLevelUp() {
    StartCoroutine(DisplayLevelUp());
  }

  IEnumerator DisplayLevelUp() {
    yield return new WaitForSecondsRealtime(displaySeconds);
    GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().ClearPauseCase(resumeSoundtrack: true, waitIfLevelingUp: true);
    gameObject.SetActive(false);
  }
}
