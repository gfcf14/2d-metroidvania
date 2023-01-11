using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutOverlay : MonoBehaviour {
  void Start() {}
  void Update() {}

  public void GameOver() {
    SceneManager.LoadScene("GameOver");
  }
}
