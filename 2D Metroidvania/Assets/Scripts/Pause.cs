using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Pause : MonoBehaviour {
  // Canvases
  [Header("Canvases")]
  [SerializeField] GameObject mainCanvas;
  [SerializeField] GameObject optionsCanvas;
  [SerializeField] GameObject controlsCanvas;
  [SerializeField] GameObject preferredInputCanvas;
  [SerializeField] GameObject quitCanvas;
  [Space(10)]

  // Objects to select first upon reaching a canvas
  [Header("First Selections")]
  [SerializeField] GameObject pauseFirstSelected;
  [SerializeField] GameObject optionsButton;
  [SerializeField] GameObject optionsFirstSelected;
  [SerializeField] GameObject controlsButton;
  [SerializeField] GameObject controlsFirstSelected;
  [SerializeField] GameObject preferredInputButton;
  [SerializeField] GameObject preferredInputFirstSelected;
  [SerializeField] GameObject quitButton;
  [SerializeField] GameObject quitFirstSelected;
  [Space(10)]

  // Control buttons
  [Header("Jump Controls")]
  [SerializeField] GameObject jumpButton;
  [SerializeField] GameObject jumpKeyboardImage;
  [SerializeField] GameObject jumpGamepadImage;
  [Space(10)]

  [Header("ATK1 Controls")]
  [SerializeField] GameObject atk1Button;
  [SerializeField] GameObject atk1KeyboardImage;
  [SerializeField] GameObject atk1GamepadImage;
  [Space(10)]

  [Header("ATK2 Controls")]
  [SerializeField] GameObject atk2Button;
  [SerializeField] GameObject atk2KeyboardImage;
  [SerializeField] GameObject atk2GamepadImage;
  [Space(10)]

  // Awaiting Input Objects
  [Header("Awaiting Input Labels")]
  [SerializeField] GameObject jumpAwaitLabel;
  [SerializeField] GameObject atk1AwaitLabel;
  [SerializeField] GameObject atk2AwaitLabel;
  [Space(10)]

  // main footer legends
  [Header("Footer Legends")]
  [SerializeField] GameObject mainButtonPanel;
  [SerializeField] GameObject mainKeysPanel;
  [Space(10)]

  // dynamic hero objects
  [Header("Hero Objects")]
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
  [SerializeField] GameObject atk1Object;
  [SerializeField] GameObject atk2Object;
  [SerializeField] GameObject def1Object;
  [SerializeField] GameObject def2Object;
  [SerializeField] GameObject luck;
  [SerializeField] GameObject critical;
  [SerializeField] GameObject locationObject;
  [Space(10)]

  // magic resistances
  [Header("Magic Resistances")]
  [SerializeField] GameObject resistance1Object;
  [SerializeField] GameObject resistance2Object;
  [SerializeField] GameObject resistance3Object;
  [SerializeField] GameObject resistance4Object;
  [SerializeField] GameObject resistance5Object;
  [SerializeField] GameObject resistance6Object;
  [SerializeField] GameObject resistance7Object;
  [SerializeField] GameObject resistance8Object;
  [SerializeField] GameObject magicEmptyObject;
  [Space(10)]

  // miscellaneous
  [Header("Miscellaneous")]
  [SerializeField] GameObject preferredInputObject;
  [SerializeField] EventSystem eventSystem;
  [Space(10)]

  [System.NonSerialized] bool hasGamepad = false;

  // variables to keep track of stats and preferences
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
  [System.NonSerialized] int atk1 = -1;
  [System.NonSerialized] int atk2 = -1;
  [System.NonSerialized] int def1 = -1;
  [System.NonSerialized] int def2 = -1;
  [System.NonSerialized] float luckPercentage = -0.1f;
  [System.NonSerialized] float criticalPercentage = -0.1f;
  [System.NonSerialized] string location = "";
  [System.NonSerialized] string magicResistances = " ";
  [System.NonSerialized] string preferredInputString = "";
  [System.NonSerialized] string jumpKeyboardString = "";
  [System.NonSerialized] string jumpGamepadString = "";
  [System.NonSerialized] string atk1KeyboardString = "";
  [System.NonSerialized] string atk1GamepadString = "";
  [System.NonSerialized] string atk2KeyboardString = "";
  [System.NonSerialized] string atk2GamepadString = "";

  // current mapping button
  [System.NonSerialized] public static string currentlyMapping = "";

  void Start() {
    heroScript = hero.GetComponent<Hero>();
    eventSystem = EventSystem.current;
  }

  void Update() {
    CheckIfGamepad();
    UpdateControls();
    UpdatePreferredInput();
    UpdatePlayerStats();
    UpdateMagicResistances();
  }

  void FadeOut() {
    optionsCanvas.SetActive(false);
    controlsCanvas.SetActive(false);
    preferredInputCanvas.SetActive(false);
    quitCanvas.SetActive(false);
    mainCanvas.SetActive(true);
    gameObject.SetActive(false);
    eventSystem.SetSelectedGameObject(null, new BaseEventData(eventSystem));
    Time.timeScale = 1;
  }

  void SelectItemsButton() {
    eventSystem.SetSelectedGameObject(pauseFirstSelected, new BaseEventData(eventSystem));
  }

  public void ShowOptionsCanvas() {
    mainCanvas.SetActive(false);
    optionsCanvas.SetActive(true);

    eventSystem.SetSelectedGameObject(optionsFirstSelected, new BaseEventData(eventSystem));
  }

  public void ShowControlsCanvas() {
    optionsCanvas.SetActive(false);
    controlsCanvas.SetActive(true);

    eventSystem.SetSelectedGameObject(controlsFirstSelected, new BaseEventData(eventSystem));
  }

  public void ShowPreferredInputCanvas() {
    optionsCanvas.SetActive(false);
    preferredInputCanvas.SetActive(true);

    eventSystem.SetSelectedGameObject(preferredInputFirstSelected, new BaseEventData(eventSystem));
  }

  public void ShowQuitCanvas() {
    mainCanvas.SetActive(false);
    quitCanvas.SetActive(true);

    eventSystem.SetSelectedGameObject(quitFirstSelected, new BaseEventData(eventSystem));
  }

  public void GoBackToMainFromOptions() {
    optionsCanvas.SetActive(false);
    mainCanvas.SetActive(true);

    eventSystem.SetSelectedGameObject(optionsButton, new BaseEventData(eventSystem));
  }

  public void GoBackToOptionsFromControls() {
    controlsCanvas.SetActive(false);
    optionsCanvas.SetActive(true);

    eventSystem.SetSelectedGameObject(controlsButton, new BaseEventData(eventSystem));
  }

  public void GoBackToOptionsFromPreferredInput() {
    preferredInputCanvas.SetActive(false);
    optionsCanvas.SetActive(true);

    eventSystem.SetSelectedGameObject(preferredInputButton, new BaseEventData(eventSystem));
  }

  public void GoBackToMainFromQuit() {
    quitCanvas.SetActive(false);
    mainCanvas.SetActive(true);

    eventSystem.SetSelectedGameObject(quitButton, new BaseEventData(eventSystem));
  }

  public void QuitGame() {
    Debug.Log("should return to title screen");
  }

  void CheckIfGamepad() {
    List<string> validGamepads = new List<String>();
    foreach(string s in Input.GetJoystickNames()) {
      if (s != "") {
        validGamepads.Add(s);
      }
    }
    hasGamepad = validGamepads.Count > 0;

    if (hasGamepad && Constants.preferredInput == "gamepad" && mainButtonPanel.activeInHierarchy == false ) {
      ShowGamePadOptions();
    } else if ((!hasGamepad || Constants.preferredInput == "keyboard") && mainKeysPanel.activeInHierarchy == false) {
      ShowKeyboardOptions();
    }
  }

  void UpdateControls() {
    if (jumpKeyboardString != Controls.currentKeyboardJump) {
      jumpKeyboardString = Controls.currentKeyboardJump;
      jumpKeyboardImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[jumpKeyboardString];
    }

    if (jumpGamepadString != Controls.currentGamepadJump) {
      jumpGamepadString = Controls.currentGamepadJump;
      jumpGamepadImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[jumpGamepadString];
    }

    if (atk1KeyboardString != Controls.currentKeyboardAttack1) {
      atk1KeyboardString = Controls.currentKeyboardAttack1;
      atk1KeyboardImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[atk1KeyboardString];
    }

    if (atk1GamepadString != Controls.currentGamepadAttack1) {
      atk1GamepadString = Controls.currentGamepadAttack1;
      atk1GamepadImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[atk1GamepadString];
    }

    if (atk2KeyboardString != Controls.currentKeyboardAttack2) {
      atk2KeyboardString = Controls.currentKeyboardAttack2;
      atk2KeyboardImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[atk2KeyboardString];
    }

    if (atk2GamepadString != Controls.currentGamepadAttack2) {
      atk2GamepadString = Controls.currentGamepadAttack2;
      atk2GamepadImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[atk2GamepadString];
    }
  }

  void UpdatePreferredInput() {
    if (preferredInputString != Constants.preferredInput) {
      preferredInputString = Constants.preferredInput;
      preferredInputObject.GetComponent<Text>().text = preferredInputString.ToUpper();
    }
  }

  void UpdatePlayerStats() {
    if (playerEquipment != heroScript.playerEquipment) {
      playerEquipment = heroScript.playerEquipment;
      playerAvatar.GetComponent<Image>().sprite = Sprites.pauseAvatars[playerEquipment];
    }

    if (playerLevel != heroScript.playerLevel) {
      playerLevel = heroScript.playerLevel;
      level.GetComponent<Text>().text = (playerLevel).ToString();
    }

    if (currentHP != heroScript.currentHP) {
      currentHP = heroScript.currentHP;
      currentHPObject.GetComponent<Text>().text = (currentHP).ToString();
    }

    if (maxHP != heroScript.maxHP) {
      maxHP = heroScript.maxHP;
      totalHP.GetComponent<Text>().text = (maxHP).ToString();
    }

    if (currentMP != heroScript.currentMP) {
      currentMP = heroScript.currentMP;
      currentMPObject.GetComponent<Text>().text = (currentMP).ToString();
    }

    if (maxMP != heroScript.maxMP) {
      maxMP = heroScript.maxMP;
      totalMP.GetComponent<Text>().text = (maxMP).ToString();
    }

    if (status != heroScript.status) {
      status = heroScript.status;
      statusObject.GetComponent<Text>().text = char.ToUpper(status[0]) + status.Substring(1);
    }

    if (exp != heroScript.exp) {
      exp = heroScript.exp;
      expObject.GetComponent<Text>().text = (exp).ToString();
    }

    if (next != heroScript.next) {
      next = heroScript.next;
      nextObject.GetComponent<Text>().text = (next).ToString();
    }

    if (gold != heroScript.gold) {
      gold = heroScript.gold;
      goldObject.GetComponent<Text>().text = (gold).ToString();
    }

    if (atk1 != heroScript.atk1) {
      atk1 = heroScript.atk1;
      atk1Object.GetComponent<Text>().text = (atk1).ToString();
    }

    if (atk2 != heroScript.atk2) {
      atk2 = heroScript.atk2;
      atk2Object.GetComponent<Text>().text = (atk2).ToString();
    }

    if (def1 != heroScript.def1) {
      def1 = heroScript.def1;
      def1Object.GetComponent<Text>().text = (def1).ToString();
    }

    if (def2 != heroScript.def2) {
      def2 = heroScript.def2;
      def2Object.GetComponent<Text>().text = (def2).ToString();
    }

    if (criticalPercentage != heroScript.criticalPercentage) {
      criticalPercentage = heroScript.criticalPercentage;
      critical.GetComponent<Text>().text = ((int)(criticalPercentage * 100)).ToString() + " %";
    }

    if (luckPercentage != heroScript.luckPercentage) {
      luckPercentage = heroScript.luckPercentage;
      luck.GetComponent<Text>().text = ((int)(luckPercentage * 100)).ToString() + " %";
    }

    if (location != heroScript.location) {
      location = heroScript.location;
      locationObject.GetComponent<Image>().sprite = Sprites.locationImages[location];
    }
  }

  void UpdateMagicResistances() {
    string currentMagicResistances = String.Join(",", heroScript.magicResistances);

    if (magicResistances != currentMagicResistances) {
      GameObject[] magicResistanceObjects = new GameObject[] {resistance1Object, resistance2Object, resistance3Object, resistance4Object, resistance5Object, resistance6Object, resistance7Object, resistance8Object};
      string[] heroMagicResistances = heroScript.magicResistances;

      if (heroMagicResistances.Length > 1) {
        magicEmptyObject.SetActive(false);
      } else {
        magicEmptyObject.SetActive(true);
      }

      int i = 0;
      foreach (GameObject currMagicResistanceObject in magicResistanceObjects) {
        if (i >= heroMagicResistances.Length) {
          currMagicResistanceObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        } else {
          currMagicResistanceObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
          currMagicResistanceObject.GetComponent<Image>().sprite = Sprites.magicResistances[heroMagicResistances[i]];
        }
        i++;
      }

      magicResistances = currentMagicResistances;
    }
  }

  void ShowGamePadOptions() {
    mainKeysPanel.SetActive(false);
    mainButtonPanel.SetActive(true);
  }

  void ShowKeyboardOptions() {
    mainButtonPanel.SetActive(false);
    mainKeysPanel.SetActive(true);
  }

  public void SetKeyboardAsPreferredInput() {
    Constants.preferredInput = "keyboard";
  }

  public void SetGamepadAsPreferredInput() {
    Constants.preferredInput = "gamepad";
  }

  public void AwaitInput(String key) {
    eventSystem.SetSelectedGameObject(null, new BaseEventData(eventSystem));

    switch(key) {
      case "jump":
        jumpAwaitLabel.SetActive(true);
        break;
      case "atk1":
        atk1AwaitLabel.SetActive(true);
        break;
      case "atk2":
        atk2AwaitLabel.SetActive(true);
        break;
      default:
        break;
    }

    currentlyMapping = key;
  }

  public void FinishMapping(String keyCode) {
    switch(currentlyMapping) {
      case "jump":
        if (Helpers.IsGamepadKey(keyCode)) {
          if (keyCode == Controls.currentGamepadAttack1) {
            Controls.currentGamepadAttack1 = Controls.currentGamepadJump;
          } else if (keyCode == Controls.currentGamepadAttack2) {
            Controls.currentGamepadAttack2 = Controls.currentGamepadJump;
          }

          Controls.currentGamepadJump = keyCode;
        } else {
          if (keyCode == Controls.currentKeyboardAttack1) {
            Controls.currentKeyboardAttack1 = Controls.currentKeyboardJump;
          } else if (keyCode == Controls.currentKeyboardAttack2) {
            Controls.currentKeyboardAttack2 = Controls.currentKeyboardJump;
          }

          Controls.currentKeyboardJump = keyCode;
        }

        jumpAwaitLabel.SetActive(false);
        eventSystem.SetSelectedGameObject(jumpButton, new BaseEventData(eventSystem));
        break;
      case "atk1":
        if (Helpers.IsGamepadKey(keyCode)) {
          if (keyCode == Controls.currentGamepadJump) {
            Controls.currentGamepadJump = Controls.currentGamepadAttack1;
          } else if (keyCode == Controls.currentGamepadAttack2) {
            Controls.currentGamepadAttack2 = Controls.currentGamepadAttack1;
          }

          Controls.currentGamepadAttack1 = keyCode;
        } else {
          if (keyCode == Controls.currentKeyboardJump) {
            Controls.currentKeyboardJump = Controls.currentKeyboardAttack1;
          } else if (keyCode == Controls.currentKeyboardAttack2) {
            Controls.currentKeyboardAttack2 = Controls.currentKeyboardAttack1;
          }

          Controls.currentKeyboardAttack1 = keyCode;
        }

        atk1AwaitLabel.SetActive(false);
        eventSystem.SetSelectedGameObject(atk1Button, new BaseEventData(eventSystem));
        break;
      case "atk2":
        if (Helpers.IsGamepadKey(keyCode)) {
          if (keyCode == Controls.currentGamepadJump) {
            Controls.currentGamepadJump = Controls.currentGamepadAttack2;
          } else if (keyCode == Controls.currentGamepadAttack1) {
            Controls.currentGamepadAttack1 = Controls.currentGamepadAttack2;
          }

          Controls.currentGamepadAttack2 = keyCode;
        } else {
          if (keyCode == Controls.currentKeyboardJump) {
            Controls.currentKeyboardJump = Controls.currentKeyboardAttack2;
          } else if (keyCode == Controls.currentKeyboardAttack1) {
            Controls.currentKeyboardAttack1 = Controls.currentKeyboardAttack2;
          }

          Controls.currentKeyboardAttack2 = keyCode;
        }

        atk2AwaitLabel.SetActive(false);
        eventSystem.SetSelectedGameObject(atk2Button, new BaseEventData(eventSystem));
        break;
      default:
        break;
    }

    currentlyMapping = "";
  }

  public void ResetMapping() {
    Controls.currentKeyboardJump = Controls.DEFAULT_KEYBOARD_JUMP;
    Controls.currentKeyboardAttack1 = Controls.DEFAULT_KEYBOARD_ATTACK_1;
    Controls.currentKeyboardAttack2 = Controls.DEFAULT_KEYBOARD_ATTACK_2;

    Controls.currentGamepadJump = Controls.DEFAULT_GAMEPAD_JUMP;
    Controls.currentGamepadAttack1 = Controls.DEFAULT_GAMEPAD_ATTACK_1;
    Controls.currentGamepadAttack2 = Controls.DEFAULT_GAMEPAD_ATTACK_2;
  }
}