using System.Collections;
using TMPro;
using UnityEngine;

public class FanfareCanvas : MonoBehaviour {
  [SerializeField] GameObject textObject;
  [System.NonSerialized] float displaySeconds = 2f;
  void Start() {}

  void Update() {}

  public void ShowLevelUp() {
    textObject.GetComponent<TextMeshProUGUI>().text = "LEVEL UP!";
    StartCoroutine(DisplayFanfare());
  }

  public void ShowGetRelic() {
    textObject.GetComponent<TextMeshProUGUI>().text = "GOT RELIC!";
    StartCoroutine(DisplayFanfare());
  }

  IEnumerator DisplayFanfare() {
    yield return new WaitForSecondsRealtime(displaySeconds);
    GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().ClearPauseCase(resumeSoundtrack: true, waitIfLevelingUp: true);
    gameObject.SetActive(false);
  }
}
