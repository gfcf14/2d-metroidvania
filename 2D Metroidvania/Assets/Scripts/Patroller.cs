using UnityEngine;

// Made by Bartha Szabolcs of GameDevJourney

public class Patroller : MonoBehaviour {
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
  public bool isDeadByPoison = false;

  public bool attackedFromBehind = false;

  public int standardDamage = 20;

  public bool heroIsDead = false;

  [SerializeField] public int hp = 40;

  public bool stunOnAttack = false;

  public Vector2 deadPosition;
  public int deadAnimationIncrement = 0;

  public bool isPoisoned = false;
  public float poisonTime = 0;
  public float poisonEffectTime = 0;
  public int poisonAttackCounter = 1;

  // TODO: consider moving these to Utilities
  [System.NonSerialized] public int maxPoisonAttacks = 3;
  [System.NonSerialized] public float poisonAttackInterval = 600;
  [System.NonSerialized] public float poisonEffectDuration = 50;
  [System.NonSerialized] public int poisonDamage = 10;

  SpriteRenderer weaponSpriteRenderer;
  Hero hero;

  string[] elementResistances;
  Color enemyColor;

  public bool isBurning = false;
  public bool isDeadByBurning = false;

  public float burnTime = 0;

  // TODO: consider moving these to Utilities
  [System.NonSerialized] public float burningDuration = 3000;

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

