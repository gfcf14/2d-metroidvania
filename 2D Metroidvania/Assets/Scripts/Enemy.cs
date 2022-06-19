using UnityEngine;

// Made by Bartha Szabolcs of GameDevJourney

public class Enemy : MonoBehaviour {
  [SerializeField] private SimpleFlash flashEffect;
  [SerializeField] public float speed;

  private Rigidbody2D body;
  private Animator anim;
  private SpriteRenderer enemyRenderer;
  private float enemyHeight = 0f;
  private float enemyWidth = 0f;

  public bool isFacingLeft = false;
  public bool isWalking;

  public bool playerFound = false;

  public bool isAttacking = false;

  public bool needsCoolDown = false;

  public float diagonalForwardCastLength = 1f;
  public float forwardCastLength = 2f;
  public float proximityCastLength = 0.1f;

  public float coolDownTime = 750;
  public float coolDownStart = 0;

  public bool isDead = false;
  public bool isStunned = false;

  public bool attackedFromBehind = false;

  public int standardDamage = 20;

  public bool heroIsDead = false;

  public int hp = 40;

  public bool stunOnAttack = false;

  SpriteRenderer weaponSpriteRenderer;
  Hero hero;

  void Awake() {
    body = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    enemyRenderer = GetComponent<SpriteRenderer>();
    enemyHeight = enemyRenderer.bounds.size.y;
    enemyWidth = enemyRenderer.bounds.size.x;

    isWalking = true;

    flashEffect = GetComponent<SimpleFlash>();
    weaponSpriteRenderer = GameObject.Find("Weapon").GetComponent<SpriteRenderer>();
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
  }

  void Update() {
    heroIsDead = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().isDead != 0;

    // Debug.Log(GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().isDead);

    if (isDead) {
      if (attackedFromBehind) {
        transform.position = new Vector2(transform.position.x + (isFacingLeft ? -1 : 1) * 0.05f, transform.position.y + 0.02f);
      } else {
        transform.position = new Vector2(transform.position.x + (isFacingLeft ? 1 : -1) * 0.05f, transform.position.y + 0.02f);
      }
    }

    if (!needsCoolDown) {
      if (isWalking && !isAttacking) {
        int direction = isFacingLeft ? -1 : 1;

        body.velocity = new Vector2(direction * speed, body.velocity.y);

        Vector2 beginDiagonalForwardCast = new Vector2(transform.position.x + ((enemyWidth / 2) * direction), transform.position.y + enemyHeight / 4);
        Vector2 diagonalForwardCastDirection = transform.TransformDirection(new Vector2(1 * (direction), -1));

        RaycastHit2D diagonalForwardCast = Physics2D.Raycast(beginDiagonalForwardCast, diagonalForwardCastDirection, diagonalForwardCastLength);
        Debug.DrawRay(beginDiagonalForwardCast, diagonalForwardCastDirection.normalized * diagonalForwardCastLength, Color.green);

        // There's floor forward
        // if (!diagonalForwardCast && diagonalForwardCast.collider.tag == "Ground") {
        if (!diagonalForwardCast) {
          isFacingLeft = !isFacingLeft;
        }

        if (!heroIsDead) {
          Vector2 beginForwardCast = new Vector2(transform.position.x + ((enemyWidth / 2) * direction), transform.position.y + enemyHeight / 2);
          Vector2 forwardCastDirection = transform.TransformDirection(new Vector2(1 * (direction), 0));

          if (!playerFound) {
            RaycastHit2D forwardCast = Physics2D.Raycast(beginForwardCast, forwardCastDirection, forwardCastLength);
            Debug.DrawRay(beginForwardCast, forwardCastDirection.normalized * forwardCastLength, Color.red);

            // Player is nearby
            if (forwardCast && forwardCast.collider.tag == "Hero") {
              playerFound = true;
            }
          } else {
            Vector2 beginProximityCast = new Vector2(transform.position.x + ((enemyWidth / 5) * direction), transform.position.y + enemyHeight / 2);

            RaycastHit2D proximityCast = Physics2D.Raycast(beginProximityCast, forwardCastDirection, proximityCastLength);
            Debug.DrawRay(beginProximityCast, forwardCastDirection.normalized * proximityCastLength, Color.magenta);

            if (proximityCast && proximityCast.collider.tag == "Hero") {
              isAttacking = true;
              body.velocity = new Vector2(0, body.velocity.y);
            }
          }
        }
      }
    } else {
      float currentTime = Time.time * 1000;

      if ((Time.time * 1000) > (coolDownStart + coolDownTime)) {
          coolDownStart = 0;
          needsCoolDown = false;
          playerFound = false;
        }
    }

    if (isFacingLeft) {
      transform.localScale = new Vector3(-1, 1, 1);
    } else {
      transform.localScale = Vector3.one;
    }

    anim.SetBool("isWalking", isWalking);
    anim.SetBool("isAttacking", isAttacking);
    anim.SetBool("needsCooldown", needsCoolDown);
    anim.SetBool("isDead", isDead);
    anim.SetBool("isStunned", isStunned);
    anim.SetBool("isStunnedOnAttack", stunOnAttack);
  }

  private void OnCollisionEnter2D(Collision2D col) {
    if (col.gameObject.tag == "Hero") {
      // isAttacking = false;
      needsCoolDown = true;
      coolDownStart = Time.time * 1000;
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    string colliderTag = col.gameObject.tag;

    if (colliderTag == "Weapon" && !hero.isParrying) {
      float currentX = transform.position.x;
      float enemyX = col.transform.position.x;

      attackedFromBehind = (currentX < enemyX && isFacingLeft) || (currentX > enemyX && !isFacingLeft);

      if (hero.isKicking || hero.isDropKicking) {
        hp -= 10;
      } else {
        if (hero.currentWeapon != "fists") {
          string weaponWielded = weaponSpriteRenderer.sprite.name.Split('_')[0];
          hp -= Utilities.GetDamage(weaponWielded);
        } else {
          hp -= 5;
        }
      }

      if (hp > 0) {
        if (flashEffect != null) {
          flashEffect.Flash();
        }

        Stun();
      } else {
        isDead = true;
        isStunned = false;
        isWalking = false;
        body.velocity = new Vector2(0, body.velocity.y);
      }
    } else if (colliderTag == "Shield") {
      if (isAttacking) {
        // consider reusing for higher level shields
        // Stun();
      }
    }
  }

  public void Stun() {
    isStunned = true;
    isWalking = false;

    body.velocity = Vector2.zero;
  }

  void Recover() {
    isStunned = false;
    stunOnAttack = false;
    isWalking = true;
  }

  public void StunOnAttack() {
    if (stunOnAttack) {
      Stun();
    }
  }

  void FinishAttack() {
    isAttacking = false;
  }

  void Destroy() {
    Destroy(gameObject);
  }
}
