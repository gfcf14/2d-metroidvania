using UnityEngine;

public class MainOverlay : MonoBehaviour {
  private Animator anim;
  private InGame inGame;

  void Start() {
    anim = GetComponent<Animator>();
    inGame = GameObject.Find("UnityHelpers").gameObject.GetComponent<InGame>();
  }

  void Update() {}

  public void PauseUntilFading() {
    anim.speed = 1;
    inGame.SetPauseCase("fading-in");
  }

  public void ProceedWhenFading() {
    inGame.ClearPauseCase();
  }

  public void OverlayStandBy() {
    anim.speed = 0;
    anim.enabled = false;
  }
}
