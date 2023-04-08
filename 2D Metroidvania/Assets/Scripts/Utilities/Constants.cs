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

  public static float[] hurtCXTransitions = {-0.0133081f, -0.05283513f, -0.1179852f, -0.2081625f, -0.322771f, -0.4612149f, -0.6228983f,
                                              -0.8072253f, -1.0136f, -1.241427f, -1.490109f, -1.759052f, -2.047659f, -2.355334f, -2.681482f,
                                              -3.025506f, -3.386811f, -3.764801f, -4.158878f, -4.56845f, -4.992919f, -5.431689f, -5.884164f,
                                              -6.349749f, -6.827847f, -7.317863f, -7.8192f, -8.331264f, -8.853458f, -9.385186f, -9.925852f,
                                              -10.47486f, -11.03162f, -11.59552f, -12.16598f, -12.7424f, -13.32418f, -13.91073f, -14.50145f,
                                              -15.09575f, -15.69302f, -16.29268f, -16.89413f, -17.49677f, -18.1f, -18.70324f, -19.30588f,
                                              -19.90732f, -20.50698f, -21.10425f, -21.69855f, -22.28927f, -22.87582f, -23.4576f, -24.03402f,
                                              -24.60448f, -25.16839f, -25.72514f, -26.27415f, -26.81482f};

  public static float[] hurtCYTransitions = {0.2849069f, 0.5440728f, 0.7780132f, 0.9872433f, 1.172279f, 1.333635f, 1.471827f, 1.587371f,
                                              1.680781f, 1.752574f, 1.803265f, 1.833369f, 1.843401f, 1.833878f, 1.805314f, 1.758225f, 1.693126f,
                                              1.610532f, 1.51096f, 1.394924f, 1.26294f, 1.115523f, 0.95319f, 0.7764542f, 0.5858318f, 0.3818388f,
                                              0.1649906f, -0.06419907f, -0.3052122f, -0.5575352f, -0.8206525f, -1.094049f, -1.377207f, -1.669612f,
                                              -1.97075f, -2.280107f, -2.597163f, -2.921405f, -3.25232f, -3.589387f, -3.932098f, -4.27993f, -4.632371f,
                                              -4.988907f, -5.349021f, -5.712196f, -6.077921f, -6.445676f, -6.814947f, -7.185219f, -7.555978f, -7.926707f,
                                              -8.29689f, -8.666012f, -9.033559f, -9.399015f, -9.761862f, -10.12159f, -10.47768f, -10.82961f};

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
