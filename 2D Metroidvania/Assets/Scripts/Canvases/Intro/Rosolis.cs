using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rosolis : MonoBehaviour {
  void Start() {}

  void Update() {}

  public void TransitionToTitle() {
    StartCoroutine(WaitBeforeTransition());
  }

  IEnumerator WaitBeforeTransition() {
    yield return new WaitForSeconds(5);
    SceneManager.LoadScene("Title");
  }

  public void StartFade() {
    transform.parent.gameObject.GetComponent<IntroCanvas>().FadeToWhite();
  }
}
