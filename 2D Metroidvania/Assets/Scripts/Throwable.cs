using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {
    private SpriteRenderer objectRenderer;
    private CapsuleCollider2D hitBounds;

    Hero hero;

    [System.NonSerialized] public GameObject extraSprite;
    [System.NonSerialized] public Animator anim;

    [System.NonSerialized] public bool isFacingLeft;
    [System.NonSerialized] public bool hasCollided = false;
    [System.NonSerialized] public bool mustBounce = false;
    [System.NonSerialized] public bool mustFall = false;
    [System.NonSerialized] public bool isExploding = false;

    [System.NonSerialized] public int transitionIncrement = 0;
    [System.NonSerialized] public int gravityResistance = 0;

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

    float parabolaValue(float x) {
      return (-0.5f * Mathf.Pow(x, 2)) + (direction * maxDistance * x);
    }

    float bounceParabolaValue(float x) {
      return (-0.25f * Mathf.Pow(x, 2)) - (direction * x);
    }

    void Start() {
      anim = GetComponent<Animator>();
      objectRenderer = GetComponent<SpriteRenderer>();
      hitBounds = transform.Find("ThrowableCollider").gameObject.GetComponent<CapsuleCollider2D>();
      extraSprite = transform.Find("Extra").gameObject;
      hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();

      if (type == "lance") {
        DestroyExtra();
        Destroy(anim);
        objectRenderer.sprite = Utilities.sprites[type];
        transform.rotation = Quaternion.Euler(0, 0, initialAngle);
      } else if (type == "bomb") {
        anim.SetBool("isBomb", true);
      } else if (type == "knife") {
        DestroyExtra();
        Destroy(anim);
        objectRenderer.sprite = Utilities.sprites[type];
        gravityResistance = 2;

        hitBounds.offset = new Vector2(0.15f, -0.15f);
        hitBounds.size = new Vector2(0.2f, 1.18f);
      }

      direction = isFacingLeft ? -1 : 1;
      initialAngle = direction * 30f;

      rotationAngle = (maxDistance / initialAngle);

      if (isFacingLeft) {
        transform.localScale = new Vector3(-1, 1, 1);
      }
    }

    void Update() {
      if (!hasCollided) {
        if (!mustFall) {
          float newX = direction * distanceMultiplier * transitionIncrement;

          if (type == "lance" || type == "bomb") {
            newAngle = initialAngle - (rotationAngle * transitionIncrement);

            transform.position = new Vector2(startX + newX, startY + parabolaValue(newX));

            if (type != "bomb") {
              transform.rotation = Quaternion.Euler(0, 0, newAngle);
            }
          } else if (type == "knife") {
            float heightDrop = (transitionIncrement / gravityResistance);

            newAngle = -0.5f * direction * heightDrop;

            transform.position = new Vector2(startX + (newX * 5), startY - (heightDrop * distanceMultiplier));
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
          }

          transitionIncrement++;
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
          if (type == "lance" || type == "knife") {
            objectRenderer.color = new Color(255, 255, 255, 1 - (ellapsedCollideTime / maxEllapsedCollideTime));
          }
        } else {
          if (type == "lance" || type == "knife") {
            Destroy(gameObject);
          } else if (type == "bomb") {
            isExploding = true;
            DestroyExtra();
            anim.speed = 1;
            anim.SetBool("isExploding", isExploding);
          }
        }
      }
    }

    void DestroyExtra() {
      if (extraSprite != null) {
        Destroy(extraSprite);
      }
    }

    void DestroyBomb() {
      Destroy(gameObject);
    }
}
