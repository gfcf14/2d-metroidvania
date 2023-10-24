using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rosolis : MonoBehaviour {
  IntroCanvas introCanvas;
  Animator anim;
  private float waitTime = 2.2f;

  void Start() {
    introCanvas = transform.parent.gameObject.GetComponent<IntroCanvas>();
    anim = GetComponent<Animator>();
    anim.speed = 0;
    StartCoroutine(StartBloom());
  }

  void Update() {}

  public void TransitionToTitle() {
    StartCoroutine(WaitBeforeTransition());
  }

  IEnumerator WaitBeforeTransition() {
    yield return new WaitForSeconds(5);
    SceneManager.LoadScene("Title");
  }

  IEnumerator StartBloom() {
    yield return new WaitForSeconds(waitTime);
    anim.speed = 1;
  }

  public void StartFade() {
    introCanvas.FadeToWhite();
  }
}
