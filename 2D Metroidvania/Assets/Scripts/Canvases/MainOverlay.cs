using UnityEngine;

public class MainOverlay : MonoBehaviour {
  private Animator anim;
  private InGame inGame;

  public float timeOnFade = 0;

  void Start() {
    anim = GetComponent<Animator>();
    inGame = GameObject.Find("InGame").gameObject.GetComponent<InGame>();
  }

  void Update() {}

  public void PauseUntilFading() {
    anim.speed = 1;
    inGame.SetPauseCase("fading-in");
  }

  public void ProceedWhenFading() {
    inGame.ClearPauseCase();
    timeOnFade = Time.realtimeSinceStartup;
  }

  public void OverlayStandBy() {
    anim.speed = 0;
    anim.enabled = false;
  }

  public void PlaySoundtrack() {
    // TODO: find a way to get location
    inGame.PlaySoundtrack("meadows");
  }
}
