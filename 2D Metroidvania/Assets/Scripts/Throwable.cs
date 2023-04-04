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
  [System.NonSerialized] public bool isExploding = false;

  [System.NonSerialized] public int transitionIncrement = 0;
  [System.NonSerialized] public int distance = 0;

  [System.NonSerialized] public float bounceRotationMultiplier = 3;
  [System.NonSerialized] public float bounceX;
  [System.NonSerialized] public float bounceY;
  [System.NonSerialized] public float newAngle;
  [System.NonSerialized] public float maxDistance;
  [System.NonSerialized] public float collideTime;
  [System.NonSerialized] public float maxEllapsedCollideTime = 1500f;

  [System.NonSerialized] public string type;

  float initialAngle;
  float rotationAngle;

  void Start() {
    anim = GetComponent<Animator>();
    objectRenderer = GetComponent<SpriteRenderer>();
    throwableCollider = transform.Find("ThrowableCollider").gameObject;

    // TODO: remove extra component once bomb sprites are modified to include spark in them
      extraSprite = transform.Find("Extra").gameObject;
      DestroyExtra();

    if (Helpers.IsValueInArray(Constants.angledThrowables, type)) {
      initialAngle = 45 * (isFacingLeft ? 1 : -1);
    }
    anim.Play(type + (isFacingLeft ? "-left" : ""));
  }

  void Update() {
    // TODO: consider updating this to keep object "alive" for some time/length after off camera
    if (!objectRenderer.isVisible) {
      DestroyThrowable();
    }

    if (mustBounce) {
      anim.speed = 0;
      anim.Play("bounce-back" + (Helpers.IsValueInArray(Constants.smallThrowables, type) ? "-small": "")  + (isFacingLeft ? "-right" : ""));
      anim.speed = 1;
    }
  }

  void LateUpdate() {
    if (Helpers.IsValueInArray(Constants.rotatingThrowables, type) && (!hasCollided || hasCollided && mustBounce)) {
      newAngle = initialAngle - (transitionIncrement * bounceRotationMultiplier) * (isFacingLeft ? -1 : 1) * (mustBounce ? -4 : 1);
      transform.rotation = Quaternion.Euler(0, 0, newAngle);

      if (type == "axe" && mustBounce) {
        objectRenderer.sprite = bounceSprite;
      }
    }

    if (!Helpers.IsValueInArray(Constants.nonGroundableThrowables, type)) {
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
        newAngle = initialAngle - (transitionIncrement * bounceRotationMultiplier) * (isFacingLeft ? -1 : 1) * (mustBounce ? -2 : 1) * (Helpers.IsValueInArray(Constants.smallThrowables, type) ? 2 : 1);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
        objectRenderer.sprite = bounceSprite;
      }
    }

    if (Helpers.IsValueInArray(Constants.rotatingThrowables, type) || mustBounce) {
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
