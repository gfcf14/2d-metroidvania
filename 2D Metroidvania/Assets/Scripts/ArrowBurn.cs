using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBurn : MonoBehaviour {
  private Animator anim;

  [System.NonSerialized] public float startTime;
  [System.NonSerialized] float maxBurnDuration = 3000f;
  [System.NonSerialized] public bool isFinished;

  void Start() {}

  void Update() {
    anim = GetComponent<Animator>();

    float currentTime = Time.time * 1000;

    if (currentTime > startTime + maxBurnDuration) {
      isFinished = true;
    }

    anim.SetBool("isFinished", isFinished);
  }

  public void DestroySmoke() {
    Destroy(gameObject);
  }
}
