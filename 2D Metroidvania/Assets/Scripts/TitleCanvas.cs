using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleCanvas : MonoBehaviour {
  [SerializeField] GameObject buttonsPanel;
  [SerializeField] GameObject pressPrompt;
  [SerializeField] GameObject buttonsFirstSelected;
  [SerializeField] EventSystem eventSystem;

  void Start() {}

  void Update() {
    if (Input.anyKey && !buttonsPanel.activeSelf) {
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
