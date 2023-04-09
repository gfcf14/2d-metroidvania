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

  public static float[] hurtCXTransitions = {-0.003228738f, -0.01286694f, -0.02884259f, -0.05108368f, -0.07951818f, -0.1140741f,
                                              -0.1546794f, -0.201262f, -0.25375f, -0.3120714f, -0.376154f, -0.445926f, -0.5213152f,
                                              -0.6022497f, -0.6886574f, -0.7804665f, -0.8776046f, -0.9800001f, -1.087581f, -1.200274f,
                                              -1.318009f, -1.440714f, -1.568314f, -1.700741f, -1.83792f, -1.97978f, -2.12625f,
                                              -2.277257f, -2.432728f, -2.592593f, -2.756778f, -2.925213f, -3.097824f, -3.27454f,
                                              -3.45529f, -3.64f, -3.828599f, -4.021015f, -4.217175f, -4.41701f, -4.620445f,
                                              -4.827407f, -5.037827f, -5.251633f, -5.46875f, -5.689108f, -5.912636f, -6.13926f,
                                              -6.368908f, -6.601509f, -6.836991f, -7.075282f, -7.316309f, -7.56f, -7.806285f,-8.05509f,
                                              -8.306343f, -8.559972f, -8.815908f, -9.074075f, -9.334402f, -9.596817f, -9.861249f,
                                              -10.12763f, -10.39588f, -10.66593f, -10.9377f, -11.21114f, -11.48616f, -11.76269f,
                                              -12.04066f, -12.32f, -12.60064f, -12.8825f, -13.16551f, -13.4496f, -13.7347f, -14.02074f,
                                              -14.30764f, -14.59534f, -14.88375f, -15.17281f, -15.46245f, -15.75259f, -16.04317f,
                                              -16.3341f, -16.62533f, -16.91676f, -17.20835f, -17.5f, -17.79166f, -18.08324f, -18.37468f,
                                              -18.6659f, -18.95684f, -19.24741f, -19.53755f, -19.82719f, -20.11625f, -20.40466f,
                                              -20.69236f, -20.97926f, -21.2653f, -21.5504f, -21.83449f, -22.1175f, -22.39936f, -22.68f,
                                              -22.95934f, -23.23731f, -23.51384f, -23.78886f, -24.0623f, -24.33408f, -24.60412f,
                                              -24.87238f, -25.13875f, -25.40318f, -25.6656f, -25.92593f};

  public static float[] hurtCYTransitions = {0.09382155f, 0.1835404f, 0.2691962f, 0.3508284f, 0.4284767f, 0.5021806f, 0.5719797f,
                                              0.6379135f, 0.7000217f, 0.7583439f, 0.8129194f, 0.8637881f, 0.9109893f, 0.9545628f,
                                              0.994548f, 1.030985f, 1.063912f, 1.09337f, 1.119398f, 1.142036f, 1.161323f, 1.177299f,
                                              1.190002f, 1.199474f, 1.205754f, 1.208881f, 1.208894f, 1.205833f, 1.199739f, 1.190649f,
                                              1.178605f, 1.163645f, 1.145809f, 1.125137f, 1.101668f, 1.075441f, 1.046498f, 1.014876f,
                                              0.9806156f, 0.9437565f, 0.904338f, 0.8623996f, 0.817981f, 0.7711219f, 0.7218618f, 0.6702403f,
                                              0.6162967f, 0.5600712f, 0.5016026f, 0.4409309f, 0.3780953f, 0.3131362f, 0.2460926f,
                                              0.1770039f, 0.1059095f, 0.03284989f, -0.04213581f, -0.119008f, -0.1977276f, -0.2782545f,
                                              -0.3605492f, -0.4445726f, -0.5302842f, -0.6176458f, -0.7066159f, -0.7971563f, -0.8892272f,
                                              -0.9827893f, -1.077803f, -1.174227f, -1.272024f, -1.371154f, -1.471576f, -1.573252f, -1.676142f,
                                              -1.780206f, -1.885404f, -1.991699f, -2.099048f, -2.207414f, -2.316756f, -2.427034f, -2.53821f,
                                              -2.650244f, -2.763095f, -2.876725f, -2.991096f, -3.106164f, -3.221893f, -3.338242f, -3.45517f,
                                              -3.57264f, -3.690613f, -3.809049f, -3.927904f, -4.047143f, -4.166725f, -4.286611f, -4.406762f,
                                              -4.527136f, -4.647697f, -4.768402f, -4.889212f, -5.010089f, -5.130994f, -5.251885f, -5.372723f,
                                              -5.493469f, -5.614084f, -5.734528f, -5.85476f, -5.974743f, -6.094436f, -6.213798f, -6.332791f,
                                              -6.451376f, -6.569514f, -6.687163f, -6.804286f, -6.920841f};

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
