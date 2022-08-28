using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects {
  public static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject> {
    {"arrow", Resources.Load("Prefabs/Arrow") as GameObject},
    {"arrow-burn", Resources.Load("Prefabs/ArrowBurn") as GameObject},
    {"arrow-explosion", Resources.Load("Prefabs/ArrowExplosion") as GameObject},
    {"enemy-explosion", Resources.Load("Prefabs/EnemyExplosion") as GameObject},
    {"pierce", Resources.Load("Prefabs/Pierce") as GameObject},
    {"throwable", Resources.Load("Prefabs/Throwable") as GameObject}
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
}
