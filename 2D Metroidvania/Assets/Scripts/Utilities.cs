using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage {
  public int damage;
  // TODO: include property for when a weapon is allowed to damage so long as collision happens, not just on enter
}

public class Utilities {
  public static Dictionary<string, WeaponDamage> weaponDamages = new Dictionary<string, WeaponDamage> {
    {"sword-1", new WeaponDamage() {damage = 20}},
    {"longsword-1", new WeaponDamage() {damage = 40}},
    {"lance", new WeaponDamage() {damage = 60}}
  };

  public static int GetDamage(string weaponWielded) {
	return weaponDamages[weaponWielded].damage;
  }
}
