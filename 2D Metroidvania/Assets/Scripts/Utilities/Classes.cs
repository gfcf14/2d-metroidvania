using System;
using UnityEngine;

public class Classes {}

public class WeaponDamage {
  public int damage;
  // TODO: include property for when a weapon is allowed to damage so long as collision happens, not just on enter
}

public class Item {
  public string key { get; set; }
  public int amount { get; set; }

  public Item(string key, int amount) {
    this.key = key;
    this.amount = amount;
  }
}

public class ProbabilityItem {
  public string key;
  public float probability;
}

public class Consumable {
  public string key;
  public float duration;
  public float useTime;
}

public class MagicResistance {
  public string name;
  public string type; // add or remove
}

public class Effects {
  public float? duration; // to be measured in seconds
  public int? hp;
  public float? hpPercentage;
  public int? mp;
  public float? mpPercentage;
  public string[] statusHeal;
  public int? atk;
  public int? def;
  public float? crit; // number between 0 and 1
  public float? luck;
  public MagicResistance[] magicResistances;
}

public class RelicEffect {
  public string type;
  public string value;

  public RelicEffect(string type, string value) {
    this.type = type;
    this.value = value;
  }
}

public class PauseItem {
  public Sprite thumbnail;
  public Sprite image;
  public string name;
  public string description;

  public PauseItem(Sprite thumbnail, Sprite image, string name, string description) {
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
  }
}

public class RegularItem : PauseItem {
  public string type;
  public Effects effects;

  public RegularItem(Sprite thumbnail, Sprite image, string name, string description, string type, Effects effects = null) : base(thumbnail, image, name, description) {
    this.type = type;

    if (effects != null) {
      this.effects = effects;
    }
  }

  // TODO: add values to increase player stats (atk, def, two-handed, etc.)
}

public class RelicItem : PauseItem {
  public RelicEffect effect;

  public RelicItem(Sprite thumbnail, Sprite image, string name, string description, RelicEffect effect) : base(thumbnail, image, name, description) {
    this.effect = effect;
  }
}

public class CompositePauseImage {
  public Sprite thumbnail;
  public string name;
}

public class HeroMagicResistance {
  public string name;
  public int frequency;
}

public class BreakableDimension {
  public Vector2 offset;
  public Vector2 size;
}

public class MoneyItem {
  public Sprite image;
  public int increment;
  public string text;
}

public class EnemyHealth {
  public int current;
  public int maximum;

  public EnemyHealth(int current, int maximum) {
    this.current = current;
    this.maximum = maximum;
  }
}

public class EnemyStats {
  public string name;
  public string type;
  public string baseMaterial;
  public string normalAttackType;
  public int hp;
  public int atk;
  public int def;
  public float crit;
  public int exp;
  public int speed;
  public float reach;
  public float edgeCastLength;
  public float arrowBurnPosition;
}

public class Outcome {
  public string outcomeCase;
  public string outcomeValue;
}

public class ChatLine {
  public string character;
  public string emotion;
  public string line;
  public Outcome outcome;
}

public class Condition {
  public string conditionCheck;
  public string conditionValue;
}

public class ChatNode {
  public Condition nodeCondition;
  public ChatLine[] nodeLines;
  public string nextNode;
  public string fallbackNode;
}

public class FragmentOutcome {
  public string key;
  public int count;
}
