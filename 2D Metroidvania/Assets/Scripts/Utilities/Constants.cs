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

  public static string[] stackableBreakables = {"barrel", "box"};

  public static string[] landingObjects = {"Breakable", "Ground"};

  public static int arrowExplosionDamage = 50;
  public static int maxItemNameLength = 20;
  public static int maxItemDescriptionLength = 120;
  public static int maxItemCount = 999;

  // indicates the maximum "whole" items the items container can visualize
  public static int maxItemContainerHeight = 13;

  // Both of these keep track of the widths of HP and MP bars' left and right edges
  public static int existingHPWidth = 24;
  public static int existingMPWidth = 16;

  public static float startItemY = 375;
  public static float itemIncrementY = 60;

  public static float hpBarLeft = -172.4f;
  public static float hpBarTop = 372;
  public static float hpMultiplier = 0.982f; // used to be 0.91f. keep testing
  public static float mpBarLeft = -173.5f;
  public static float mpBarTop = 344.6f;
  public static float mpMultiplier = 0.9833f; // used to be 0.65f. keep testing
}
