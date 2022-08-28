using System.Collections.Generic;
using UnityEngine;

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
}
