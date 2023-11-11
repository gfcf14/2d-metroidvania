using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {
  AudioSource audioSource;
  void Start() {
    audioSource = GetComponent<AudioSource>();
  }

  void Update() {}

  public void PlaySound(string projectileEquipment) {
    if (Settings.playSFX) {
      audioSource.PlayOneShot(Sounds.attackSounds["bow"]);
    }
  }
}
