using System;
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

public class MagicResistance {
  public string name;
  public string type; // add or remove
}

public class Effects {
  public float? duration; // to be measured in seconds
  public int? hp;
  public int? mp;
  public string[] statusHeal;
  public int? atk;
  public int? def;
  public float? crit; // number between 0 and 1
  public float? luck;
  public MagicResistance[] magicResistances;
}

public class PauseItem {
  public Sprite thumbnail;
  public Sprite image;
  public string name;
  public string description;
  public string type;
  public Effects effects;

  public PauseItem(Sprite thumbnail, Sprite image, string name, string description, string type, Effects effects = null) {
    this.thumbnail = thumbnail;
    this.image = image;

    if (name.Length > Constants.maxItemNameLength) {
      throw new Exception("An item name (\"" + name + "\") must not exceed " + Constants.maxItemNameLength + " characters");
    } else {
      this.name = name;
    }

    if (description.Length > Constants.maxItemDescriptionLength) {
      throw new Exception("An item description (\"" + description + "\") must not exceed " + Constants.maxItemDescriptionLength + " characters");
    } else {
      this.description = description;
    }

    this.type = type;

    if (effects != null) {
      this.effects = effects;
    }
  }

  // TODO: add values to increase player stats (atk, def, two-handed, etc.)
}

public class HeroMagicResistance {
  public string name;
  public int frequency;
}
