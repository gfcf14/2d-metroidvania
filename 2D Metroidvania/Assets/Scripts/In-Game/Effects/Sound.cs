using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
  public AudioSource audioSource;
  void Start() {}

  void Update() {}

  public void PlaySound(AudioClip clip) {
    if (Settings.playSFX) {
      audioSource = GetComponent<AudioSource>();
      audioSource.PlayOneShot(clip);
      StartCoroutine(WaitForSoundFinish());
    }
  }

  IEnumerator WaitForSoundFinish() {
    while (audioSource.isPlaying) {
        yield return null;
    }

    Destroy(gameObject);
  }
}
