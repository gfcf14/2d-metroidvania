using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleCanvas : MonoBehaviour {
  [SerializeField] GameObject buttonsPanel;
  [SerializeField] GameObject pressPrompt;
  [SerializeField] GameObject buttonsFirstSelected;
  [SerializeField] EventSystem eventSystem;
  Hero hero;

  void Start() {
    // TODO: ensure this is truly not needed when starting the game
    // hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
    // hero.SetPauseCase("title");
  }

  void Update() {
    if (Input.anyKey && !buttonsPanel.activeSelf) {
      pressPrompt.SetActive(false);
      buttonsPanel.SetActive(true);
      eventSystem.SetSelectedGameObject(buttonsFirstSelected, new BaseEventData(eventSystem));
    }
  }

  public void GameStart() {
    // hero.ClearPauseCase();
    SceneManager.LoadScene("GameWorld");
  }

  public void Quit() {
    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
    #endif

    Application.Quit();
  }
}
