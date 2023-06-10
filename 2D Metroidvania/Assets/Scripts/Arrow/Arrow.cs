using UnityEngine;

public class Arrow : MonoBehaviour {

  private Animator anim;
  private SpriteRenderer objectRenderer;
  private GameObject arrowAnchor;
  private GameObject arrowMask;

  [System.NonSerialized] public GameObject arrowCollider;

  Hero hero;

  [System.NonSerialized] public GameObject extraSprite;

  [System.NonSerialized] public bool isFacingLeft;
  [System.NonSerialized] public bool hasCollided = false;
  [System.NonSerialized] public bool hasFired = false;
  [System.NonSerialized] public float startX;
  [System.NonSerialized] public float startY;
  [System.NonSerialized] public float collideTime;
  [System.NonSerialized] public float maxEllapsedCollideTime = 1500f;

  [System.NonSerialized] public string type;

  int direction;

  void Start() {
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
    anim = GetComponent<Animator>();
    arrowCollider = transform.Find("ArrowCollider").gameObject;
    arrowAnchor = hero.transform.Find("ArrowAnchor").gameObject;
    arrowMask = transform.Find("Mask").gameObject;
    objectRenderer = GetComponent<SpriteRenderer>();
    extraSprite = transform.Find("Extra").gameObject;
    direction = isFacingLeft ? -1 : 1;

    if (type != "arrow-fire") {
      DestroyExtra();
    }

    objectRenderer.sprite = Objects.arrows[type];

    // TODO: add extra check when making the fire arrow

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
      Destroy(arrowMask);

      if (!anim.enabled) {
        anim.enabled = true;
        anim.Play(type + (isFacingLeft ? "-left" : ""));
      }

      if (hasCollided) {
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

  public void RemovePhysics() {
    Destroy(GetComponent<Rigidbody2D>());
    Destroy(arrowCollider.GetComponent<CapsuleCollider2D>());
  }

  public void StopAndFade() {
    RemovePhysics();
    anim.speed = 0;
  }

  void DestroyExtra() {
    if (extraSprite != null) {
      Destroy(extraSprite);
    }
  }

  public void DestroyArrow() {
    Destroy(transform.parent.gameObject);
  }
}
