using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Pause : MonoBehaviour {
  [SerializeField] EventSystem eventSystem;
  [SerializeField] GameObject mainCanvas;
  [SerializeField] GameObject quitCanvas;

  [SerializeField] GameObject pauseFirstSelected;
  [SerializeField] GameObject quitButton;
  [SerializeField] GameObject quitFirstSelected;

  [SerializeField] GameObject hero;
  [SerializeField] GameObject playerAvatar;
  [SerializeField] GameObject level;
  [SerializeField] GameObject currentHPObject;
  [SerializeField] GameObject totalHP;
  [SerializeField] GameObject currentMPObject;
  [SerializeField] GameObject totalMP;
  [SerializeField] GameObject statusObject;
  [SerializeField] GameObject expObject;
  [SerializeField] GameObject nextObject;
  [SerializeField] GameObject goldObject;
  [SerializeField] GameObject atkLeftObject;
  [SerializeField] GameObject atkRightObject;
  [SerializeField] GameObject defLeftObject;
  [SerializeField] GameObject defRightObject;
  [SerializeField] GameObject luck;
  [SerializeField] GameObject critical;
  [SerializeField] GameObject locationObject;
  [SerializeField] GameObject resistance1Object;
  [SerializeField] GameObject resistance2Object;
  [SerializeField] GameObject resistance3Object;
  [SerializeField] GameObject resistance4Object;
  [SerializeField] GameObject resistance5Object;
  [SerializeField] GameObject resistance6Object;
  [SerializeField] GameObject resistance7Object;
  [SerializeField] GameObject resistance8Object;

  [System.NonSerialized] Hero heroScript;
  [System.NonSerialized] string playerEquipment = "";
  [System.NonSerialized] int playerLevel = -1;
  [System.NonSerialized] int currentHP = -1;
  [System.NonSerialized] int maxHP = -1;
  [System.NonSerialized] int currentMP = -1;
  [System.NonSerialized] int maxMP = -1;
  [System.NonSerialized] string status = "";
  [System.NonSerialized] int exp = -1;
  [System.NonSerialized] int next = -1;
  [System.NonSerialized] int gold = -1;
  [System.NonSerialized] int atkLeft = -1;
  [System.NonSerialized] int atkRight = -1;
  [System.NonSerialized] int defLeft = -1;
  [System.NonSerialized] int defRight = -1;
  [System.NonSerialized] float luckPercentage = -0.1f;
  [System.NonSerialized] float criticalPercentage = -0.1f;
  [System.NonSerialized] string location = "";
  [System.NonSerialized] string magicResistances = " ";

  void Start() {
    heroScript = hero.GetComponent<Hero>();
    eventSystem = EventSystem.current;
  }

  void Update() {
    if (playerEquipment != heroScript.playerEquipment) {
      playerAvatar.GetComponent<Image>().sprite = Utilities.pauseAvatars[heroScript.playerEquipment];
    }

    if (playerLevel != heroScript.playerLevel) {
      level.GetComponent<Text>().text = (heroScript.playerLevel).ToString();
    }

    if (currentHP != heroScript.currentHP) {
      currentHPObject.GetComponent<Text>().text = (heroScript.currentHP).ToString();
    }

    if (maxHP != heroScript.maxHP) {
      totalHP.GetComponent<Text>().text = (heroScript.maxHP).ToString();
    }

    if (currentMP != heroScript.currentMP) {
      currentMPObject.GetComponent<Text>().text = (heroScript.currentMP).ToString();
    }

    if (maxMP != heroScript.maxMP) {
      totalMP.GetComponent<Text>().text = (heroScript.maxMP).ToString();
    }

    if (status != heroScript.status) {
      string statusString = (heroScript.status).ToString();
      statusObject.GetComponent<Text>().text = char.ToUpper(statusString[0]) + statusString.Substring(1);
    }

    if (exp != heroScript.exp) {
      expObject.GetComponent<Text>().text = (heroScript.exp).ToString();
    }

    if (next != heroScript.next) {
      nextObject.GetComponent<Text>().text = (heroScript.next).ToString();
    }

    if (gold != heroScript.gold) {
      goldObject.GetComponent<Text>().text = (heroScript.gold).ToString();
    }

    if (atkLeft != heroScript.atkLeft) {
      atkLeftObject.GetComponent<Text>().text = (heroScript.atkLeft).ToString();
    }

    if (atkRight != heroScript.atkRight) {
      atkRightObject.GetComponent<Text>().text = (heroScript.atkRight).ToString();
    }

    if (defLeft != heroScript.defLeft) {
      defLeftObject.GetComponent<Text>().text = (heroScript.defLeft).ToString();
    }

    if (defRight != heroScript.defRight) {
      defRightObject.GetComponent<Text>().text = (heroScript.defRight).ToString();
    }

    if (criticalPercentage != heroScript.criticalPercentage) {
      critical.GetComponent<Text>().text = ((int)(heroScript.criticalPercentage * 100)).ToString() + " %";
    }

    if (luckPercentage != heroScript.luckPercentage) {
      luck.GetComponent<Text>().text = ((int)(heroScript.luckPercentage * 100)).ToString() + " %";
    }

    if (location != heroScript.location) {
      locationObject.GetComponent<Image>().sprite = Utilities.locationImages[heroScript.location];
    }

    string currentMagicResistances = String.Join(",", heroScript.magicResistances);

    if (magicResistances != currentMagicResistances) {
      GameObject[] magicResistanceObjects = new GameObject[] {resistance1Object, resistance2Object, resistance3Object, resistance4Object, resistance5Object, resistance6Object, resistance7Object, resistance8Object};
      string[] heroMagicResistances = heroScript.magicResistances;

      int i = 0;
      foreach (GameObject currMagicResistanceObject in magicResistanceObjects) {
        if (i >= heroMagicResistances.Length) {
          currMagicResistanceObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        } else {
          currMagicResistanceObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
          currMagicResistanceObject.GetComponent<Image>().sprite = Utilities.magicResistances[heroMagicResistances[i]];
        }
        i++;
      }

      magicResistances = currentMagicResistances;
    }
  }

  void FadeOut() {
    quitCanvas.SetActive(false);
    mainCanvas.SetActive(true);
    gameObject.SetActive(false);
    Time.timeScale = 1;
  }

  void SelectItemsButton() {
    eventSystem.SetSelectedGameObject(pauseFirstSelected, new BaseEventData(eventSystem));
  }

  public void ShowQuitCanvas() {
    mainCanvas.SetActive(false);
    quitCanvas.SetActive(true);

    eventSystem.SetSelectedGameObject(quitFirstSelected, new BaseEventData(eventSystem));
  }

  public void GoBackToMainFromQuit() {
    quitCanvas.SetActive(false);
    mainCanvas.SetActive(true);

    eventSystem.SetSelectedGameObject(quitButton, new BaseEventData(eventSystem));
  }

  public void QuitGame() {
    Debug.Log("should return to title screen");
  }
}
