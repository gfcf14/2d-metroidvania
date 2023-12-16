using UnityEngine;

public class Constants {
  public static string preferredInput = "gamepad";
  public static string[] fragmentableThrowables = { "watermelon" };
  public static string[] nonBouncingThrowables = { "lance", "knife", "kunai", "shuriken-4", "shuriken-6", "hatchet", "axe", "king-bone", "coconut", "honeydew", "watermelon" };
  public static string[] smallRotatingThrowables = { "shuriken-4", "shuriken-6", "hatchet" };
  public static string[] forbiddenKeys = {"Escape", "KeypadEnter", "Return", "W", "A", "S", "D", "UpArrow", "DownArrow", "RightArrow", "LeftArrow"};
  public static string[] usableItemTypes = {"consumable", "food", "throwable-food"};

  public static string[] bodyEquipmentTypes = {"body"};
  public static string[] armEquipmentTypes = {"double", "single", "defense", "throwable-double", "throwable", "bow", "throwable-food"};

  public static string[] throwableTypes = {"throwable", "throwable-double", "throwable-food"};
  public static string[] neckEquipmentTypes = {"neck"};
  public static string[] armwearEquipmentTypes = {"armwear"};
  public static string[] ringEquipmentTypes = {"ring"};

  public static string[] doubleHandedWeaponTypes = {"double", "throwable-double", "bow"};
  public static string[] projectileHoldingWeaponTypes = {"bow"};
  public static string[] shields = {"basic-shield"};
  public static string[] itemContainerStates = {"items", "equipment_select"};
  public static string[] stackableBreakables = {"barrel", "box"};
  public static string[] landingObjects = {"Breakable", "Ground", "Interactable"};
  public static string[] nonHorizontalCollidableObjects= {"Breakable", "Interactable"};
  public static string[] enemyNonColliderNames = {"Enemy", "EnemyCollider", "Grounder"};
  public static string[] enemyThrowableBouncers = {"Hero", "Shield", "Weapon"};
  public static string[] proximityCheckNonColliderTags = {"Breakable", "Enemy", "Ground", "Interactable", "Item"};
  public static string[] droppableNonColliderNames = {"ProximityCheck", "ChestOpener", "Grounder"};
  public static string[] droppableNonColliderTags = {"Enemy", "Hero"};

  public static string[] smallThrowables = {"knife", "kunai", "shuriken-4", "shuriken-6"};
  public static string[] angledThrowables = {"king-bone", "hatchet", "axe"};
  public static string[] rotatingThrowables = {"king-bone", "shuriken-4", "shuriken-6", "hatchet", "axe"};

  public static string[] nonGroundableThrowables = {"king-bone", "axe", "bomb"};

  // TODO: remove skeleton once testing for enemies is complete
  public static string[] meadowEnemies = {"skeleton", "pixie", "dwarf", "nymph", "goblin"};
  public static string[] flyingDeathEnemies = {"dwarf", "goblin", "pixie", "skeleton"};
  public static string[] flyingEnemies = {"pixie"};

  public static string[] lowLevelFood = {"chicken-drumstick", "apple", "banana", "orange", "pear", "strawberry", "cherry", "grapes", "mango"};
  public static string[] lowLevelMoney = {"money-50", "money-100"};
  public static string[] midLevelFood = {"pineapple", "coconut", "honeydew", "watermelon", "wine"};

  // TODO: wine is on high level food array as placeholder. Remove once better food items are implemented
  public static string[] highLevelFood = {"wine"};
  public static string[] goblinKnives = {"bandit-knife", "knife"};
  public static string[] goblinMidItem4 = {"silver-bar", "bronze-ingot"};
  public static string[] midLevelMoney = {"money-200", "money-500"};
  public static string[] goblinHighItem4 = {"silver-ingot", "gold-bar"};

  // TODO: silver bracelet is on low level bracelets array as placeholder. Remove once worse bracelets are implemented
  public static string[] lowLevelBracelets = {"silver-bracelet"};
  public static string[] lowLevelPotions = {"potion", "magic-ampoule"};

  // TODO: moonlight-pendant should not have such low stats. Remove from this list when worse pendants have been implemented
  public static string[] lowLevelPendants = {"moonlight-pendant"};
  public static string[] midLevelPotions = {"mid-potion", "magic-vial"};
  public static string[] highLevelPotions = {"high-potion", "magic-bottle"};
  public static string[] recalculatableItemKeys = {"goblin-high-item4", "goblin-knives", "goblin-mid-item4", "high-food", "high-potions", "low-bracelets", "low-food", "low-money", "low-pendants", "low-potions", "mid-food", "mid-money", "mid-potions"};
  public static string[] moneyItemKeys = {"money-50", "money-100", "money-200", "money-500", "money-1000", "money-2000", "money-5000"};
  public static string[] rotateDirections = {"west", "east"};

  // TODO: consider if this will be used again in the future. If not, delete
  // public static string[] patrollerStates = {"attack", "burning", "death", "death-by-burning", "death-by-poison", "stunned", "stunned-on-attack", "walk"};

  // ensure that these tiles are updated if the dirt position in the detail tileset is updated
  public static int[] detailDirt = {150, 151, 158, 159};
  public static int[] inclineMeadows = {163, 164, 165, 166, 168, 169, 170, 171};

  public static int arrowExplosionDamage = 50;
  public static int arrowPoisonDamage = 10;
  public static int kickDamage = 10;
  public static int punchDamage = 5;

  public static int minimumDamageDealt = 5;
  public static int maximumDamageDealt = 9999;
  public static int maxItemNameLength = 20;
  public static int maxItemDescriptionLength = 120;
  public static int maxItemCount = 999;

