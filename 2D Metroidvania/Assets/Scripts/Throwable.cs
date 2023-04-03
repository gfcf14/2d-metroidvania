using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {
  private SpriteRenderer objectRenderer;
  private CapsuleCollider2D hitBounds;

  [System.NonSerialized] public GameObject extraSprite;
  [System.NonSerialized] public GameObject throwableCollider;
  [System.NonSerialized] public Animator anim;

  [System.NonSerialized] public Sprite bounceSprite;

  [System.NonSerialized] public bool isFacingLeft;
  [System.NonSerialized] public bool hasCollided = false;
  [System.NonSerialized] public bool mustBounce = false;
  [System.NonSerialized] public bool mustFall = false;
  [System.NonSerialized] public bool isExploding = false;

  [System.NonSerialized] public int transitionIncrement = 0;
  [System.NonSerialized] public int gravityResistance = 0;
  [System.NonSerialized] public int distance = 0;

  [System.NonSerialized] public float bounceRotationMultiplier = 3;
  [System.NonSerialized] public float bounceX;
  [System.NonSerialized] public float bounceY;
  [System.NonSerialized] public float newAngle;
  [System.NonSerialized] public float startX;
  [System.NonSerialized] public float startY;
  [System.NonSerialized] public float maxDistance;
  [System.NonSerialized] public float distanceMultiplier = 0.0125f;
  [System.NonSerialized] public float collideTime;
  [System.NonSerialized] public float maxEllapsedCollideTime = 1500f;

  [System.NonSerialized] public string type;

  float initialAngle;
  float rotationAngle;
  int direction;

  // follows the formula f(x) = -1/2(x^2) + mx/2
  float parabolaValue(float x) {
    return (-0.5f * Mathf.Pow(x, 2)) + (direction * maxDistance * x * 0.5f);
  }

  // follows the formula f(x) = -1/4(x^2) + mx/4
  float flatParabolaValue(float x) {
    return (-0.25f * Mathf.Pow(x, 2)) + (direction * maxDistance * x * 0.25f);
  }

  // follows the formula f(x) = -1/32(x^2) + 1/8(x)
  float shurikenParabolaValue(float x) {
    return (-0.03125f * Mathf.Pow(x, 2)) + (direction * x * 0.125f);
  }

  // follows the formula f(x) = -1/4(x^2) - mx
  float bounceParabolaValue(float x) {
    return (-0.25f * Mathf.Pow(x, 2)) - (direction * x);
  }

  void Start() {
    anim = GetComponent<Animator>();
    objectRenderer = GetComponent<SpriteRenderer>();
    throwableCollider = transform.Find("ThrowableCollider").gameObject;
    hitBounds = throwableCollider.GetComponent<CapsuleCollider2D>();
    extraSprite = transform.Find("Extra").gameObject;
    direction = isFacingLeft ? -1 : 1;

    ThrowableObject currentThrowable = Objects.throwableObjects[type];

    if (!(type == "king-bone" && throwableCollider.tag == "EnemyWeapon") && type != "lance" && type != "bomb" && type != "knife" && type != "kunai" && type != "shuriken-4" && type != "shuriken-6" && type != "hatchet" && type != "axe") {
      hitBounds.offset = currentThrowable.colliderOffset;
      hitBounds.size = currentThrowable.colliderSize;

      if (!currentThrowable.hasExtra) {
        DestroyExtra();
      }

      if (!currentThrowable.hasAnim) {
        Destroy(anim);
        objectRenderer.sprite = Sprites.throwableSprites[type];
      } else {
        if (type == "bomb") {
          anim.SetBool("isBomb", true);
        }
      }

      if (currentThrowable.initialAngle != 0f) {
        initialAngle = direction * currentThrowable.initialAngle;
        transform.rotation = Quaternion.Euler(0, 0, initialAngle);
      }

      if (currentThrowable.gravityResistance != 0) {
        gravityResistance = currentThrowable.gravityResistance;
      }

      rotationAngle = initialAngle == 0 ? 0 : (maxDistance / initialAngle);
    }

    if (isFacingLeft) {
      // transform.localScale = new Vector3(-1, Helpers.IsValueInArray(Constants.woodCuttingThrowables, type) ? -1 : 1, 1);
      transform.localScale = new Vector3(-1, 1, 1);
    }

    if (type == "king-bone" && throwableCollider.tag == "EnemyWeapon") {
      DestroyExtra();
      initialAngle = 45 * (isFacingLeft ? 1 : -1);
      anim.Play("king-bone-" + distance + (isFacingLeft ? "-left" : ""));
    }

    if (type == "lance") {
      DestroyExtra();
      anim.Play("lance" + (isFacingLeft ? "-left" : ""));
    }

    if (type == "bomb") {
      anim.Play("bomb" + (isFacingLeft ? "-left" : ""));
    }

    if (type == "knife") {
      DestroyExtra();
      anim.Play("knife" + (isFacingLeft ? "-left" : ""));
    }

    if (type == "kunai") {
      DestroyExtra();
      anim.Play("kunai" + (isFacingLeft ? "-left" : ""));
    }

    if (type == "shuriken-4") {
      DestroyExtra();
      anim.Play("shuriken-4" + (isFacingLeft ? "-left" : ""));
    }

    if (type == "shuriken-6") {
      DestroyExtra();
      anim.Play("shuriken-6" + (isFacingLeft ? "-left" : ""));
    }

    if (type == "hatchet") {
      DestroyExtra();
      anim.Play("hatchet" + (isFacingLeft ? "-left" : ""));
    }

    if (type == "axe") {
      DestroyExtra();
      anim.Play("axe" + (isFacingLeft ? "-left" : ""));
    }
  }

  void Update() {
    // TODO: consider updating this to keep object "alive" for some time/length after off camera
    if (!objectRenderer.isVisible) {
      DestroyThrowable();
    }

    if (!(type == "king-bone" && throwableCollider.tag == "EnemyWeapon") && type != "lance" && type != "bomb" && type != "knife" && type != "kunai" && type != "shuriken-4" && type != "shuriken-6" && type != "hatchet" && type != "axe") {
      if (!hasCollided) {
        if (!mustFall) {
          float newX = direction * distanceMultiplier * transitionIncrement;

          if (Helpers.IsValueInArray(Constants.parabolaThrowables, type)) {
            if (type == "lance") {
              newAngle = initialAngle - (rotationAngle * transitionIncrement);
            } else if (type == "axe" || type == "king-bone") {
              newAngle = initialAngle - (transitionIncrement * bounceRotationMultiplier * 0.75f) * (isFacingLeft ? -1 : 1);
            }

            transform.position = new Vector2(startX + newX, startY + (type == "king-bone" ? flatParabolaValue(newX) : parabolaValue(newX)));

            if (type != "bomb") {
              transform.rotation = Quaternion.Euler(0, 0, newAngle);
            }
          } else if (type == "knife" || type == "kunai") {
            float heightDrop = (transitionIncrement / gravityResistance);

            newAngle = -0.5f * direction * heightDrop;

            transform.position = new Vector2(startX + (newX * 5), startY - (heightDrop * distanceMultiplier));
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
          } else if (Helpers.IsSmallRotatingThrowable(type)) {
            newAngle = initialAngle - (transitionIncrement * bounceRotationMultiplier);

            transform.position = new Vector2(startX + newX, startY + shurikenParabolaValue(newX * (type == "shuriken-4" ? 2 : 1)));
            transform.rotation = Quaternion.Euler(0, 0, newAngle * (type == "hatchet" ? 0.5f * (isFacingLeft ? -1 : 1) : 1));

            // ensures transition increment doubles so rotating throwables travel faster
            transitionIncrement++;
          }

          transitionIncrement++;

          if (Helpers.IsValueInArray(Constants.parabolaThrowables, type)) {
            transitionIncrement++;
          }
        } else {
          transform.position = new Vector2(transform.position.x, transform.position.y - 0.025f);
        }
      } else {
        if (mustBounce) {
          float newX = -1 * direction * distanceMultiplier * transitionIncrement;
          float bounceAngle = newAngle - (transitionIncrement * bounceRotationMultiplier);

          transform.position = new Vector2(bounceX + newX, bounceY + bounceParabolaValue(newX));
          transform.rotation = Quaternion.Euler(0, 0, bounceAngle);

          transitionIncrement++;
        }

        float ellapsedCollideTime = (Time.time * 1000) - collideTime;

        if (ellapsedCollideTime < maxEllapsedCollideTime) {
          if (Helpers.IsNonBouncingThrowable(type)) {
            objectRenderer.color = new Color(1, 1, 1, 1 - (ellapsedCollideTime / maxEllapsedCollideTime));
          }
        } else {
          if (Helpers.IsNonBouncingThrowable(type)) {
            Destroy(gameObject);
          } else if (type == "bomb") {
            isExploding = true;
            DestroyExtra();
            anim.speed = 1;
            anim.SetBool("isExploding", isExploding);
          }
        }
      }
    } else {
      if (mustBounce) {
        anim.speed = 0;
        anim.Play("bounce-back" + (type == "knife" || type == "kunai" || type == "shuriken-4" || type == "shuriken-6" ? "-small": "")  + (isFacingLeft ? "-right" : ""));
        anim.speed = 1;
      }
    }
  }

  void LateUpdate() {
    if ((type == "king-bone" && throwableCollider.tag == "EnemyWeapon") || ((type == "shuriken-4" || type == "shuriken-6" || type == "hatchet" || type == "axe") && (!hasCollided || hasCollided && mustBounce))) {
      newAngle = initialAngle - (transitionIncrement * bounceRotationMultiplier) * (isFacingLeft ? -1 : 1) * (mustBounce ? -4 : 1);
      transform.rotation = Quaternion.Euler(0, 0, newAngle);

      if (type == "axe" && mustBounce) {
        objectRenderer.sprite = bounceSprite;
      }
    }

    if (type == "lance" || type == "knife"  || type == "kunai" || type == "shuriken-4" || type == "shuriken-6" || type == "hatchet") {
      if (!mustBounce) {
        if (hasCollided) {
          float ellapsedCollideTime = (Time.time * 1000) - collideTime;

          if (ellapsedCollideTime < maxEllapsedCollideTime) {
            if (Helpers.IsNonBouncingThrowable(type)) {
              objectRenderer.color = new Color(1, 1, 1, 1 - (ellapsedCollideTime / maxEllapsedCollideTime));
            }
          } else {
            if (Helpers.IsNonBouncingThrowable(type)) {
              DestroyThrowable();
            }
          }
        }
      } else {
        newAngle = initialAngle - (transitionIncrement * bounceRotationMultiplier) * (isFacingLeft ? -1 : 1) * (mustBounce ? -2 : 1) * (type == "knife" || type == "kunai" || type == "shuriken-4" || type == "shuriken-6" ? 2 : 1);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
        objectRenderer.sprite = bounceSprite;
      }
    }

    if ((type == "king-bone" || type == "shuriken-4" || type == "shuriken-6" || type == "hatchet" || type == "axe") || mustBounce) {
      transitionIncrement++;
    }
  }

  public void DestroyExtra() {
    if (extraSprite != null) {
      Destroy(extraSprite);
    }
  }

  void DestroyBomb() {
    Destroy(gameObject);
  }

  public void DestroyThrowable() {
    Destroy(transform.parent.gameObject);
  }

  public void RemovePhysics() {
    Destroy(GetComponent<Rigidbody2D>());
    Destroy(throwableCollider.GetComponent<CapsuleCollider2D>());
  }

  public void StopAndFade() {
    RemovePhysics();
    anim.speed = 0;
  }

  public void Explode() {
    anim.speed = 0;
    anim.Play("exploding");
    isExploding = true;
    anim.speed = 1;
    transform.parent.position = new Vector2(bounceX - (objectRenderer.bounds.size.x * 2), bounceY - objectRenderer.bounds.size.y);
    transform.position = Vector2.zero;
  }
}
