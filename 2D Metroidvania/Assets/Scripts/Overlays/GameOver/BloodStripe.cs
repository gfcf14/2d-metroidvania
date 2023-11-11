using UnityEngine;

public class BloodStripe : MonoBehaviour {
  [SerializeField] GameObject gameOverText;
  AudioSource audioSource;
  void Start() {
    audioSource = GetComponent<AudioSource>();
    audioSource.clip = Sounds.gameOverSound;

    // TODO: ensure this volume changes with settings
    audioSource.volume = 0.5f;
  }
  void Update() {}

  public void ShowGameOverText() {
    gameOverText.SetActive(true);

    if (Settings.playSFX) {
      audioSource.Play();
    }
  }
}
