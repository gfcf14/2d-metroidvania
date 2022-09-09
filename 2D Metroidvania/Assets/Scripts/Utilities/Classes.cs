using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classes {}

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

public class Item {
  public string key { get; set; }
  public int amount { get; set; }

  public Item(string key, int amount) {
    this.key = key;
    this.amount = amount;
  }
}

public class PauseItem {
  public Sprite thumbnail;
  public Sprite image;
  public string name;
  public string description;
  public string type;

  // TODO: add values to increase player stats (atk, def, two-handed, etc.)
}
