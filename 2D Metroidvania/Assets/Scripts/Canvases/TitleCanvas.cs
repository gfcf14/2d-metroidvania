using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleCanvas : MonoBehaviour {
  [SerializeField] GameObject buttonsPanel;
  [SerializeField] GameObject pressPrompt;
  [SerializeField] GameObject buttonsFirstSelected;
  [SerializeField] GameObject overlay;
  [SerializeField] EventSystem eventSystem;
  [SerializeField] AudioSource audioSource;

  [System.NonSerialized] AudioClip startSound;

  private bool canPlayDeselect = false;
  GameObject previouslyFocusedButton = null;
  void Start() {
    audioSource = GetComponent<AudioSource>();
    overlay = transform.Find("Overlay").gameObject;
    startSound = Sounds.impactSounds["sword"]["critical"];

    // starts time in case it was stopped (such as moving from pause to title)
    Time.timeScale = 1;
  }

  public void PlaySound(AudioClip clip) {
    if (Settings.playSFX) {
      audioSource.PlayOneShot(clip);
    }
  }

  void CheckEventChange() {
    if (canPlayDeselect) {
      GameObject currentlyFocusedButton = eventSystem.currentSelectedGameObject;

      if (currentlyFocusedButton != previouslyFocusedButton) {
        PlayMenuSound("move");
        previouslyFocusedButton = currentlyFocusedButton;
      }
    }
  }

  void Update() {
    CheckEventChange();

    if (Input.anyKeyDown) {
      string currentKey = "";
      // Iterate through all possible keys
      foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode))) {
        // Check if the current key or mouse button is pressed
        if (Input.GetKey(keyCode)) {
            // Gets the name of the pressed key or mouse button
            currentKey = keyCode.ToString();
            break;  // Exit the loop after finding the first pressed key
        }
      }

      // only proceed with showing the title menu if the key pressed is not a mouse key and if it isn't active
      if (!currentKey.Contains("Mouse") && !buttonsPanel.activeSelf) {
        PlayMenuSound("select");

        pressPrompt.SetActive(false);
        buttonsPanel.SetActive(true);
        eventSystem.SetSelectedGameObject(buttonsFirstSelected, new BaseEventData(eventSystem));
        previouslyFocusedButton = buttonsFirstSelected;
        canPlayDeselect = true;
      }
    }
  }

  public void StopSelectedAnimation() {
    eventSystem.currentSelectedGameObject.GetComponent<Animator>().speed = 0;
  }

  public void PlayPressedAnimation() {
    eventSystem.currentSelectedGameObject.GetComponent<Animator>().Play("Pressed");
    StopSelectedAnimation();
    // nullifies selection to avoid moving after selecting
    eventSystem.SetSelectedGameObject(null, new BaseEventData(eventSystem));
  }

  public void GameStart() {
    canPlayDeselect = false;
    PlayPressedAnimation();
    PlaySound(startSound);
    StartCoroutine(WaitForStartSoundFinish());
  }

  IEnumerator WaitForStartSoundFinish() {
    yield return new WaitForSeconds(startSound.length);

    overlay.SetActive(true);
  }

  public void TransitionToWorld() {
    SceneManager.LoadScene("GameWorld");
  }

  public void Quit() {
    canPlayDeselect = false;
    PlayPressedAnimation();
    PlayMenuSound("select");
    StartCoroutine(QuitGame());
  }

  IEnumerator QuitGame() {
    yield return new WaitForSeconds(1);

    // perform specific quit for windows, mac, and linux
    #if UNITY_STANDALONE
      Application.Quit();
    #endif

    // perform specific quit for editor
    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
    #endif
  }

  public void PlayMenuSound(string sound) {
    PlaySound(Sounds.menuSounds[sound]);
  }
}
