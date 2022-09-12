using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Helpers {
  public static int GetDamage(string weaponWielded) {
	  return Damages.weaponDamages[weaponWielded].damage;
  }

  public static bool IsGroundThrowable(string type) {
    return System.Array.IndexOf(Constants.groundThrowables, type) != -1;
  }

  public static bool IsNonBouncingThrowable(string type) {
    return System.Array.IndexOf(Constants.nonBouncingThrowables, type) != -1;
  }

  public static bool IsSmallRotatingThrowable(string type) {
    return System.Array.IndexOf(Constants.smallRotatingThrowables, type) != -1;
  }

  public static bool IsUsableItem(string type) {
    return System.Array.IndexOf(Constants.usableItemTypes, type) != -1;
  }

  public static Color GetColorFromResistances(string[] elementResistances) {
    int resistancesLength = elementResistances.Length;

    if (resistancesLength == 0) {
      return Color.white;
    }

    float r = 0;
    float g = 0;
    float b = 0;

    foreach (string currentElementResistance in elementResistances) {
      r += Colors.elementResistancesColors[currentElementResistance].r;
      g += Colors.elementResistancesColors[currentElementResistance].g;
      b += Colors.elementResistancesColors[currentElementResistance].b;
    }

    return new Color(r / resistancesLength, g / resistancesLength, b / resistancesLength);
  }

  public static bool IsFireResistant(string[] resistances) {
    return System.Array.IndexOf(resistances, "fire") != -1;
  }

  public static bool IsPoisonResistant(string[] resistances) {
    return System.Array.IndexOf(resistances, "poison") != -1;
  }

  public static void TogglePause(bool pauseState, GameObject pauseCanvas) {
    if (pauseState) {
      Time.timeScale = 0;
      pauseCanvas.SetActive(pauseState);
    } else {
      pauseCanvas.GetComponent<Animator>().Play("pause-fade-out");
    }
  }

  public static bool IsKeyHeld(string key) {
    return Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), key));
  }

  public static bool IsKeyDown(string key) {
    return Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), key));
  }

  public static bool IsKeyUp(string key) {
    return Input.GetKeyUp((KeyCode)Enum.Parse(typeof(KeyCode), key));
  }

  // ensure proper mapping works for several kinds of gamepads
  public static bool IsPauseKeyUp() {
    return Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.JoystickButton9);
  }
  public static bool IsBackKeyDown() {
    return Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.JoystickButton0);
  }

  public static bool IsForbiddenToRemap(string keyCode) {
    return System.Array.IndexOf(Constants.forbiddenKeys, keyCode) != -1 || keyCode.Contains("Mouse") || keyCode.Contains("Button9");
  }

  public static bool IsGamepadKey(string keyCode) {
    return keyCode.Contains("Joystick");
  }

  public static void FocusUIElement(GameObject element) {
    Pause.eventSystem.SetSelectedGameObject(element, new BaseEventData(Pause.eventSystem));
  }
}
