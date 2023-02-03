using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damages {
  public static Dictionary<string, WeaponDamage> weaponDamages = new Dictionary<string, WeaponDamage> {
    {"sword-1", new WeaponDamage() {damage = 20}},
    {"longsword-1", new WeaponDamage() {damage = 40}},
    {"lance", new WeaponDamage() {damage = 60}},
    {"bomb", new WeaponDamage() {damage = 100}},
    {"king-bone", new WeaponDamage() {damage = 60}},
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
}
