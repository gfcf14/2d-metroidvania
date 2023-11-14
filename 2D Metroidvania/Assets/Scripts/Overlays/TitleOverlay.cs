using UnityEngine;

public class TitleOverlay : MonoBehaviour {
  void Start() {}

  void Update() {}

  public void ProceedAfterFadeOut() {
    transform.parent.gameObject.GetComponent<TitleCanvas>().TransitionToWorld();
  }
}
