using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants {
  public static string preferredInput = "gamepad";
  public static string[] groundThrowables = { "lance", "bomb", "knife", "kunai", "shuriken-4", "shuriken-6", "hatchet" };
  public static string[] nonBouncingThrowables = { "lance", "knife", "kunai", "shuriken-4", "shuriken-6", "hatchet", "axe" };
  public static string[] smallRotatingThrowables = { "shuriken-4", "shuriken-6", "hatchet" };
  public static string[] forbiddenKeys = {"Escape", "KeypadEnter", "Return", "W", "A", "S", "D", "UpArrow", "DownArrow", "RightArrow", "LeftArrow"};
  public static int arrowExplosionDamage = 50;

  public static float startItemY = 252;
  public static float itemIncrementY = -48;
}
