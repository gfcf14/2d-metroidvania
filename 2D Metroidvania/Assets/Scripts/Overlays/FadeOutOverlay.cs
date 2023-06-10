using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutOverlay : MonoBehaviour {
  void Start() {}
  void Update() {}

  public void GameOver() {
    SceneManager.LoadScene("GameOver");
    Time.timeScale = 1; // starts time again so game can keep playing if player starts over
  }
}
