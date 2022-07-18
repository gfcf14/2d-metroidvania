using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
  private SpriteRenderer objectRenderer;
  private CapsuleCollider2D hitBounds;
  private GameObject arrowAnchor;

  Hero hero;

  [System.NonSerialized] public GameObject extraSprite;

  [System.NonSerialized] public bool isFacingLeft;
  [System.NonSerialized] public bool hasCollided = false;
  [System.NonSerialized] public bool hasFired = false;

  [System.NonSerialized] public int transitionIncrement = 0;
  [System.NonSerialized] public int gravityResistance = 0;

  [System.NonSerialized] public float newAngle;
  [System.NonSerialized] public float startX;
  [System.NonSerialized] public float startY;
  [System.NonSerialized] public float distanceMultiplier = 0.0125f;
  [System.NonSerialized] public float collideTime;
  [System.NonSerialized] public float maxEllapsedCollideTime = 1500f;

  [System.NonSerialized] public string type;

  int direction;

  void Start() {
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
    arrowAnchor = hero.transform.Find("ArrowAnchor").gameObject;
    objectRenderer = GetComponent<SpriteRenderer>();
    hitBounds = transform.Find("ArrowCollider").gameObject.GetComponent<CapsuleCollider2D>();
    extraSprite = transform.Find("Extra").gameObject;
    direction = isFacingLeft ? -1 : 1;

    hitBounds.offset = new Vector2(0.4f, 0);
    hitBounds.size = new Vector2(0.3f, 0.2f);

    ArrowObject currentArrow = Utilities.arrows[type];

    if (!currentArrow.hasExtra) {
      DestroyExtra();
    }

    objectRenderer.sprite = currentArrow.sprite;

    // TODO: add extra check when making the fire arrow

    gravityResistance = currentArrow.gravityResistance;

    if (isFacingLeft) {
      transform.localScale = new Vector3(-1, 1, 1);
    }
  }

  void Update() {
    // TODO: consider updating this to keep object "alive" for some time/length after off camera
    if (!objectRenderer.isVisible) {
      Destroy(gameObject);
    }

    if (hasFired) {
      if (!hasCollided) {
        float newX = direction * distanceMultiplier * transitionIncrement;
        float heightDrop = (transitionIncrement / gravityResistance);

        newAngle = -0.5f * direction * heightDrop;

        transform.position = new Vector2(startX + (newX * 5), startY - (heightDrop * distanceMultiplier));
        transform.rotation = Quaternion.Euler(0, 0, newAngle);

        transitionIncrement++;
      } else {
        float ellapsedCollideTime = (Time.time * 1000) - collideTime;

        if (ellapsedCollideTime < maxEllapsedCollideTime) {
          objectRenderer.color = new Color(1, 1, 1, 1 - (ellapsedCollideTime / maxEllapsedCollideTime));
        } else {
          DestroyArrow();
        }
      }
    } else {
      transform.position = arrowAnchor.GetComponent<Transform>().position;
      startX = transform.position.x;
      startY = transform.position.y;
    }
  }

  void DestroyExtra() {
    if (extraSprite != null) {
      Destroy(extraSprite);
    }
  }

  public void DestroyArrow() {
    Destroy(gameObject);
  }
}
