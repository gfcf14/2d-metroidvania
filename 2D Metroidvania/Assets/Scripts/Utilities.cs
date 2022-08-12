using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage {
  public int damage;
  // TODO: include property for when a weapon is allowed to damage so long as collision happens, not just on enter
}

public class ThrowableObject {
  public bool hasExtra;
  public bool hasAnim;

  public float initialAngle;
  public float startX;
  public float startY;

  public int gravityResistance;
  public int maxDistance;

  public Vector2 colliderOffset;
  public Vector2 colliderSize;
}

public class ArrowObject {
  public Sprite sprite;

  public bool hasExtra;

  public int gravityResistance;
}

public class Utilities {
  public static Dictionary<string, Color> elementResistancesColors = new Dictionary<string, Color> {
    {"fire", new Color(0.9f, 0, 0)},
    {"poison", new Color(0.7f, 0.86f, 0.19f)}
  };

  public static Dictionary<string, Color> specialColors = new Dictionary<string, Color> {
    {"ash", new Color(0.3f, 0.3f, 0.3f)},
    {"poisoned", new Color(0.4f, 0, 0.4f)}
  };

  public static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject> {
    {"arrow", Resources.Load("Prefabs/Arrow") as GameObject},
    {"arrow-burn", Resources.Load("Prefabs/ArrowBurn") as GameObject},
    {"arrow-explosion", Resources.Load("Prefabs/ArrowExplosion") as GameObject},
    {"enemy-explosion", Resources.Load("Prefabs/EnemyExplosion") as GameObject},
    {"pierce", Resources.Load("Prefabs/Pierce") as GameObject},
    {"throwable", Resources.Load("Prefabs/Throwable") as GameObject}
  };

  public static Dictionary<string, Sprite> throwableSprites = new Dictionary<string, Sprite> {
    {"lance", Resources.Load<Sprite>("Sprites/lance")},
    {"knife", Resources.Load<Sprite>("Sprites/knife")},
    {"kunai", Resources.Load<Sprite>("Sprites/kunai")},
    {"shuriken-4", Resources.Load<Sprite>("Sprites/shuriken-4")},
    {"shuriken-6", Resources.Load<Sprite>("Sprites/shuriken-6")},
    {"hatchet", Resources.Load<Sprite>("Sprites/hatchet")},
    {"axe", Resources.Load<Sprite>("Sprites/axe")}
  };

  public static Dictionary<string, ArrowObject> arrows = new Dictionary<string, ArrowObject> {
    {"arrow-fire", new ArrowObject() {sprite = Resources.Load<Sprite>("Sprites/arrow-fire"), hasExtra = true, gravityResistance = 6}},
    {"arrow-poison", new ArrowObject() {sprite = Resources.Load<Sprite>("Sprites/arrow-poison"), hasExtra = false, gravityResistance = 8}},
    {"arrow-standard", new ArrowObject() {sprite = Resources.Load<Sprite>("Sprites/arrow-standard"), hasExtra = false, gravityResistance = 10}}
  };