  // indicates the maximum "whole" items the items container can visualize
  public static int maxItemContainerHeight = 13;

  // Marks the default mandatory additional width for the Action Canvas text container
  public static int defaultActionTextContainerWidth = 76;
  public static int actionTextContainerHeight = 75;
  public static int actionTextHeight = 60;

  public static float[] HurtBTransitions = {0.009155554f, 0.01235556f, 0.01528889f, 0.01795555f, 0.02035556f, 0.02248894f, 0.0243555f,
                                            0.0259556f, 0.0272888f, 0.0283556f, 0.0291556f, 0.0296888f, 0.0299556f, 0.0299556f, 0.0296889f,
                                            0.0291556f, 0.0283554f, 0.027289f, 0.0259555f, 0.0243556f, 0.0224889f, 0.0203555f, 0.0179556f,
                                            0.0152889f, 0.0123555f, 0.0091556f, 0.0056889f, 0.0019555f};

  public static float[] hurtCXTransitions = {-0.003228738f, -0.02884259f, -0.07951818f, -0.1546794f, -0.25375f, -0.376154f, -0.5213152f,
                                              -0.6886574f, -0.8776046f, -1.087581f, -1.318009f, -1.568314f, -1.83792f, -2.12625f,
                                              -2.432728f, -2.756778f, -3.097824f, -3.45529f, -3.828599f, -4.217175f, -4.620445f,
                                              -5.037827f, -5.46875f, -5.912636f, -6.368908f, -6.836991f, -7.316309f, -7.806285f,
                                              -8.306343f, -8.815908f, -9.334402f, -9.861249f, -10.39588f, -10.9377f, -11.48616f,
                                              -12.04066f, -12.60064f, -13.16551f, -13.7347f, -14.59534f, -15.17281f, -15.75259f,
                                              -16.3341f, -16.91676f, -17.5f, -18.08324f, -18.95684f, -19.53755f, -20.11625f,
                                              -20.69236f, -21.2653f, -21.83449f, -22.39936f, -22.95934f, -23.51384f, -24.0623f,
                                              -24.60412f, -25.13875f, -25.6656f};

  public static float[] hurtCYTransitions = {0.09382155f, 0.2691962f, 0.4284767f, 0.5719797f, 0.7000217f, 0.8129194f, 0.9109893f,
                                              0.994548f, 1.063912f, 1.119398f, 1.161323f, 1.190002f, 1.205754f, 1.208894f, 1.199739f,
                                              1.178605f, 1.145809f, 1.101668f, 1.046498f, 0.9806156f, 0.904338f, 0.817981f, 0.7218618f,
                                              0.6162967f, 0.5016026f, 0.3780953f, 0.2460926f, 0.1059095f, -0.04213581f, -0.1977276f,
                                              -0.3605492f, -0.5302842f, -0.7066159f, -0.8892272f, -1.077803f, -1.272024f, -1.471576f,
                                              -1.676142f, -1.885404f, -2.099048f, -2.316756f, -2.53821f, -2.763095f, -2.991096f,
                                              -3.221893f, -3.45517f, -3.690613f, -3.927904f, -4.166725f, -4.406762f, -4.647697f,
                                              -4.889212f, -5.130994f, -5.372723f, -5.614084f, -5.85476f, -6.094436f, -6.332791f,
                                              -6.569514f};

  public static float[] enemyDeathXTransitions = {-0.02268519f, -0.08814816f, -0.1925f, -0.3318519f, -0.5023149f, -0.7f, -0.9210186f,
                                                  -1.161482f, -1.4175f, -1.685185f, -1.960648f, -2.24f, -2.519352f, -2.794815f, -3.0625f,
                                                  -3.318519f, -3.558982f, -3.78f, -3.977685f, -4.148149f, -4.287501f, -4.391853f, -4.457315f, -4.48f};

  public static float[] enemyDeathYTransitions = {0.230508f, 0.4401893f, 0.6300644f, 0.8011541f, 0.9544794f, 1.091061f, 1.211919f, 1.318075f,
                                                  1.41055f, 1.490363f, 1.558537f, 1.616092f, 1.664048f, 1.703426f, 1.735248f, 1.760533f, 1.780303f,
                                                  1.795577f, 1.807378f, 1.816726f, 1.824642f, 1.832145f, 1.840258f, 1.85f};

  public static float startItemY = 375;
  public static float itemIncrementY = 60;

  public static float hpAdjustDifference = 15;
  public static float mpAdjustDifference = 9;
  public static float maxHPDisplayableLimit = 500;

  public static float maxMPDisplayableLimit = 500;

  public static float minimumSoundPlayElapsedTime = 0.5f;

  public static float defaultRoomHeight = 9;
  public static float defaultRoomWidth = 16;

  // ensures that each HP unit (until reaching 500) displays in 2 UI units
  public static int containerMultiplier = 2;

  public static int infoCanvasRightAlignOffset = 30;

  public static float fragmentOffset = 0.1f;
  public static Vector2[] fragmentPositions = { new Vector2(-fragmentOffset, fragmentOffset), new Vector2(0, fragmentOffset), new Vector2(fragmentOffset, fragmentOffset),
                                                new Vector2(-fragmentOffset, 0),              new Vector2(0, 0),              new Vector2(fragmentOffset, 0),
                                                new Vector2(-fragmentOffset, -fragmentOffset), new Vector2(0, -fragmentOffset), new Vector2(fragmentOffset, -fragmentOffset)};
}
