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
  void Start() {
    audioSource = GetComponent<AudioSource>();
    overlay = transform.Find("Overlay").gameObject;
    startSound = Sounds.impactSounds["sword"]["critical"];
  }

  void Update() {
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
        if (Settings.playSFX) {
          audioSource.PlayOneShot(Sounds.menuSounds["select"]);
        }

        pressPrompt.SetActive(false);
        buttonsPanel.SetActive(true);
        eventSystem.SetSelectedGameObject(buttonsFirstSelected, new BaseEventData(eventSystem));
      }
    }
  }

  public void GameStart() {
    audioSource.PlayOneShot(startSound);
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
    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
    #endif

    Application.Quit();
  }

  public void PlayMenuSound(string sound) {
    if (Settings.playSFX) {
      audioSource.PlayOneShot(Sounds.menuSounds[sound]);
    }
  }
}
