using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Helpers {
  public static int GetDamage(string weaponWielded) {
	  return Damages.weaponDamages[weaponWielded].damage;
  }

  public static bool IsNonBouncingThrowable(string type) {
    return IsValueInArray(Constants.nonBouncingThrowables, type);
  }

  public static bool IsSmallRotatingThrowable(string type) {
    return IsValueInArray(Constants.smallRotatingThrowables, type);
  }

  public static bool IsUsableItem(string type) {
    return IsValueInArray(Constants.usableItemTypes, type);
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
    return IsValueInArray(resistances, "fire");
  }

  public static bool IsPoisonResistant(string[] resistances) {
    return IsValueInArray(resistances, "poison");
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
    return IsValueInArray(Constants.forbiddenKeys, keyCode) || keyCode.Contains("Mouse") || keyCode.Contains("Button9");
  }

  public static bool IsGamepadKey(string keyCode) {
    return keyCode.Contains("Joystick");
  }

  public static void FocusUIElement(GameObject element) {
    Pause.eventSystem.SetSelectedGameObject(element, new BaseEventData(Pause.eventSystem));
  }

  public static List<Item> GetSpecificItemList(string[] includedTypes, List<Item> itemsList) {
    string types = String.Join(",", includedTypes);
    List<Item> specificItems = new List<Item>();

    foreach (Item currItem in itemsList) {
      if (IsValueInArray(includedTypes, Objects.pauseItems[currItem.key].type)) {
        specificItems.Add(currItem);
      }
    }

    return specificItems;
  }

  public static bool HasProjectilesForWeapon(string weapon, List<Item> list) {
    string[] availableProjectiles = Objects.itemProjectiles[weapon];
    string[] keyList = list.Select(item => item.key).ToArray();

    return availableProjectiles.Any(projectile => keyList.Contains(projectile));
  }

  public static Item GetItemFromList(List<Item> itemList, string key) {
    return itemList.Find(currItem => currItem.key == key);
  }

  public static string GetPauseItemKeyByName(string name) {
    return Objects.pauseItems.FirstOrDefault(currEntry => currEntry.Value.name == name).Key;
  }

  public static bool IsValueInArray(string[] arr, string val) {
    return System.Array.IndexOf(arr, val) != -1;
  }
  public static bool IsValueInArray(int[] arr, int val) {
    return System.Array.IndexOf(arr, val) != -1;
  }

  public static int ValueFrequencyInArray(string[] arr, string val) {
    int i = 0;
    int totalTimes = 0;
    foreach (string currentVal in arr) {
      if (currentVal != "") {
        if (currentVal == val) {
          totalTimes++;
        }
      }

      i++;
    }

    return totalTimes;
  }

  public static bool IsOnItemContainerState(string[] containerStates, string currentState) {
    return IsValueInArray(containerStates, currentState);
  }

  public static bool IsCritical(float rate) {
    float randomOutcome = UnityEngine.Random.Range(0.0f, 1.0f);
    return randomOutcome <= rate;
  }

  // determines the next level exp amount following the formula: f(x) = 50x^2 - 50x
  public static int NextLevelEXP(int newLevel) {
    return (50 * (int)Mathf.Pow(newLevel, 2)) - (50 * newLevel);
  }

  public static bool ExceedsTime(float start, float limit) {
    return Time.time * 1000 > start + limit;
  }

  public static int ChildCountWithTag(Transform tr, string tag, bool checkInactive = false) {
    int count = 0;
    Transform [] trs = tr.GetComponentsInChildren<Transform>(checkInactive);
    foreach(Transform t in trs) {
      if(t.gameObject.CompareTag(tag) == true) { count++; }
    }
    return count;
  }

  public static bool IsBottomCollision(float topObject, float bottomObject) {
    Debug.Log(topObject + " VS " + bottomObject);
    return topObject > bottomObject;
  }

  public static string GetRandomItemFromGroup(string[] itemGroup) {
    return itemGroup[UnityEngine.Random.Range(0, itemGroup.Length)];
  }

  public static string GetLevelString(int level) {
    return level >= 51 ? "high" : (level >= 21 ? "mid" : "low");
  }

  // TODO: include luck in this calculation
  public static string GetDroppableItem(string key, int level, float playerLuck) {
    string enemyLevel = GetLevelString(level);
    ProbabilityItem[] itemProbabilities = Objects.enemyDroppables[key][enemyLevel];
    float randomOutcome = UnityEngine.Random.Range(0.0f, 1.0f) + playerLuck;

    if (randomOutcome > 1) {
      randomOutcome = 1;
    }

    string randomItemKey = itemProbabilities.FirstOrDefault(item => randomOutcome <= item.probability).key;

    if (Helpers.IsValueInArray(Constants.recalculatableItemKeys, randomItemKey)) {
      return GetRandomItemFromGroup(Objects.itemGroups[randomItemKey]);
    }

    return randomItemKey;
  }

  public static string PascalToKebab(string input) {
    return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + char.ToLower(x) : x.ToString())).ToLower();
  }
}
