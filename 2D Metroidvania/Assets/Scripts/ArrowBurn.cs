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

    if (Helpers.ExceedsTime(startTime, maxBurnDuration)) {
      isFinished = true;
    }

    anim.SetBool("isFinished", isFinished);
  }

  public void DestroySmoke() {
    Destroy(gameObject);
  }
}
