using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleCanvas : MonoBehaviour {
  [SerializeField] GameObject buttonsPanel;
  [SerializeField] GameObject pressPrompt;
  [SerializeField] GameObject buttonsFirstSelected;
  [SerializeField] EventSystem eventSystem;
  Hero hero;

  void Start() {
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
    hero.SetPauseCase("title");
  }

  void Update() {
    if (Input.anyKey && !buttonsPanel.active) {
      pressPrompt.SetActive(false);
      buttonsPanel.SetActive(true);
      eventSystem.SetSelectedGameObject(buttonsFirstSelected, new BaseEventData(eventSystem));
    }
  }
}
