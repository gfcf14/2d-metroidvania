using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Pause : MonoBehaviour {
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
  [SerializeField] GameObject critical;
  [SerializeField] GameObject pauseFirstSelected;

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
  [System.NonSerialized] float criticalPercentage = -0.1f;

  void Start() {
    heroScript = hero.GetComponent<Hero>();
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
  }

  void FadeOut() {
    gameObject.SetActive(false);
    Time.timeScale = 1;
  }

  void SelectItemsButton() {
    EventSystem eventSystem = EventSystem.current;
    eventSystem.SetSelectedGameObject(pauseFirstSelected, new BaseEventData(eventSystem));
  }
}
