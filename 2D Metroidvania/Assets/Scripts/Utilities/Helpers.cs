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

  public static bool IsStartKeyDown() {
    return Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton9);
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

  public static int GetItemIndex(List<Item> itemList, string key) {
    return itemList.FindIndex(currItem => currItem.key == key);
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

  public static AudioClip GetRandomClipFromGroup(AudioClip[] audioclipGroup) {
    return audioclipGroup[UnityEngine.Random.Range(0, audioclipGroup.Length)];
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

    // gets the rarity of an item to play a sound when the item is picked
    bool isRare = itemProbabilities.FirstOrDefault(item => randomItemKey == item.key).probability == 1;
    string rarity = randomItemKey.Contains("money") || IsValueInArray(Constants.moneyItemKeys, randomItemKey) ? "money" : (isRare ? "rare" : "normal");

    // if the item is from a group, it needs to be recalculated
    if (Helpers.IsValueInArray(Constants.recalculatableItemKeys, randomItemKey)) {
      return GetRandomItemFromGroup(Objects.itemGroups[randomItemKey]) + "|" + rarity;
    }

    return randomItemKey + "|" + rarity;
  }

  public static string PascalToKebab(string input) {
    return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + char.ToLower(x) : x.ToString())).ToLower();
  }

  // splits the input by '-', capitalizes the first letter of each word, then joins them by a space
  public static string KebabToCharacter(string input) {
    return string.Join(" ", input.Split('-').Select(word => char.ToUpper(word[0]) + word.Substring(1)));
  }

  public static string KebabToObject(string input) {
    return string.Concat(input.Split('-').Select(word => char.ToUpper(word[0]) + word.Substring(1)));
  }

  public static bool HasAll(List<Item> items, string[] itemsToCheck) {
    return itemsToCheck.All(key => items.Any(item => item.key == key));
  }

  public static string GetThrowableSoundType(string itemType) {
    // TODO: include any other non-collidable throwables (e.g. ones that should explode on contact)
    if (itemType == "bomb") {
      return "";
    }

    PauseItem throwableItem = Objects.pauseItems[itemType];

    // TODO: account for throwable-double items which do not slash (e.g. king-bone)
    if (throwableItem.type == "throwable-double") {
      return "throwable-double-large";
    }

    return "throwable-" + (Helpers.IsValueInArray(Constants.smallThrowables, itemType) ? "small" : "middle");
  }

  public static int GetCharacterDisplayWidth(char c) {
    foreach (string key in Objects.characterWidths.Keys) {
      if (key.Contains(c)) {
        return Objects.characterWidths[key];
      }
    }

    return 0;
  }

  public static int GetTextDisplayWidth(string text) {
    int textDisplayWidth = 0;

    for (int i = 0; i < text.Length; i++) {
      textDisplayWidth += GetCharacterDisplayWidth(text[i]);
    }

    return textDisplayWidth;
  }

  public static string GetMaterial(string material, string tileName = null) {
    switch (material) {
      case "meadows":
        return "grass";
      case "details":
        return "";
      default:
        if (tileName != null) {
          Debug.Log("Material (" + material + ") not accounted for, using tile " + tileName);
        }
        return null;
    }
  }

  // gets tile index based on name passed
  public static int GetTileIndex(string tileName = null) {
    if (tileName == null) {
      return -1;
    }

    return int.Parse(tileName.Split('_')[1]);
  }

  // recursive function to get exp for levels between 21 and 40
  public static int GetEXP2140(int level) {
    if (level == 20) {
      return 10450;
    }

    return 200 * (level - 20) + 1000 + GetEXP2140(--level);
  }

  // recursive function to get exp for levels between 41 and 60
  public static int GetEXP4160(int level) {
    if (level == 40) {
      return 92450;
    }

    return 400 * (level - 40) + 1000 + GetEXP4160(--level);
  }

  // recursive function to get exp for levels between 61 and 80
  public static int GetEXP6180(int level) {
    if (level == 60) {
      return 196450;
    }

    return 600 * (level - 60) + 1000 + GetEXP6180(--level);
  }

  // recursive function to get exp for levels between 81 and 90
  public static int GetEXP8190(int level) {
    return 15000 * (level - 80) + 342450;
  }

  // recursive function to get exp for levels between 91 and 100
  public static int GetEXP91100(int level) {
    return 20000 * (level - 90) + 492450;
  }

  // determines the next level exp amount following the formula: f(x) = 50x^2 - 50x
  public static int NextLevelEXP(int nextLevel) {
    if (nextLevel >= 1 && nextLevel <= 20) {
      return (25 * (int)Mathf.Pow(nextLevel, 2)) + (25 * nextLevel) - 50;
    } else if (nextLevel >= 21 && nextLevel <= 40) {
      return GetEXP2140(nextLevel);
    } else if (nextLevel >= 41 && nextLevel <= 60) {
      return GetEXP4160(nextLevel);
    } else if (nextLevel >= 61 && nextLevel <= 80) {
      return GetEXP6180(nextLevel);
    } else if (nextLevel >= 81 && nextLevel <= 90) {
      return GetEXP8190(nextLevel);
    } else if (nextLevel >= 91 && nextLevel <= 100) {
      return GetEXP91100(nextLevel);
    }

    return GameData.highestEXP;
  }

  public static float GetStatByLevel(string stat, int level) {
    switch(stat) {
      case "HP":
      case "MP":
        if (level == 100) {
          return GameData.highestHP;
        } else if (level < 20) {
          return GameData.baseHP + (5 * (level - 1));
        } else if (level < 50) {
          return 200 + (10 * (level - 20));
        } else if (level < 80) {
          return 500 + (50 * (level - 50));
        } else if (level < 90) {
          return 2000 + (100 * (level - 80));
        } else if (level >= 90) {
          return 3000 + (200 * (level - 90));
        }
      break;
      case "STR":
        if (level == 100) {
          return GameData.highestSTR;
        } else if (level < 35) {
          return GameData.baseSTR + (5 * (level - 1));
        } else if (level < 49) {
          return 180 + (10 * (level - 35));
        } else if (level < 80) {
          return 330 + (20 * (level - 49));
        } else if (level < 90) {
          return 950 + (45 * (level - 80));
        } else if (level >= 90) {
          return 1400 + (60 * (level - 90));
        }
      break;
      case "STA":
        if (level == 100) {
          return GameData.highestSTA;
        } else if (level < 8) {
          return GameData.baseSTA + (5 * (level - 1));
        } else if (level < 41) {
          return 60 + (20 * (level - 8));
        } else if (level < 80) {
          return 725 + (25 * (level - 41));
        } else if (level < 90) {
          return 1700 + (30 * (level - 80));
        } else if (level >= 90) {
          return 2000 + (50 * (level - 90));
        }
      break;
      case "CRI":
        if (level == 100) {
          return GameData.highestCRI;
        } else if (level < 10) {
          return GameData.baseCRI + (0.01f * (level - 1));
        } else if (level < 20) {
          return 0.04f + (0.02f * (level - 10));
        } else if (level < 80) {
          return 0.06f + (0.025f * (level - 20));
        } else if (level < 90) {
          return 0.21f + (0.04f * (level - 80));
        } else if (level >= 90) {
          return 0.25f + (0.05f * (level - 90));
        }
      break;
      case "LCK":
        if (level == 100) {
          return GameData.highestLCK;
        } else if (level < 20) {
          return GameData.baseLCK + (0.0005f * (level - 1));
        } else if (level < 50) {
          return 0.035f + (0.002f * (level - 20));
        } else if (level < 80) {
          return 0.095f + (0.0025f * (level - 50));
        } else if (level < 90) {
          return 0.173f + (0.003f * (level - 80));
        } else if (level >= 90) {
          return 0.205f + (0.005f * (level - 90));
        }
      break;
    }

    return -1;
  }

  public static string TwoDecimalPlaces(float decimalValue, bool ignoreWhenWhole = false) {
    if (ignoreWhenWhole && (decimalValue == (int)decimalValue)) {
      return ((int) decimalValue).ToString();
    }
    return String.Format("{0:0.00}", decimalValue);
  }

  public static string TwoDecimalPlaces(float? decimalValue, bool ignoreWhenWhole = false) {
    if (ignoreWhenWhole && (decimalValue == (int)decimalValue)) {
      return ((int) decimalValue).ToString();
    }
    return String.Format("{0:0.00}", decimalValue);
  }

  public static int GetStatsOnEnemyLevel(int stat, int level) {
    return stat * level;
    // TODO: consider how the stats would be decided for higher levels, but for low levels this formula is too much
    // return stat * level * 2;
  }

  public static float GetStatsOnEnemyLevel(float stat, int level) {
    return stat * level;
    // TODO: consider how the stats would be decided for higher levels, but for low levels this formula is too much
    // return stat * level * 2;
  }

  public static int GetEnemyEXP(int heroLevel, int enemyLevel, int baseExp) {
    int equalLevelExp = (int)(baseExp * GameData.enemyEXPValues[(int)Mathf.Floor(heroLevel / 10)]);

    if (heroLevel == enemyLevel) {
      return equalLevelExp;
    }

    if (heroLevel > enemyLevel) {
      if (heroLevel - enemyLevel >= 5) {
        return 5;
      } else {
        return (int)(equalLevelExp * ((5 - (heroLevel - enemyLevel)) / 5f));
      }
    } else { // heroLevel < enemyLevel
      if (enemyLevel - heroLevel >= 5) {
        return equalLevelExp * 2;
      } else {
        return (int)(equalLevelExp * (1 + ((enemyLevel - heroLevel) / 5f)));
      }
    }
  }
}
