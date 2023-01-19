using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
  // Serialized
    [SerializeField] public string key;
    [SerializeField] private SimpleFlash flashEffect;

  // Components
    [System.NonSerialized] public Animator anim;
    [System.NonSerialized] public Rigidbody2D body;
    [System.NonSerialized] public SpriteRenderer enemyRenderer;

  // Properties
    public bool isFacingLeft = false;
    public bool needsCoolDown = false;


    public float burnTime = 0;
    public float coolDownStart = 0;
    public float coolDownTime = 750;
    public float enemyHeight = 0f;
    public float enemyWidth = 0f;
    public float poisonEffectTime = 0;
    public float poisonTime = 0;


    public int deadAnimationIncrement = 0;
    public int poisonAttackCounter = 1;


    string[] elementResistances;


    public Color enemyColor;


    public Vector2 deadPosition;


    [System.NonSerialized] public float burningDuration = 3000;
    [System.NonSerialized] public float criticalRate;
    [System.NonSerialized] public float poisonAttackInterval = 600;
    [System.NonSerialized] public float poisonEffectDuration = 50;
    [System.NonSerialized] public float speed;


    [System.NonSerialized] public int atk;
    [System.NonSerialized] public int currentHP;
    [System.NonSerialized] public int def;
    [System.NonSerialized] public int exp;
    [System.NonSerialized] public int maxHP;
    [System.NonSerialized] public int maxPoisonAttacks = 3;


    [System.NonSerialized] public string EnemyName;
    [System.NonSerialized] public string type;

  // Game Properties
    public bool attackedFromBehind = false;
    public bool isAttacking = false;
    public bool isBurning = false;
    public bool isDead = false;
    public bool isDeadByBurning = false;
    public bool isDeadByPoison = false;
    public bool isHitting = false; // provisional variable to only detect the moment the enemy attacks
    public bool isPoisoned = false;
    public bool isStunned = false;
    public bool isWalking;
    public bool stunOnAttack = false;

  // Player Related Properties
    public bool playerFound = false;
    public Hero hero;
    SpriteRenderer weaponSpriteRenderer;

  void Start() {
    body = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    enemyRenderer = GetComponent<SpriteRenderer>();
    enemyHeight = enemyRenderer.bounds.size.y;
    enemyWidth = enemyRenderer.bounds.size.x;

    isWalking = true;

    flashEffect = GetComponent<SimpleFlash>();
    weaponSpriteRenderer = GameObject.Find("Weapon").GetComponent<SpriteRenderer>();
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();

    elementResistances = new string[] {};
    enemyColor = Helpers.GetColorFromResistances(elementResistances);
    flashEffect.repaintColor = enemyColor;
    enemyRenderer.color = enemyColor;

    EnemyStats enemyStats = Objects.enemyStats[key];
    EnemyName = enemyStats.name;
    type = enemyStats.type;
    atk = enemyStats.atk;
    def = enemyStats.def;
    currentHP = enemyStats.hp;
    maxHP = enemyStats.hp;
    criticalRate = enemyStats.crit;
    exp = enemyStats.exp;
    speed = enemyStats.speed;

    if (type == "patroller") {
      gameObject.AddComponent<Patroller>();
    }
  }

  void Update() {}

  private void OnCollisionEnter2D(Collision2D col) {
    GameObject colliderObject = col.gameObject;

    if (colliderObject.tag == "Hero") {
      // isAttacking = false;
      coolDownStart = Time.time * 1000;
      if (!needsCoolDown) {
        hero.ReceiveAttack(gameObject, col.collider.ClosestPoint(transform.position));
        needsCoolDown = true;
      }
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
        bool isCritical = Helpers.IsCritical(hero.criticalPercentage + hero.equippedCRIT);
        int damage = def - ((Constants.kickDamage + hero.strength + (int)hero.equippedSTR) * (isCritical ? 2 : 1));
        TakeDamage(damage < 0 ? Math.Abs(damage) : Constants.minimumDamageDealt, col.ClosestPoint(transform.position), isCritical);
      } else {
        string currentWeapon = hero.armUsed == 1 ? Hero.arm1Equipment : Hero.arm2Equipment;

        if (currentWeapon == "") {
          bool isCritical = Helpers.IsCritical(hero.criticalPercentage + hero.equippedCRIT);
          int damage = def - ((Constants.punchDamage + hero.strength + (int)hero.equippedSTR) * (isCritical ? 2 : 1));
          TakeDamage(damage < 0 ? Math.Abs(damage) : Constants.minimumDamageDealt, col.ClosestPoint(transform.position), isCritical);
        } else {
          string weaponType = Objects.pauseItems[currentWeapon].type;

          if (weaponType == "single" || weaponType == "double") {
            string weaponWielded = weaponSpriteRenderer.sprite.name.Split('_')[0];
            bool isCritical = Helpers.IsCritical(hero.criticalPercentage + hero.equippedCRIT);
            int damage = def - ((Helpers.GetDamage(weaponWielded) + hero.strength + (int)hero.equippedSTR) * (isCritical ? 2 : 1));
            TakeDamage(damage < 0 ? Math.Abs(damage) : Constants.minimumDamageDealt, col.ClosestPoint(transform.position), isCritical);
          } else if (weaponType == "throwable" || weaponType == "throwable-double") {
            GameObject parentObject = col.transform.parent.gameObject;
            Throwable parentThrowable = parentObject.GetComponent<Throwable>();
            string weaponWielded = parentThrowable.type;

            mustTakeDamage = (Helpers.IsNonBouncingThrowable(weaponWielded) && !parentThrowable.hasCollided) || (weaponWielded == "bomb" && parentThrowable.isExploding);

            if (mustTakeDamage) {
              bool isCritical = Helpers.IsCritical(hero.criticalPercentage + hero.equippedCRIT);
              int damage = def - ((Helpers.GetDamage(weaponWielded) + hero.strength + (int)hero.equippedSTR) * (isCritical ? 2 : 1));
              TakeDamage(damage < 0 ? Math.Abs(damage) : Constants.minimumDamageDealt, col.ClosestPoint(transform.position), isCritical);

              Transform parentTransform = parentObject.GetComponent<Transform>();

              if(Helpers.IsNonBouncingThrowable(weaponWielded)) {
                parentThrowable.bounceX = parentTransform.position.x;
                parentThrowable.bounceY = parentTransform.position.y;
                parentThrowable.mustBounce = true;
                parentThrowable.transitionIncrement = 0;
              }

              parentThrowable.collideTime = Time.time * 1000;
              parentThrowable.hasCollided = true;
              parentThrowable.maxEllapsedCollideTime = 1000f;
            }
          } else if (Helpers.IsValueInArray(Constants.projectileHoldingWeaponTypes, weaponType)) {
            GameObject parentObject = col.transform.parent.gameObject;
            Arrow parentArrow = parentObject.GetComponent<Arrow>();
            string arrowUsed = parentArrow.type;

            mustTakeDamage = !parentArrow.hasCollided;
            willBurn = parentArrow.type == "arrow-fire" && !Helpers.IsFireResistant(elementResistances) && currentHP <= Constants.arrowExplosionDamage;

            if (mustTakeDamage) {
              bool isCritical = Helpers.IsCritical(hero.criticalPercentage + hero.equippedCRIT);
              int damage = def - ((Helpers.GetDamage(arrowUsed) + hero.strength + (int)hero.equippedSTR) * (isCritical ? 2 : 1));
              TakeDamage(damage < 0 ? Math.Abs(damage) : Constants.minimumDamageDealt, col.ClosestPoint(transform.position), isCritical);

              if (parentArrow.type == "arrow-poison" && !Helpers.IsPoisonResistant(elementResistances)) {
                isPoisoned = true;
                poisonTime = Time.time * 1000;
              }

              parentArrow.DestroyArrow();
            }
          }
        }
      }

      if (mustTakeDamage) {
        if (currentHP > 0) {
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

          if (!isDeadByPoison) { // avoids getting double exp if attacking while dying from poison
            hero.exp += exp;
            hero.CheckLevel();
          }
        }
      }

      hero.infoCanvas.GetComponent<InfoCanvas>().Display(EnemyName, new EnemyHealth(currentHP, maxHP));
    } else if (colliderTag == "Shield") {
      if (isAttacking) {
        // TODO: consider reusing for higher level shields
        // Stun();
      }

      hero.infoCanvas.GetComponent<InfoCanvas>().Display(EnemyName, new EnemyHealth(currentHP, maxHP));
    } else if (colliderTag == "Explosion") {
      string colName = col.gameObject.name.Replace("(Clone)", "");

      if (colName == "ArrowExplosion" || colName == "ArrowBurn") {
        bool willBurn = !Helpers.IsFireResistant(elementResistances) && currentHP <= Constants.arrowExplosionDamage;

        if (willBurn) {
          float currentTime = Time.time * 1000;

          if (!isBurning) {
            GameObject arrowBurn = Instantiate(Objects.prefabs["arrow-burn"], new Vector2(transform.position.x, transform.position.y + (enemyHeight / 2)), Quaternion.identity);
            arrowBurn.GetComponent<ArrowBurn>().startTime = currentTime;

            burnTime = currentTime;
            isBurning = true;
            isWalking = false;
            body.velocity = Vector2.zero;
          }
        } else {
          if (!Helpers.IsFireResistant(elementResistances)) {
            int damage = def - Constants.arrowExplosionDamage;
            TakeDamage(damage < 0 ? Math.Abs(damage) : Constants.minimumDamageDealt, col.ClosestPoint(transform.position));

            if (flashEffect != null) {
              flashEffect.Flash();
              Stun();
            }
          }
        }
      }
    }
  }

  public void TakeDamage(int damage, Vector2? damagePosition = null, bool? isCritical = false) {
    currentHP -= damage;

    if (Settings.showDamage) {
      GameObject damageObject = Instantiate(Objects.prefabs["damage-container"], damagePosition ?? new Vector2(transform.position.x, transform.position.y + (enemyHeight / 2)), Quaternion.identity);
      damageObject.transform.SetParent(null);
      damageObject.GetComponent<DamageContainer>().damage = damage;
      damageObject.GetComponent<DamageContainer>().isCritical = isCritical ?? false;
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

  public void StartHitting() {
    isHitting = true;
  }

  void FinishAttack() {
    isAttacking = false;
    isHitting = false;
  }

  void Destroy() {
    // instantiates the dropped item
    Vector2 itemOrigin = new Vector2(transform.position.x, transform.position.y + (enemyHeight / 2));
    GameObject droppedItem = Instantiate(Objects.prefabs["droppable"], itemOrigin, Quaternion.identity);
    droppedItem.GetComponent<Droppable>().key = "moonlight-pendant"; // TODO: come up with a random system to add the item instead of hardcoding
    droppedItem.GetComponent<Droppable>().isDropped = true;

    // instantiates the explosion of the enemy
    Instantiate(Objects.prefabs["enemy-explosion"], new Vector2(transform.position.x, transform.position.y + (enemyHeight / 2)), Quaternion.identity);
    Destroy(gameObject);
  }

  public void OnGUI() {
    if (hero.showDebug) {
      string guiLabel = "HP: " + currentHP + "\n";
      GUI.Label(new Rect(600, 0, 200, 400), guiLabel);
    }
  }
}
