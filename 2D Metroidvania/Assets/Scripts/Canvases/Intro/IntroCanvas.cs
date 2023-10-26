using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class IntroCanvas : MonoBehaviour {
  [SerializeField] public GameObject background;
  [SerializeField] public GameObject fader;

  [System.NonSerialized] Animator faderAnimator;
  [System.NonSerialized] AudioSource audioSource;

  private AudioClip[] audioClips = new AudioClip[2];
  private bool hasSkipped = false;

  void Start() {
    background.GetComponent<Image>().color = Color.black;

    faderAnimator = fader.GetComponent<Animator>();
    faderAnimator.speed = 0;

    audioSource = GetComponent<AudioSource>();
    audioClips[0] = Sounds.introSounds["suspense"];
    audioClips[1] = Sounds.introSounds["last"];

    PlaySequential();
  }

  void Update() {
    if (!hasSkipped && Helpers.IsStartKeyDown()) {
      hasSkipped = true;
      StopCurrentSound();
      PlayNextSound();
      Skip();
    }
  }

  public void StopCurrentSound() {
    if (audioSource.isPlaying) {
      audioSource.Stop();
    }
  }

  public void PlayNextSound() {
    if (audioSource.clip != null) {
      int currentIndex = System.Array.IndexOf(audioClips, audioSource.clip);
      if (currentIndex < audioClips.Length - 1) {
        audioSource.clip = audioClips[currentIndex + 1];
        audioSource.Play();
      }
    }
  }

  public void Skip() {
    transform.Find("Rosolis").gameObject.GetComponent<Rosolis>().AccelerateTransition();
    transform.Find("Text").gameObject.GetComponent<IntroText>().ShowCompleteText();
  }

  public void FadeToWhite() {
    faderAnimator.speed = 1;
  }

  public void PlaySequential() {
    StartCoroutine(PlaySequentially());
  }

  IEnumerator PlaySequentially() {
    foreach (AudioClip clip in audioClips) {
      if (!hasSkipped) {
        audioSource.clip = clip;
        audioSource.Play();

        // Wait for the clip to finish playing
        yield return new WaitForSeconds(clip.length);
      }
    }
  }
}
