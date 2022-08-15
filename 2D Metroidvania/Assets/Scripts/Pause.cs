using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Pause : MonoBehaviour {
  [SerializeField] GameObject pauseFirstSelected;

  void Start() {}

  void Update() {}

  void FadeOut() {
    gameObject.SetActive(false);
    Time.timeScale = 1;
  }

  void SelectItemsButton() {
    EventSystem eventSystem = EventSystem.current;
    eventSystem.SetSelectedGameObject(pauseFirstSelected, new BaseEventData(eventSystem));
  }
}