  public static Dictionary<string, ThrowableObject> throwableObjects = new Dictionary<string, ThrowableObject> {
    {"lance", new ThrowableObject() {hasExtra = false, hasAnim = false, initialAngle = 30f, startX = 0.5f, startY = 0.5f, gravityResistance = 0, maxDistance = 4, colliderOffset = new Vector2(1.4f, -0.14f), colliderSize = new Vector2(0.35f, 1.25f)}},
    {"bomb", new ThrowableObject() {hasExtra = true, hasAnim = true, initialAngle = 0f, startX = 0.5f, startY = 0.5f, gravityResistance = 0, maxDistance = 4, colliderOffset = new Vector2(12.5f, -12.5f), colliderSize = new Vector2(0.6f, 0.6f)}},
    {"knife", new ThrowableObject() {hasExtra = false, hasAnim = false, initialAngle = 0f, startX = 0.75f, startY = 0.75f, gravityResistance = 2, maxDistance = 0, colliderOffset = new Vector2(0.15f, -0.15f), colliderSize = new Vector2(0.2f, 1.18f)}},
    {"kunai", new ThrowableObject() {hasExtra = false, hasAnim = false, initialAngle = 0f, startX = 0.75f, startY = 0.75f, gravityResistance = 5, maxDistance = 0, colliderOffset = new Vector2(0.15f, -0.15f), colliderSize = new Vector2(0.2f, 1.18f)}},
    {"shuriken-4", new ThrowableObject() {hasExtra = false, hasAnim = false, initialAngle = 0f, startX = 0.75f, startY = 0.75f, gravityResistance = 0, maxDistance = 4, colliderOffset = new Vector2(0, 0), colliderSize = new Vector2(0.6f, 0.6f)}},
    {"shuriken-6", new ThrowableObject() {hasExtra = false, hasAnim = false, initialAngle = 0f, startX = 0.75f, startY = 0.75f, gravityResistance = 0, maxDistance = 4, colliderOffset = new Vector2(0, 0), colliderSize = new Vector2(0.6f, 0.6f)}},
    {"hatchet", new ThrowableObject() {hasExtra = false, hasAnim = false, initialAngle = 0f, startX = 0.75f, startY = 0.75f, gravityResistance = 0, maxDistance = 4, colliderOffset = new Vector2(0.4f, 0), colliderSize = new Vector2(0.6f, 0.8f)}},
    {"axe", new ThrowableObject() {hasExtra = false, hasAnim = false, initialAngle = 0f, startX = 0.5f, startY = 0.5f, gravityResistance = 0, maxDistance = 4, colliderOffset = new Vector2(0.6f, 0), colliderSize = new Vector2(1.1f, 0.8f)}}
  };

  public static Dictionary<string, WeaponDamage> weaponDamages = new Dictionary<string, WeaponDamage> {
    {"sword-1", new WeaponDamage() {damage = 20}},
    {"longsword-1", new WeaponDamage() {damage = 40}},
    {"lance", new WeaponDamage() {damage = 60}},
    {"bomb", new WeaponDamage() {damage = 100}},
    {"knife", new WeaponDamage() {damage = 15}},
    {"kunai", new WeaponDamage() {damage = 10}},
    {"shuriken-4", new WeaponDamage() {damage = 5}},
    {"shuriken-6", new WeaponDamage() {damage = 10}},
    {"hatchet", new WeaponDamage() {damage = 20}},
    {"axe", new WeaponDamage() {damage = 40}},
    {"arrow-fire", new WeaponDamage() {damage = 5}},
    {"arrow-poison", new WeaponDamage() {damage = 10}},
    {"arrow-standard", new WeaponDamage() {damage = 20}}
  };

  public static string[] groundThrowables = { "lance", "bomb", "knife", "kunai", "shuriken-4", "shuriken-6", "hatchet" };
  public static string[] nonBouncingThrowables = { "lance", "knife", "kunai", "shuriken-4", "shuriken-6", "hatchet", "axe" };
  public static string[] smallRotatingThrowables = { "shuriken-4", "shuriken-6", "hatchet" };

  public static int arrowExplosionDamage = 50;

  public static int GetDamage(string weaponWielded) {
	  return weaponDamages[weaponWielded].damage;
  }

  public static bool IsGroundThrowable(string type) {
    return System.Array.IndexOf(groundThrowables, type) != -1;
  }

  public static bool IsNonBouncingThrowable(string type) {
    return System.Array.IndexOf(nonBouncingThrowables, type) != -1;
  }

  public static bool IsSmallRotatingThrowable(string type) {
    return System.Array.IndexOf(smallRotatingThrowables, type) != -1;
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
      r += elementResistancesColors[currentElementResistance].r;
      g += elementResistancesColors[currentElementResistance].g;
      b += elementResistancesColors[currentElementResistance].b;
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