    elementResistances = new string[] {"fire"};
    enemyColor = Utilities.GetColorFromResistances(elementResistances);
    flashEffect.repaintColor = enemyColor;
    enemyRenderer.color = enemyColor;
  }

  void Update() {
    if (!isPoisoned && !isStunned) {
      enemyRenderer.color = enemyColor;
    }

    heroIsDead = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().isDead != 0;

    if (isBurning) {
      enemyColor = Utilities.specialColors["ash"];
      float currentTime = Time.time * 1000;

      if (currentTime > burnTime + burningDuration) {
        isBurning = false;
        isDeadByBurning = true;
      }
    }

    if (isPoisoned) {
      float currentTime = Time.time * 1000;
      float nextPoisonAttackTime = poisonTime + (poisonAttackInterval * poisonAttackCounter);

      if (currentTime > poisonEffectTime + poisonEffectDuration) {
        if (!isStunned) {
          enemyRenderer.color = enemyColor;
        }

        if (poisonAttackCounter == maxPoisonAttacks + 1) {
          isPoisoned = false;
        }
      }

      if (currentTime > nextPoisonAttackTime)  {
        hp -= 10;
        poisonEffectTime = Time.time * 1000;
        enemyRenderer.color = Utilities.specialColors["poisoned"];
        poisonAttackCounter++;

        if (hp <= 0) {
          isDeadByPoison = true;
          isWalking = false;
          body.velocity = Vector2.zero;
        }
      }
    }

    if (isDead) {
      if (attackedFromBehind) {
        // transform.position = new Vector2(transform.position.x + (isFacingLeft ? -1 : 1) * 0.05f, transform.position.y + 0.02f);
        transform.position = new Vector3(deadPosition.x + deadAnimationIncrement * 0.025f * (isFacingLeft ? -1 : 1), deadPosition.y + 0.01f * deadAnimationIncrement);
      } else {
        // transform.position = new Vector2(transform.position.x + (isFacingLeft ? 1 : -1) * 0.05f, transform.position.y + 0.02f);
        transform.position = new Vector3(deadPosition.x + deadAnimationIncrement * 0.025f * (isFacingLeft ? 1 : -1), deadPosition.y + 0.01f * deadAnimationIncrement);
      }

      deadAnimationIncrement++;
    }

    if (!needsCoolDown) {
      if (isWalking && !isAttacking) {
        int direction = isFacingLeft ? -1 : 1;

        if (!isDead && !isDeadByBurning && !isDeadByPoison && !isBurning && !isStunned) {
          body.velocity = new Vector2(direction * speed, body.velocity.y);
        } else {
          body.velocity = Vector2.zero;
        }

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
              body.velocity = Vector2.zero;
            }
          }
        }
      }
    } else {
      float currentTime = Time.time * 1000;

      if (currentTime > (coolDownStart + coolDownTime)) {
          coolDownStart = 0;
          needsCoolDown = false;
          playerFound = false;
        }
    }

    if (!isBurning) {
      if (isFacingLeft) {
        transform.localScale = new Vector3(-1, 1, 1);
      } else {
        transform.localScale = Vector3.one;
      }
    }

    anim.SetBool("isWalking", isWalking);
    anim.SetBool("isAttacking", isAttacking);
    anim.SetBool("needsCooldown", needsCoolDown);
    anim.SetBool("isDead", isDead);
    anim.SetBool("isDeadByPoison", isDeadByPoison);
    anim.SetBool("isStunned", isStunned);
    anim.SetBool("isStunnedOnAttack", stunOnAttack);
    anim.SetBool("isBurning", isBurning);
    anim.SetBool("isDeadByBurning", isDeadByBurning);
  }

  private void OnCollisionEnter2D(Collision2D col) {
    GameObject colliderObject = col.gameObject;

    if (colliderObject.tag == "Hero") {
      // isAttacking = false;
      needsCoolDown = true;
      coolDownStart = Time.time * 1000;
    } else if (colliderObject.tag == "Enemy") {
      Physics2D.IgnoreCollision(colliderObject.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    string colliderTag = col.gameObject.tag;

    if (colliderTag == "Weapon" && !hero.isParrying) {
      float currentX = transform.position.x;
      float enemyX = col.transform.position.x;
      bool mustTakeDamage = true;
      bool willBurn = false;

      attackedFromBehind = (currentX < enemyX && isFacingLeft) || (currentX > enemyX && !isFacingLeft);

      if (hero.isKicking || hero.isDropKicking) {
        hp -= 10;
      } else {
        string currentWeapon = hero.currentWeapon;

        if (currentWeapon == "fists") {
          hp -= 5;
        } else if (currentWeapon == "single" || currentWeapon == "heavy") {
          string weaponWielded = weaponSpriteRenderer.sprite.name.Split('_')[0];
          hp -= Utilities.GetDamage(weaponWielded);
        } else if (currentWeapon == "throwables") {
          GameObject parentObject = col.transform.parent.gameObject;
          Throwable parentThrowable = parentObject.GetComponent<Throwable>();
          string weaponWielded = parentThrowable.type;

          mustTakeDamage = (Utilities.IsNonBouncingThrowable(weaponWielded) && !parentThrowable.hasCollided) || (weaponWielded == "bomb" && parentThrowable.isExploding);

          if (mustTakeDamage) {
            hp -= Utilities.GetDamage(weaponWielded);

            Transform parentTransform = parentObject.GetComponent<Transform>();

            if(Utilities.IsNonBouncingThrowable(weaponWielded)) {
              parentThrowable.bounceX = parentTransform.position.x;
              parentThrowable.bounceY = parentTransform.position.y;
              parentThrowable.mustBounce = true;
              parentThrowable.transitionIncrement = 0;
            }

            parentThrowable.collideTime = Time.time * 1000;
            parentThrowable.hasCollided = true;
            parentThrowable.maxEllapsedCollideTime = 1000f;
          }
        } else if (currentWeapon == "projectile-pull") {
          GameObject parentObject = col.transform.parent.gameObject;
          Arrow parentArrow = parentObject.GetComponent<Arrow>();
          string arrowUsed = parentArrow.type;

          mustTakeDamage = !parentArrow.hasCollided;
          willBurn = parentArrow.type == "arrow-fire" && !Utilities.IsFireResistant(elementResistances) && hp <= Utilities.arrowExplosionDamage;

          if (mustTakeDamage) {
            hp -= Utilities.GetDamage(arrowUsed);

            if (parentArrow.type == "arrow-poison" && !Utilities.IsPoisonResistant(elementResistances)) {
              isPoisoned = true;
              poisonTime = Time.time * 1000;
            }

            parentArrow.DestroyArrow();
          }
        }
      }

      if (mustTakeDamage) {
        if (hp > 0) {
          if (flashEffect != null) {
            flashEffect.Flash();
          }

          if (!willBurn) {
            Stun();
          }
        } else {
          isDead = true;
          isPoisoned = false;
          isStunned = false;
          isWalking = false;
          body.velocity = Vector2.zero;
          deadPosition = new Vector2(transform.position.x, transform.position.y);
        }
      }
    } else if (colliderTag == "Shield") {
      if (isAttacking) {
        // TODO: consider reusing for higher level shields
        // Stun();
      }
    } else if (colliderTag == "Explosion") {
      string colName = col.gameObject.name.Replace("(Clone)", "");

      if (colName == "ArrowExplosion" || colName == "ArrowBurn") {
        bool willBurn = !Utilities.IsFireResistant(elementResistances) && hp <= Utilities.arrowExplosionDamage;

        if (willBurn) {
          float currentTime = Time.time * 1000;

          if (!isBurning) {
            GameObject arrowBurn = Instantiate(Utilities.prefabs["arrow-burn"], new Vector2(transform.position.x, transform.position.y + (enemyHeight / 2)), Quaternion.identity);
            arrowBurn.GetComponent<ArrowBurn>().startTime = currentTime;

            burnTime = currentTime;
            isBurning = true;
            isWalking = false;
            body.velocity = Vector2.zero;
          }
        } else {
          if (!Utilities.IsFireResistant(elementResistances)) {
            hp -= Utilities.arrowExplosionDamage;

            if (flashEffect != null) {
              flashEffect.Flash();
              Stun();
            }
          }
        }
      }
    }
  }

  public void Flip() {
    transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
  }

  public void Stun() {
    isStunned = true;
    enemyRenderer.color = Color.white;
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
    Instantiate(Utilities.prefabs["enemy-explosion"], new Vector2(transform.position.x, transform.position.y + (enemyHeight / 2)), Quaternion.identity);
    Destroy(gameObject);
  }

  // public void OnGUI() {
  //   string guiLabel = "HP: " + hp + "\n";
  //   GUI.Label(new Rect(600, 0, 200, 400), guiLabel);
  // }
}
