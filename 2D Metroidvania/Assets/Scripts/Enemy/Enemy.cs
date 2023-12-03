using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour {
  // Serialized
    [SerializeField] public string key;
    [SerializeField] public string summonKey;
    [SerializeField] public int level;
    [SerializeField] public int currentHP;
    [SerializeField] public int maxHP;
    [SerializeField] public int atk;
    [SerializeField] public int def;
    [SerializeField] public float criticalRate;
    [SerializeField] public bool isOnCamera = false;

  // Components
    [System.NonSerialized] public Animator anim;
    [System.NonSerialized] private SimpleFlash flashEffect;
    [System.NonSerialized] public Rigidbody2D body;
    [System.NonSerialized] public SpriteRenderer enemyRenderer;
    private AudioSource audioSource;
    private InGame inGame;

  // Properties
    [System.NonSerialized] public bool isFacingLeft = false;
    [System.NonSerialized] public bool needsCoolDown = false;
    [System.NonSerialized] public bool diesFlying = false;
    [System.NonSerialized] public bool isFlyingEnemy = false;

    [System.NonSerialized] public float arrowBurnPosition = 0;
    [System.NonSerialized] public float attackedStart = 0;
    [System.NonSerialized] public float burnTime = 0;
    [System.NonSerialized] public float consecutiveAttackTime = 5000;
    [System.NonSerialized] public float coolDownStart = 0;
    [System.NonSerialized] public float coolDownTime = 750;
    [System.NonSerialized] public float enemyHeight = 0f;
    [System.NonSerialized] public float enemyWidth = 0f;
    [System.NonSerialized] public float edgeCastLength = 0;
    [System.NonSerialized] public float poisonEffectTime = 0;
    [System.NonSerialized] public float poisonTime = 0;


    [System.NonSerialized] public int deadAnimationIncrement = 0;
    [System.NonSerialized] public int poisonAttackCounter = 1;


    [System.NonSerialized] string[] elementResistances;


    [System.NonSerialized] public Color enemyColor;


    [System.NonSerialized] public Vector2 deadPosition;


    [System.NonSerialized] public float burningDuration = 3000;
    [System.NonSerialized] public float distanceToPlayer;
    [System.NonSerialized] public float poisonAttackInterval = 600;
    [System.NonSerialized] public float poisonEffectDuration = 50;
    [System.NonSerialized] public float reach;
    [System.NonSerialized] public float speed;

    [System.NonSerialized] public int attacksReceived = 0;
    [System.NonSerialized] public int attackRetaliationCounter = 3;
    [System.NonSerialized] public int exp;
    [System.NonSerialized] public int maxPoisonAttacks = 3;
    [System.NonSerialized] public int maxThrows = 3;
    [System.NonSerialized] public int maxThrowCounter = 0;


    [System.NonSerialized] public string enemyName;
    [System.NonSerialized] public string type;
    [System.NonSerialized] public string baseMaterial;
    [System.NonSerialized] public string normalAttackType;

  // Game Properties
    [System.NonSerialized] public bool attackedFromBehind = false;
    [System.NonSerialized] public bool gaveExp = false;
    [System.NonSerialized] public bool isAttacking = false;
    [System.NonSerialized] public bool isAttackingMelee = false;
    [System.NonSerialized] public bool isBurning = false;
    [System.NonSerialized] public bool isDead = false;
    [System.NonSerialized] public bool isDeadByBurning = false;
    [System.NonSerialized] public bool isDeadByPoison = false;
    [System.NonSerialized] public bool isDefending = false;
    [System.NonSerialized] public bool isDying = false; // should encompass all death or death-leading states (isBurning, isDeadByBurning, isDeadByPoison)
    [System.NonSerialized] public bool isHitting = false; // provisional variable to only detect the moment the enemy attacks
    [System.NonSerialized] public bool isPoisoned = false;
    [System.NonSerialized] public bool isStunned = false;
    [System.NonSerialized] public bool isSummoning = false;
    [System.NonSerialized] public bool isThrowingWeapon = false;
    [System.NonSerialized] public bool isWalking;
    [System.NonSerialized] public bool stunOnAttack = false;

    public int direction = 1;

    bool mustTakeDamage = true;

  // Player Related Properties
    [System.NonSerialized] public bool playerFound = false;
    [System.NonSerialized] public Hero hero;
    [System.NonSerialized] SpriteRenderer weaponSpriteRenderer;

  void Start() {
    body = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    enemyRenderer = GetComponent<SpriteRenderer>();
    inGame = GameObject.Find("UnityHelpers").gameObject.GetComponent<InGame>();
    audioSource = GetComponent<AudioSource>();
    enemyHeight = enemyRenderer.bounds.size.y;
    enemyWidth = enemyRenderer.bounds.size.x;

    isWalking = true;

    flashEffect = GetComponent<SimpleFlash>();
    weaponSpriteRenderer = GameObject.Find("Weapon").GetComponent<SpriteRenderer>();
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
    isFacingLeft = !hero.isFacingLeft;
    if (isFacingLeft) {
      Flip();
    }

    elementResistances = new string[] {};
    enemyColor = Helpers.GetColorFromResistances(elementResistances);
    flashEffect.repaintColor = enemyColor;
    enemyRenderer.color = enemyColor;

    EnemyStats enemyStats = Objects.enemyStats[key];
    enemyName = enemyStats.name + " Lvl " + level;
    type = enemyStats.type;
    baseMaterial = enemyStats.baseMaterial;
    normalAttackType = enemyStats.normalAttackType;

    atk = Helpers.GetStatsOnEnemyLevel(enemyStats.atk, level);
    def = Helpers.GetStatsOnEnemyLevel(enemyStats.def, level);
    currentHP = Helpers.GetStatsOnEnemyLevel(enemyStats.hp, level);
    maxHP = Helpers.GetStatsOnEnemyLevel(enemyStats.hp, level);
    criticalRate = Helpers.GetStatsOnEnemyLevel(enemyStats.crit, level);

    exp = enemyStats.exp;
    speed = enemyStats.speed;
    reach = enemyStats.reach;

    edgeCastLength = enemyStats.edgeCastLength;
    arrowBurnPosition = enemyStats.arrowBurnPosition;

    diesFlying = Helpers.IsValueInArray(Constants.flyingDeathEnemies, key);
    isFlyingEnemy = Helpers.IsValueInArray(Constants.flyingEnemies, key);

    if (type == "patroller") {
      gameObject.AddComponent<Patroller>();
    } else if (type == "champion") {
      gameObject.AddComponent<Champion>();
    }

    if (type != "champion") {
      // perform a check for an enemy which has the same type and key, thus it has a prepared animator with states
      bool animatorAlreadyExists = false;
      // gets a list of all Enemies usable
      GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy").Where(obj => obj.name.Contains("Enemy") && obj.name != "EnemyCollider").ToArray();

      foreach (GameObject objectWithAnimator in enemyObjects) {
        Enemy objectEnemyScript = objectWithAnimator.GetComponent<Enemy>();

        // if it is not the current gameObject and the type and key are the same and the animator and runtimeAnimatorController are not null, make a copy of that object's animator controller
        if (objectEnemyScript != gameObject && objectEnemyScript.type == type && objectEnemyScript.key == key && objectEnemyScript.GetComponent<Animator>() != null && objectEnemyScript.GetComponent<Animator>().runtimeAnimatorController != null) {
          anim.runtimeAnimatorController = objectWithAnimator.GetComponent<Animator>().runtimeAnimatorController;
          animatorAlreadyExists = true;
          break;
        }
      }

      // if no such object (same type and key) was found, instantiate a new copy and assign clips based on key to states
      if (!animatorAlreadyExists) {
        AnimatorOverrideController aoc = new AnimatorOverrideController(Instantiate(Objects.animationControllers[type]));
        AnimatorOverrideController resourceAoc = new AnimatorOverrideController(GameObject.Find("UnityHelpers").gameObject.GetComponent<Animator>().runtimeAnimatorController);

        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        foreach (AnimationClip a in aoc.animationClips) {
          string stateName = a.name.Split('_')[1];
          anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(a, resourceAoc.animationClips.FirstOrDefault(
            resourceClip => resourceClip.name == key + "_" + stateName ||
            (key == "nymph" && stateName == "death-by-poison" && resourceClip.name == "nymph_death") // nymph edge case since she uses nymph_death for regular death, and poison/burning death
          )));
        }
        aoc.ApplyOverrides(anims);

        anim.runtimeAnimatorController = aoc;
      }
    } else {
      anim.runtimeAnimatorController = Objects.animationControllers[key];
    }

    // move enemy upward a bit from ground to account for flying behavior
    if (isFlyingEnemy) {
      transform.position = new Vector2(transform.position.x, transform.position.y + Objects.enemyDimensions[key].y);
      body.gravityScale = 0;
      transform.Find("Grounder").gameObject.SetActive(false);
    }
  }

  void awardExp() {
    gaveExp = true;
    int expToAward = Helpers.GetEnemyEXP(heroLevel: hero.playerLevel, enemyLevel: level, baseExp: exp);
    hero.exp += expToAward;
    hero.CheckLevel();
  }

  public void PlaySound(AudioClip sound) {
    if (Settings.playSFX) {
      audioSource.PlayOneShot(sound);
    }
  }

  // TODO: consider if enemies should constantly be making sound (e.g. walking)
  public void PlayRunningSound() {
    // string materialRunningOn = inGame.GetTileMaterial(transform.position);

    // if (materialRunningOn != null) {
    //   AudioClip[] materialClips = Sounds.runningSounds[materialRunningOn][baseMaterial];
    //   PlaySound(materialClips[UnityEngine.Random.Range(0, materialClips.Length)]);
    // }
  }

  public void PlayAttackSound() {
    if (Settings.playSFX) {
      audioSource.PlayOneShot(Sounds.attackSounds[normalAttackType]);
    }
  }

  void Update() {
    direction = isFacingLeft ? -1 : 1;
    isDying = isBurning || isDeadByBurning || isDeadByPoison;

    if (hero.isAutonomous && gameObject.name == "Boss") {
      if (enemyRenderer.sprite.name == "boss-placeholder") {
        enemyRenderer.sprite = Sprites.firstBossSprites[key];
        isFacingLeft = !hero.isFacingLeft;
        if (isFacingLeft) {
          Flip();
        }
      }
    } else {
      if ((gameObject.name == "Boss" && isOnCamera) || gameObject.name != "Boss") {
        // DEFENSE CAST
        Vector2 defenseCast = new Vector2(transform.position.x + ((enemyWidth / 2) * reach * direction), transform.position.y + enemyHeight / 2 + 0.05f);
        Vector2 defenseCastDirection = transform.TransformDirection(new Vector2(1 * (direction), 0));

        RaycastHit2D defenseRayCast = Physics2D.Raycast(defenseCast, defenseCastDirection, reach * 2);
        Debug.DrawRay(defenseCast, defenseCastDirection.normalized * (reach * 2), Colors.raycastColors["defense"]);

        if (defenseRayCast && defenseRayCast.collider.tag == "Weapon") {
          if (level - hero.playerLevel >= 10) {
            isDefending = true;
            anim.SetTrigger("isDefending");
          }
        }


        if (hero != null && hero.pauseCase == "") {
          // ENEMY BURNING
            if (isBurning) {
              enemyColor = Colors.statusColors["burned"];

              if (Helpers.ExceedsTime(burnTime, burningDuration)) {
                isBurning = false;
                isDeadByBurning = true;
                // so flying enemies drop dead
                body.gravityScale = 1;
                transform.Find("Grounder").gameObject.SetActive(true); // so flying enemies can hit the ground while dropping
              }

              // make HP 0 so it reflects upon dying by burning in the boss status canvas
              currentHP = 0;
            }

          // ENEMY POISONED
            if (isPoisoned) {
              float currentTime = Time.time * 1000;
              float nextPoisonAttackTime = poisonTime + (poisonAttackInterval * poisonAttackCounter);

              if (currentTime > poisonEffectTime + poisonEffectDuration) {
                if (!isStunned) {
                  enemyRenderer.color = enemyColor;
                }

                if (poisonAttackCounter == maxPoisonAttacks + 1) {
                  isPoisoned = false;
                  poisonAttackCounter = 0;
                }
              }

              if (currentTime > nextPoisonAttackTime)  {
                inGame.PlaySound(Sounds.poisonSounds["basic"], transform.position);
                TakeDamage(Constants.arrowPoisonDamage);
                poisonEffectTime = Time.time * 1000;
                enemyRenderer.color = Colors.statusColors["poisoned"];
                poisonAttackCounter++;

                if (currentHP <= 0) {
                  isDeadByPoison = true;
                  isWalking = false;
                  body.velocity = Vector2.zero;
                  // so flying enemies drop dead
                  body.gravityScale = 1;

                  if (!isDead) { // avoids getting double exp if dying from poison after being attacked
                     if (!gaveExp) {
                      awardExp();
                     }
                  }
                }
              }
            }

          // ENEMY BURNING
            if (!isBurning) {
              if (isFacingLeft) {
                transform.localScale = new Vector3(-1, 1, 1);
              } else {
                transform.localScale = Vector3.one;
              }
            }

          // RESET ATTACKS RECEIVED
            if (Helpers.ExceedsTime(attackedStart, consecutiveAttackTime)) {
              attacksReceived = 0;
            }

          anim.SetBool("isThrowingWeapon", isThrowingWeapon);
          anim.SetBool("isAttacking", isAttacking);
          anim.SetBool("isAttackingMelee", isAttackingMelee);
          anim.SetBool("isBurning", isBurning);
          anim.SetBool("isDead", isDead);
          anim.SetBool("isDeadByBurning", isDeadByBurning);
          anim.SetBool("isDeadByPoison", isDeadByPoison);
          anim.SetBool("isStunned", isStunned);
          anim.SetBool("isStunnedOnAttack", stunOnAttack);
          anim.SetBool("isSummoning", isSummoning);
          anim.SetBool("isWalking", isWalking);
          anim.SetBool("needsCoolDown", needsCoolDown);
        }
      }
    }
  }

  void LateUpdate() {
    if (hero != null && hero.pauseCase == "") {
      if (!isPoisoned && !isStunned) {
        enemyRenderer.color = enemyColor;
      }
    }
  }

  public void CheckAttackToPlayer(Collider2D col) {
    if (col.gameObject.tag == "Hero" && isAttacking) {
      // isAttacking = false;
      coolDownStart = Time.time * 1000;
      if (!needsCoolDown) {
        // ensures the hero isn't damaged after being damaged
        if (!hero.isInvulnerable) {
          hero.ReceiveAttack(gameObject, col.ClosestPoint(transform.position));
        }
        needsCoolDown = true;
      }
    }
  }

  public void Collision(Collision2D col) {
    CheckAttackToPlayer(col.collider);
  }

  public void DamageCalculation(Collider2D col, int specificDamage, string damageSoundType, string weaponType = "", bool isCritical = false) {
    int damage = def - ((specificDamage + hero.strength + (int)hero.equippedSTR + (int)hero.effectSTR) * (isCritical ? 2 : 1));

    if ((weaponType == "throwable" || weaponType == "throwable-double") || !(isDefending && !attackedFromBehind)) {
      TakeDamage(damage < 0 ? Math.Abs(damage) : Constants.minimumDamageDealt, col.ClosestPoint(transform.position), isCritical, damageSoundType);
      if (!(weaponType == "throwable" || weaponType == "throwable-double")) {
        TurnWhenAttackedFromBehind();
      }
    } else {
      mustTakeDamage = false;
    }
  }

  public void Trigger(Collider2D col) {
    CheckAttackToPlayer(col);
    string colliderTag = col.gameObject.tag;

    if (colliderTag == "Weapon" && !hero.isParrying) {
      float currentX = transform.position.x;
      float enemyX = col.transform.position.x;
      bool willBurn = false;
      bool isCritical = Helpers.IsCritical(hero.criticalPercentage + hero.equippedCRIT + hero.effectCRIT);;
      string currentWeapon = "";
      int currentEquippedATK = 0;

      attackedFromBehind = (currentX < enemyX && isFacingLeft) || (currentX > enemyX && !isFacingLeft);

      if (hero.isKicking || hero.isDropKicking && !isDefending) {
        DamageCalculation(col, Constants.kickDamage, "kick", "", isCritical);
      } else {
        currentWeapon = hero.armUsed == 1 ? Hero.arm1Equipment : Hero.arm2Equipment;
        currentEquippedATK = hero.armUsed == 1 ? Hero.equippedATK1 : Hero.equippedATK2;

        if (currentWeapon == "" && !isDefending) {
          DamageCalculation(col, Constants.punchDamage, "punch", "", isCritical);
        } else {
          string weaponType = Objects.pauseItems[currentWeapon].type;

          if (weaponType == "single" || weaponType == "double" && !isDefending) {
            // TODO: might need to adjust to different types other than swords
            DamageCalculation(col, currentEquippedATK, "sword", weaponType, isCritical);
          } else if (weaponType == "throwable" || weaponType == "throwable-double") {
            GameObject parentObject = col.transform.parent.gameObject;
            Throwable parentThrowable = parentObject.GetComponent<Throwable>();
            string weaponWielded = parentThrowable.type;

            mustTakeDamage = (Helpers.IsNonBouncingThrowable(weaponWielded) && !parentThrowable.hasCollided) || (weaponWielded == "bomb" && parentThrowable.isExploding);

            if (mustTakeDamage) {
              string throwableSoundType = Helpers.GetThrowableSoundType(currentWeapon);
              DamageCalculation(col, currentEquippedATK, throwableSoundType, weaponType, isCritical);
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

              parentThrowable.transitionIncrement = 0;

              if (parentThrowable.type != "bomb") {
                parentObject.transform.parent.position = new Vector2(parentThrowable.transform.position.x, parentThrowable.transform.position.y);
                parentThrowable.bounceSprite = parentObject.GetComponent<SpriteRenderer>().sprite;
              }
            }
          } else if (Helpers.IsValueInArray(Constants.projectileHoldingWeaponTypes, weaponType)) {
            GameObject parentObject = col.transform.parent.gameObject;
            Arrow parentArrow = parentObject.GetComponent<Arrow>();
            string arrowUsed = parentArrow.type;

            mustTakeDamage = !parentArrow.hasCollided;
            willBurn = parentArrow.type == "arrow-fire" && !Helpers.IsFireResistant(elementResistances) && currentHP <= Constants.arrowExplosionDamage;

            if (mustTakeDamage) {
              int damage = (def * (isDefending ? 2 : 1)) - ((Helpers.GetDamage(arrowUsed) + hero.strength + (int)hero.equippedSTR + (int)hero.effectSTR) * (isCritical ? 2 : 1));

              // do not play standard damage sound if the arrow used is a fire arrow
              TakeDamage(damage < 0 ? Math.Abs(damage) : Constants.minimumDamageDealt, col.ClosestPoint(transform.position), isCritical, parentArrow.type == "arrow-fire" ? "" : "arrow");

              if (parentArrow.type == "arrow-poison" && !Helpers.IsPoisonResistant(elementResistances)) {
                isPoisoned = true;
                poisonTime = Time.time * 1000;
              }

              parentArrow.DestroyArrow();
            }
          } else if (isDefending && !Helpers.IsValueInArray(Constants.projectileHoldingWeaponTypes, weaponType)) {
            inGame.Block(col.ClosestPoint(transform.position), attackedFromBehind && isFacingLeft || !attackedFromBehind && !isFacingLeft);
          }
        }
      }

      if (mustTakeDamage) {
        if (currentHP > 0) {
          if (flashEffect != null && !isDefending) {
            flashEffect.Flash();
          }

          if (!willBurn) {
            if (attacksReceived >= attackRetaliationCounter) {
              if (level >= 30) {
                if (type == "champion") {
                  isAttackingMelee = true;
                  attackedStart = 0;
                } else {
                  isDefending = true;
                  anim.SetTrigger("isDefending");
                  // TODO: ensure all enemy types have a means to return to isDefending = false
                }
              } else {
                if (!attackedFromBehind && type == "champion") {
                  isDefending = true;
                  anim.SetTrigger("isDefending");
                } else {
                  Stun();
                }
              }
            } else {
              if (!isDefending) {
                Stun();
              }

              attacksReceived++;
              attackedStart = Time.time * 1000;
            }
          }
        } else {
          if (!isBurning) {
            isDead = true;
          }

          isPoisoned = false;
          isStunned = false;
          isWalking = false;
          body.velocity = Vector2.zero;
          deadPosition = new Vector2(transform.position.x, transform.position.y);

          if (!isDeadByPoison) { // avoids getting double exp if attacking while dying from poison
            if (!gaveExp) {
              awardExp();
            }
          }
        }
      }

      DisplayEnemyInInfoCanvas();
    } else if (colliderTag == "Shield") {
      if (isAttacking) {
        // TODO: consider reusing for higher level shields
        // Stun();
      }

      DisplayEnemyInInfoCanvas();
    } else if (colliderTag == "Explosion") {
      string colName = col.gameObject.name.Replace("(Clone)", "");

      if (colName == "ArrowExplosion" || colName == "ArrowBurn") {
        bool willBurn = !Helpers.IsFireResistant(elementResistances) && currentHP <= Constants.arrowExplosionDamage;

        if (willBurn) {
          float currentTime = Time.time * 1000;

          // only instantiate the flame if the enemy is not set to die (hence, !isDying)
          if (!isDying) {
            GameObject arrowBurn = Instantiate(Objects.prefabs["arrow-burn"], new Vector2(transform.position.x, transform.position.y + arrowBurnPosition), Quaternion.identity);

            // sets the parent room so that the flame can be found and deleted more easily on room exit
            arrowBurn.transform.SetParent(transform.parent);

            ArrowBurn arrowBurnScript = arrowBurn.GetComponent<ArrowBurn>();
            arrowBurnScript.startTime = currentTime;
            arrowBurnScript.burnDimensions = Objects.enemyDimensions[key];

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

  public void DisplayEnemyInInfoCanvas() {
    if (gameObject.name != "Boss") {
      hero.infoCanvas.GetComponent<InfoCanvas>().Display(enemyName, new EnemyHealth(currentHP, maxHP));
    }
  }

  public void TurnWhenAttackedFromBehind() {
    if (level >= 20 && attackedFromBehind) { // after level 20 enemy should be aware it's being hit from behind
      isFacingLeft = !isFacingLeft;
      Flip();
    }
  }

  public void TakeDamage(int damage, Vector2? damagePosition = null, bool? isCritical = false, string soundType = "") {
    int actualDamage = damage > Constants.maximumDamageDealt ? Constants.maximumDamageDealt : damage;
    currentHP -= actualDamage;

    if (Settings.showDamage) {
      Vector2 position = damagePosition ?? new Vector2(transform.position.x, transform.position.y + (enemyHeight / 2));
      inGame.DrawDamage(position, actualDamage, isCritical, soundType);
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
    isSummoning = false;
    isStunned = false;
    stunOnAttack = false;
    isWalking = true;
    isThrowingWeapon = false;
    distanceToPlayer = 0;
    maxThrowCounter = 0;
    isAttackingMelee = false;
    isDefending = false;
    playerFound = false;
  }

  public void PrepareWeaponThrow() {
    ThrowWeapon(distanceToPlayer);

    if (level >= 20 && maxThrowCounter < (maxThrows - 1)) {
      anim.Play("throw", -1, 0f);

      // if (maxThrowCounter % 2 != 0) {
      //   distanceToPlayer++;
      // } else {
      //   distanceToPlayer -= 2;
      // }

      if (maxThrowCounter % 2 == 0) {
        distanceToPlayer = 1;
      } else {
        distanceToPlayer = -1;
      }

      maxThrowCounter++;
    } else {
      Stun();
    }
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
    playerFound = false;
    isAttacking = false;
    isHitting = false;
    maxThrowCounter = 0;

    // checks if a RigidBody2D component exists. If not, add it
    Rigidbody2D currentBody = GetComponent<Rigidbody2D>();
    if (currentBody == null) {
      AddRigidBody();
    }
  }

  public void Destroy() {
    if (gameObject.name == "Boss") {
      transform.parent.Find("Bounds").gameObject.SetActive(false);
    }

    // prepares origin position given custom values if found
    Vector2 deathOrigin = new Vector2(transform.position.x, transform.position.y + (enemyHeight / 2)) +
      ((Objects.customEnemyDeathOriginModifiers.ContainsKey(key) ?
        Objects.customEnemyDeathOriginModifiers[key] :
        Vector2.zero) * new Vector2(direction, 0));

    // instantiates the dropped item
    string[] droppableAndRarity = Helpers.GetDroppableItem(key, level, hero.luckPercentage + hero.equippedLUCK + hero.effectLCK).Split('|');
    inGame.InstantiatePrefab("droppable", droppableAndRarity[0], droppableAndRarity[1], transform.parent.gameObject, deathOrigin, enemyRenderer);

    // instantiates the explosion of the enemy
    Instantiate(Objects.prefabs["enemy-explosion"], deathOrigin, Quaternion.identity);

    if (gameObject.name == "Boss") {
      GameObject.Find("BossStatusCanvas").SetActive(false);
      hero.isFightingBoss = false;

      // TODO: figure out how to use location
      inGame.SwitchFromMiniBossTrack("meadows");
    }

    Destroy(gameObject);
  }

  public void ThrowWeapon(float distance) {
    float throwableX = transform.position.x + ((isFacingLeft ? -2.5f : 1) * enemyWidth) + (distance * ( isFacingLeft ? 1 : -1));
    float throwableY = transform.position.y + (enemyWidth);

    GameObject throwableWeapon = Instantiate(Objects.prefabs["throwable"], new Vector3(throwableX, throwableY, 0), Quaternion.identity);
    GameObject throwableObject = throwableWeapon.transform.Find("Throwable").gameObject;
    Throwable throwableInstance = throwableObject.GetComponent<Throwable>();

    throwableInstance.isFacingLeft = isFacingLeft;
    // TODO: change when implementing other throwable types
    throwableInstance.type = "king-bone";
    throwableInstance.criticalRate = criticalRate;

    // TODO: change when implementing other throwable types
    if (Settings.playSFX) {
      audioSource.PlayOneShot(Sounds.attackSounds["throwable-double-large"]);
    }

    Transform throwableCollider = throwableObject.transform.Find("ThrowableCollider");
    throwableCollider.eulerAngles = Vector3.zero;
    throwableCollider.gameObject.tag = "EnemyWeapon";
  }

  public void Summon() {
    GameObject summonEnergy = Instantiate(Objects.prefabs["summon-energy"], new Vector3(transform.position.x + (isFacingLeft ? -1 : 1), transform.position.y, 0), Quaternion.identity);
    summonEnergy.GetComponent<SummonEnergy>().summonKey = summonKey != "" ? summonKey : Constants.meadowEnemies[UnityEngine.Random.Range(0, Constants.meadowEnemies.Length)];
    summonEnergy.GetComponent<SummonEnergy>().currentRoom = transform.parent.gameObject;
  }

  public void Smash() {
    GameObject smashWave = Instantiate(Objects.prefabs["smash-wave"], new Vector3(transform.position.x + (isFacingLeft ? -2 : 2), transform.position.y, 0), Quaternion.identity);
    SmashWave smashWaveScript = smashWave.GetComponent<SmashWave>();
    smashWaveScript.width = enemyWidth * 2;
    smashWaveScript.damage = atk * 2;
  }

  public bool ShouldMove() {
    return !isDead && !isDeadByBurning && !isDeadByPoison && !isBurning && !isStunned && !isThrowingWeapon && !isAttackingMelee && !isDefending && !isSummoning;
  }

  public bool WillDie() {
    return isDead || isDeadByBurning || isDeadByPoison || isBurning;
  }

  public void OnGUI() {
    if (hero.showDebug) {
      string guiLabel = "HP: " + currentHP + "\n";
      GUI.Label(new Rect(600, 0, 200, 400), guiLabel);
    }

    // if (key == "skeleton-king") {
    //   string guiLabel = "Attacks received: " + attacksReceived + "\n";
    //   GUI.Label(new Rect(600, 0, 200, 400), guiLabel);
    // }
  }

  public void RemoveRigidBody() {
    Destroy(GetComponent<Rigidbody2D>());
  }

  public void AddRigidBody() {
    gameObject.AddComponent<Rigidbody2D>();
    body = GetComponent<Rigidbody2D>();
    body.constraints = RigidbodyConstraints2D.FreezeRotation;
  }
}
