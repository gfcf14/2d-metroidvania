using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants {
  public static string preferredInput = "gamepad";

  public static string[] nonBouncingThrowables = { "lance", "knife", "kunai", "shuriken-4", "shuriken-6", "hatchet", "axe", "king-bone" };
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
  public static string[] enemyNonColliderNames = {"Enemy", "EnemyCollider", "Grounder"};
  public static string[] enemyThrowableBouncers = {"Hero", "Shield", "Weapon"};
  public static string[] bossEntryCheckNonColliderTags = {"Breakable", "Enemy", "Ground", "Interactable", "Item"};
  public static string[] droppableNonColliderNames = {"BossEntryCheck", "ChestOpener", "Grounder"};
  public static string[] droppableNonColliderTags = {"Enemy", "Hero"};

  public static string[] smallThrowables = {"knife", "kunai", "shuriken-4", "shuriken-6"};
  public static string[] angledThrowables = {"king-bone", "hatchet", "axe"};
  public static string[] rotatingThrowables = {"king-bone", "shuriken-4", "shuriken-6", "hatchet", "axe"};

  public static string[] nonGroundableThrowables = {"king-bone", "axe"};

  public static int arrowExplosionDamage = 50;
  public static int arrowPoisonDamage = 10;
  public static int kickDamage = 10;
  public static int punchDamage = 5;

  public static int minimumDamageDealt = 5;
  public static int maxItemNameLength = 20;
  public static int maxItemDescriptionLength = 120;
  public static int maxItemCount = 999;

  // indicates the maximum "whole" items the items container can visualize
  public static int maxItemContainerHeight = 13;

  // Both of these keep track of the widths of HP and MP bars' left and right edges
  public static int existingHPWidth = 24;
  public static int existingMPWidth = 16;

  // Used to set a width per character for the Info Canvas
  public static int characterWidth = 23;

  public static float[] HurtBTransitions = {0.009155554f, 0.01235556f, 0.01528889f, 0.01795555f, 0.02035556f, 0.02248894f, 0.0243555f,
                                            0.0259556f, 0.0272888f, 0.0283556f, 0.0291556f, 0.0296888f, 0.0299556f, 0.0299556f, 0.0296889f,
                                            0.0291556f, 0.0283554f, 0.027289f, 0.0259555f, 0.0243556f, 0.0224889f, 0.0203555f, 0.0179556f,
                                            0.0152889f, 0.0123555f, 0.0091556f, 0.0056889f, 0.0019555f};

  public static float startItemY = 375;
  public static float itemIncrementY = 60;

  public static float hpBarLeft = -172.4f;
  public static float hpBarTop = 372;
  public static float hpMultiplier = 0.982f; // used to be 0.91f. keep testing
  public static float mpBarLeft = -173.5f;
  public static float mpBarTop = 344.6f;
  public static float mpMultiplier = 0.9833f; // used to be 0.65f. keep testing
  public static float maxHPDisplayableLimit = 1000;
  public static float hpContainerMaxWidth = maxHPDisplayableLimit - existingHPWidth;

  public static float maxMPDisplayableLimit = 1000;
  public static float mpContainerMaxWidth = maxMPDisplayableLimit - existingMPWidth;
}
