using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleCanvas : MonoBehaviour {
  [SerializeField] GameObject buttonsPanel;
  [SerializeField] GameObject pressPrompt;
  [SerializeField] GameObject buttonsFirstSelected;
  [SerializeField] EventSystem eventSystem;
  [SerializeField] AudioSource audioSource;

  void Start() {
    audioSource = GetComponent<AudioSource>();
  }

  void Update() {
    if (Input.anyKey && !buttonsPanel.activeSelf) {
      if (Settings.playSFX) {
        audioSource.PlayOneShot(Sounds.menuSounds["select"]);
      }

      pressPrompt.SetActive(false);
      buttonsPanel.SetActive(true);
      eventSystem.SetSelectedGameObject(buttonsFirstSelected, new BaseEventData(eventSystem));
    }
  }

  public void GameStart() {
    SceneManager.LoadScene("GameWorld");
  }

  public void Quit() {
    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
    #endif

    Application.Quit();
  }
}
