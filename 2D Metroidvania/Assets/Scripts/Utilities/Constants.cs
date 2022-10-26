using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants {
  public static string preferredInput = "gamepad";

  public static string[] groundThrowables = { "lance", "bomb", "knife", "kunai", "shuriken-4", "shuriken-6", "hatchet" };
  public static string[] nonBouncingThrowables = { "lance", "knife", "kunai", "shuriken-4", "shuriken-6", "hatchet", "axe" };
  public static string[] smallRotatingThrowables = { "shuriken-4", "shuriken-6", "hatchet" };
  public static string[] forbiddenKeys = {"Escape", "KeypadEnter", "Return", "W", "A", "S", "D", "UpArrow", "DownArrow", "RightArrow", "LeftArrow"};
  public static string[] usableItemTypes = {"food", "potion"};

  public static string[] bodyEquipmentTypes = {"body"};
  public static string[] armEquipmentTypes = {"double", "single", "defense", "throwable-double", "throwable", "bow"};
  public static string[] neckEquipmentTypes = {"neck"};
  public static string[] armwearEquipmentTypes = {"armwear"};
  public static string[] ringEquipmentTypes = {"ring"};

  public static string[] doubleHandedWeaponTypes = {"double", "throwable-double", "bow"};

  public static string[] projectileHoldingWeaponTypes = {"bow"};

  public static string[] shields = {"basic-shield"};

  public static string[] itemContainerStates = {"items", "equipment_select"};

  public static int arrowExplosionDamage = 50;
  public static int maxItemNameLength = 20;
  public static int maxItemDescriptionLength = 120;
  public static int maxItemCount = 999;

  // indicates the maximum "whole" items the items container can visualize
  public static int maxItemContainerHeight = 13;

  public static float startItemY = 375;
  public static float itemIncrementY = 60;
}
