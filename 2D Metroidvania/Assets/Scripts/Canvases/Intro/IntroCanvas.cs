using UnityEngine.UI;
using UnityEngine;

public class IntroCanvas : MonoBehaviour {
  [SerializeField] public GameObject background;
  [SerializeField] public GameObject fader;
  [System.NonSerialized] Animator faderAnimator;

  void Start() {
    background.GetComponent<Image>().color = Color.black;
    faderAnimator = fader.GetComponent<Animator>();
    faderAnimator.speed = 0;
  }

  void Update() {}

  public void FadeToWhite() {
    faderAnimator.speed = 1;
  }
}
