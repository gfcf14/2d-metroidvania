using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class IntroCanvas : MonoBehaviour {
  [SerializeField] public GameObject background;
  [SerializeField] public GameObject fader;

  [System.NonSerialized] Animator faderAnimator;
  [System.NonSerialized] AudioSource audioSource;

  private AudioClip[] audioClips = new AudioClip[2];

  void Start() {
    background.GetComponent<Image>().color = Color.black;

    faderAnimator = fader.GetComponent<Animator>();
    faderAnimator.speed = 0;

    audioSource = GetComponent<AudioSource>();
    audioClips[0] = Sounds.introSounds["suspense"];
    audioClips[1] = Sounds.introSounds["last"];

    PlaySequential();
  }

  void Update() {}

  public void FadeToWhite() {
    faderAnimator.speed = 1;
  }

  public void PlaySequential() {
    StartCoroutine(PlaySequentially());
  }

  IEnumerator PlaySequentially() {
    foreach (AudioClip clip in audioClips) {
      audioSource.clip = clip;
      audioSource.Play();

      // Wait for the clip to finish playing
      yield return new WaitForSeconds(clip.length);
    }
  }
}
