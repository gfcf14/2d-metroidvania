using System;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {
  [System.NonSerialized] public bool showDebug = false;

  [System.NonSerialized] public string pauseCase = "";
  [SerializeField] public float speed;
  [SerializeField] private float jumpHeight;
  [SerializeField] private float jetpackHeight;
  [SerializeField] public GameObject infoCanvas;

  [SerializeField] public GameObject hpBarContainer;
  [SerializeField] public GameObject mpBarContainer;
  [SerializeField] public GameObject weaponCollider;
  [SerializeField] public GameObject levelUpCanvas;
  [SerializeField] public GameObject fadeOutCanvas;
  private Rigidbody2D body;
  private Animator anim;
  private SpriteRenderer heroRenderer;

  private float heroHeight;
  private float heroWidth;

  // for when player must move on their own
  public bool isAutonomous = false;
  public bool mustTransitionOnAir = false;

  public bool isRunning;
  public bool isGrounded;
  public bool canFlipOnAir;
  public bool isFalling;
  public bool isJumping;

  // public bool isJetpackUp;
  // public string jetpackHorizontal = "";
  // public float maxJetpackTime = 1500;
  // public float jetpackTime = 0;

  public float currentYPosition = 0;
  public float currentXPosition = 0;

  public float throwbackHeight = 0;

  public int isHurt = 0;
  public int hurtCounter = 0; // to increment/decrement position by mapping
  public int isDead = 0;

  public bool isDefending = false;
  public bool isParrying = false;
  public bool isClashing = false;

  // public bool isGliding;
  public bool isFacingLeft;

  public bool isAttackingSingle;
  public bool isAttackingHeavy;

  public bool isAirAttackSingle;
  public bool isAirAttackHeavy;

  public bool isKicking;
  public bool isDropKicking;

  public bool isPunching;
  public bool isAirPunching;

  // public bool isAirShooting;

  // isThrowing determines a throw in an int, so it's 0 when not throwing, but 1 or 2 depending on what arm throws
  public int isThrowing;
  public bool startThrow = false;

  // public bool isShootingSingle;
  // public bool isShootingAuto;

  public bool isShootingPull;

  public bool horizontalCollision;

  public int collisionCounter = 0;

  public float horizontalInput = 0;
  public float verticalInput = 0;
  public int armUsed = 0;

  // public string[] weapons = new string[] {"fists", "single", "heavy", "throwables", "projectile-single", "projectile-heavy", "projectile-auto", "projectile-pull"};

  // public string jetpackUp = "🡣🡡⌴";
  public string jetpackUp = "du$";
  // public string jetpackLeft = "🡣🡠⌴";
  public string jetpackLeft = "dl$";
  // public string jetpackRight = "🡣🡢⌴";
  public string jetpackRight = "dr$";
  public float timeoutDuration = 0.25f;

  private string userInput = "";
  private float timeoutTime = 0.0f;

  public int weaponIndex = 0;

  public string currentWeapon;

  public bool hurtFromBehind = false;

  public bool isHoldingDown = false;

  // PLAYER STATS
    [System.NonSerialized] public int playerLevel = 1;
    [System.NonSerialized] public int currentHP = 1500;
    [System.NonSerialized] public int maxHP = 5000;
    [System.NonSerialized] public int currentMP = 1500;
    [System.NonSerialized] public int maxMP = 2000;
    [System.NonSerialized] public string status = "good";
    [System.NonSerialized] public int exp = 80;
    [System.NonSerialized] public int next = 0;
    [System.NonSerialized] public int gold = 650;
    [System.NonSerialized] public int strength = 5;
    [System.NonSerialized] public int stamina = 5;
    [System.NonSerialized] public float luckPercentage = 0.05f;
    [System.NonSerialized] public float criticalPercentage = 0.05f;
    [System.NonSerialized] public string location = "meadows";
    [System.NonSerialized] public HeroMagicResistance[] magicResistances = new HeroMagicResistance[] {
      new HeroMagicResistance() {name = "earth", frequency = 1},
      new HeroMagicResistance() {name = "air", frequency = 0},
      new HeroMagicResistance() {name = "water", frequency = 0},
      new HeroMagicResistance() {name = "fire", frequency = 0},
      new HeroMagicResistance() {name = "lightning", frequency = 1},
      new HeroMagicResistance() {name = "ice", frequency = 0},
      new HeroMagicResistance() {name = "light", frequency = 0},
      new HeroMagicResistance() {name = "dark", frequency = 0}
    };

    [System.NonSerialized] public Dictionary<string, int> magicResistanceTypeIndex = new Dictionary<string, int> {
      {"Earth", 0},
      {"Air", 1},
      {"Water", 2},
      {"Fire", 3},
      {"Lightning", 4},
      {"Ice", 5},
      {"Light", 6},
      {"Dark", 7},
    };

  // PLAYER EQUIPMENT
    [System.NonSerialized] public static string bodyEquipment = "body-1";
    [System.NonSerialized] public static string arm1Equipment = "basic-longsword";
    [System.NonSerialized] public static string arm2Equipment = "basic-longsword";
    [System.NonSerialized] public static string neckEquipment = "";
    [System.NonSerialized] public static string armwear1Equipment = "silver-bracelet";
    [System.NonSerialized] public static string armwear2Equipment = "";
    [System.NonSerialized] public static string ring1Equipment = "";
    [System.NonSerialized] public static string ring2Equipment = "";

    [System.NonSerialized] public static string projectileEquipment = "";

    [System.NonSerialized] public string[] equipmentArray = { bodyEquipment, arm1Equipment, arm2Equipment, neckEquipment, armwear1Equipment, armwear2Equipment, ring1Equipment, ring2Equipment};

  // PLAYER EQUIPPED STATS
    [System.NonSerialized] public float equippedSTR = 0f;
    [System.NonSerialized] public float equippedSTA = 0f;
    [System.NonSerialized] public float equippedLUCK = 0f;
    [System.NonSerialized] public float equippedCRIT = 0f;
    [System.NonSerialized] public int equippedATK1 = 0;
    [System.NonSerialized] public int equippedATK2 = 0;
    [System.NonSerialized] public int equippedDEF1 = 0;
    [System.NonSerialized] public int equippedDEF2 = 0;

  [System.NonSerialized] public List<Item> items = new List<Item>();

  public int tiredThreshold = 40;

  private int maxShieldHP = 0;
  private float currentShieldHP = 0;
  private float currentShieldRecoverTime = 0;
  private float shieldDropTime = 0;

  // TODO: remove these variables and add a new dictionary when adding another shield sprite
  public int dummyShieldHP = 5;
  public float dummyShieldRecoverTime = 2000;

  private GameObject arrowAnchor;
  private Arrow arrowScript;
  private GameObject arrowMask;

  public bool isPaused;
  [SerializeField] GameObject pauseCanvas;

  [System.NonSerialized] public GameObject currentRoom;

  [System.NonSerialized] bool canMap = false;

  [System.NonSerialized] public int bossTransitionDirection = 0;

  // called when script is loaded
  private void Start() {
    body = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    heroRenderer = GetComponent<SpriteRenderer>();

    // currentWeapon = weapons[weaponIndex % weapons.Length];

    heroHeight = heroRenderer.bounds.size.y;
    heroWidth = heroRenderer.bounds.size.x;

    // TODO: move this to shield equipment change once equipment options are available
    maxShieldHP = dummyShieldHP;
    currentShieldHP = maxShieldHP;
    currentShieldRecoverTime = dummyShieldRecoverTime;

    arrowAnchor = transform.Find("ArrowAnchor").gameObject;

    //test items
    items.Add(new Item("chicken-drumstick", 5));
    items.Add(new Item("basic-shield", 2));
    items.Add(new Item("basic-sword", 1));
    items.Add(new Item("basic-longsword", 1));
    items.Add(new Item("love-necklace", 1));
    // items.Add(new Item("solomon-ring", 1));
    items.Add(new Item("ra-ring", 1));
    items.Add(new Item("skull-ring", 1));
    items.Add(new Item("gold-bracelet", 1));
    items.Add(new Item("silver-bracelet", 1));
    items.Add(new Item("rabbit-paw", 1));
    items.Add(new Item("moonlight-pendant", 1));
    items.Add(new Item("rainbow-bracer", 1));
    items.Add(new Item("body-1", 1));
    items.Add(new Item("lance", 3));
    items.Add(new Item("axe", 2));
    items.Add(new Item("hatchet", 14));
    items.Add(new Item("shuriken-6", 23));
    items.Add(new Item("shuriken-4", 50));
    items.Add(new Item("knife", 3));
    items.Add(new Item("kunai", 37));
    items.Add(new Item("bow", 1));
    items.Add(new Item("arrow-standard", 21));
    items.Add(new Item("arrow-poison", 5));
    items.Add(new Item("arrow-fire", 10));
    items.Add(new Item("bomb", 99));

    UpdateStatsValues();

    next = Helpers.NextLevelEXP(playerLevel + 1);
  }

  public void UpdateStatsValues() {
    equippedSTR = PrepareEquippedStat("atk");
    equippedSTA = PrepareEquippedStat("def");
    equippedLUCK = PrepareEquippedStat("luck");
    equippedCRIT = PrepareEquippedStat("crit");

    equippedATK1 = (arm1Equipment != "" ? Objects.pauseItems[arm1Equipment].effects.atk != null ? (int)Objects.pauseItems[arm1Equipment].effects.atk : 0 : 0) + (projectileEquipment != "" ? Objects.pauseItems[projectileEquipment].effects.atk ?? 0 : 0);
    equippedATK2 = (arm2Equipment != "" ? Objects.pauseItems[arm2Equipment].effects.atk != null ? (int)Objects.pauseItems[arm2Equipment].effects.atk : 0 : 0)  + (projectileEquipment != "" ? Objects.pauseItems[projectileEquipment].effects.atk ?? 0 : 0);
    equippedDEF1 = arm1Equipment != "" ? Objects.pauseItems[arm1Equipment].effects.def != null ? (int)Objects.pauseItems[arm1Equipment].effects.def : 0 : 0;
    equippedDEF2 = arm2Equipment != "" ? Objects.pauseItems[arm2Equipment].effects.def != null ? (int)Objects.pauseItems[arm2Equipment].effects.def : 0 : 0;

    equippedSTR = equippedSTR - equippedATK1 - equippedATK2 + (projectileEquipment != "" ? Objects.pauseItems[projectileEquipment].effects.atk * 2 ?? 0 : 0);
    equippedSTA = equippedSTA - equippedDEF1 - equippedDEF2 + (projectileEquipment != "" ? Objects.pauseItems[projectileEquipment].effects.atk * 2 ?? 0 : 0);

    UpdateMagicResistances();
  }

  public float PrepareEquippedStat(string effect) {
    float totalStat = 0.0f;

    int i = 0;
    foreach (string currentEquipment in equipmentArray) {
      if (currentEquipment != "") {
        Effects currentEffects = Objects.pauseItems[currentEquipment].effects;
        if (currentEffects.GetType().GetField(effect).GetValue(currentEffects) != null) {
          totalStat += float.Parse(currentEffects.GetType().GetField(effect).GetValue(currentEffects).ToString());
        }
      }

      i++;
    }

    return totalStat;
  }

  public void UpdateMagicResistances() {
    foreach (HeroMagicResistance currHeroMR in magicResistances) {
      currHeroMR.frequency = 0;
    }

    foreach (string currentEquipment in equipmentArray) {
      if (currentEquipment != "") {
        PauseItem currentPauseItem = Objects.pauseItems[currentEquipment];

        if (currentPauseItem.effects.magicResistances != null) {
          foreach (MagicResistance currMagicResistance in currentPauseItem.effects.magicResistances) {
            magicResistances[magicResistanceTypeIndex[currMagicResistance.name]].frequency += currMagicResistance.type == "add" ? 1 : -1;
          }
        }
      }
    }
  }

  public void EquipItem(string newItem, int itemIndex) {
    string newItemType = Objects.pauseItems[newItem].type;
    switch (itemIndex) {
      case 0:
        bodyEquipment = newItem;
      break;
      case 1:
        arm1Equipment = newItem;

        if (Helpers.IsValueInArray(Constants.doubleHandedWeaponTypes, newItemType)) {
          arm2Equipment = newItem;
        } else {
          if (arm2Equipment != "") {
            string arm2Type = Objects.pauseItems[arm2Equipment].type;

            if (Helpers.IsValueInArray(Constants.doubleHandedWeaponTypes, arm2Type)) {
              arm2Equipment = "";
            }
          }
        }
      break;
      case 2:
        arm2Equipment = newItem;

        if (Helpers.IsValueInArray(Constants.doubleHandedWeaponTypes, newItemType)) {
          arm1Equipment = newItem;
        } else {
          if (arm1Equipment != "") {
            string arm1Type = Objects.pauseItems[arm1Equipment].type;

            if (Helpers.IsValueInArray(Constants.doubleHandedWeaponTypes, arm1Type)) {
              arm1Equipment = "";
            }
          }
        }
      break;
      case 3:
        neckEquipment = newItem;
      break;
      case 4:
        armwear1Equipment = newItem;
      break;
      case 5:
        armwear2Equipment = newItem;
      break;
      case 6:
        ring1Equipment = newItem;
      break;
      case 7:
        ring2Equipment = newItem;
      break;
      default:
        Debug.Log("Updating item " + newItem + " at index " + itemIndex + " somehow");
      break;
    }

    equipmentArray = new string[] { bodyEquipment, arm1Equipment, arm2Equipment, neckEquipment, armwear1Equipment, armwear2Equipment, ring1Equipment, ring2Equipment};
    UpdateStatsValues();
  }

  public void UpdateStats(string stat, int? aggregate) {
    switch (stat) {
      case "hp":
        int hpAggregate = aggregate ?? 0;

        // condition below prevents healing more than allowed
        if ((currentHP + hpAggregate) >= maxHP) {
          currentHP = maxHP;
        } else {
          currentHP += hpAggregate;
        }
      break;
      default:
        Debug.Log("Value for stat \"" + stat + "\" doesn't correspond to this function. Either overload another or check the type");
      break;
    }

    // TODO: build the others as more items are created!
  }

  public void ConsumeItem(string key) {
    int i = 0;
    foreach (Item currItem in items) {
      if (key == currItem.key) {
        currItem.amount--;
        break;
      }
      i++;
    }
  }

  public void RemoveItem(int index) {
    items.RemoveAt(index);
  }

  // called on every frame of the game
  private void Update() {
    if (!isAutonomous) {
      // TODO: remove key combinations as they will not be used to favor two keys pressed
      foreach (KeyCode currentKey in System.Enum.GetValues(typeof(KeyCode))) {
        if(Input.GetKeyUp(currentKey)) {
          if (userInput.Length == 0) {
            timeoutTime = Time.time + timeoutDuration;
          }

          // if (currentKey.ToString() == "Space") {
          //   // userInput += "⌴";
          //   userInput += "$";
          // } else
          if (currentKey.ToString() == "UpArrow") {
            // userInput += "🡡";
            userInput += "u";
          } else if (currentKey.ToString() == "DownArrow") {
            // userInput += "🡣";
            userInput += "d";
          } else if (currentKey.ToString() == "LeftArrow") {
            // userInput += "🡠";
            userInput += "l";
          } else if (currentKey.ToString() == "RightArrow") {
            // userInput += "🡢";
            userInput += "r";
          } else {
            if (currentKey.ToString() != "Space") {
              userInput += currentKey.ToString();
            }
          }

          // if (userInput.Contains(jetpackUp)) {
          //   JetpackUp();
          //   userInput = "";
          // } else if (userInput.Contains(jetpackLeft)) {
          //   Debug.Log("jetpack left");
          //   userInput = "";
          // } else if (userInput.Contains(jetpackRight)) {
          //   Debug.Log("jetpack right");
          //   userInput = "";
          // } else if (userInput == "s") { // jumping?
          //   Jump();
          // }

          // Debug.Log(currentKey.ToString());

          if (isPaused && Pause.currentlyMapping != "") {
            if (((currentKey.ToString()).Contains("JoystickButton") && Input.GetJoystickNames()[0] != "") || !(currentKey.ToString()).Contains("Joystick")) {
              if (canMap) {
                if (!Helpers.IsForbiddenToRemap(currentKey.ToString())) {
                  pauseCanvas.GetComponent<Pause>().FinishMapping(currentKey.ToString());
                  canMap = false;
                }
              } else {
                canMap = true;
              }
            }
          }
        } else if (Time.time > timeoutTime && userInput.Length > 0) { // input is cleared
          userInput = "";
        }
      }

      if (!isPaused && pauseCase == "") {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        float verticalSpeed = body.velocity.y;

        if (shieldDropTime != 0) {
          if (Helpers.ExceedsTime(shieldDropTime, currentShieldRecoverTime)) {
            currentShieldHP = maxShieldHP;
            shieldDropTime = 0;
          }
        }

        // x axis movement
        if (!horizontalCollision && isHurt < 1) {
          if (!isDefending && !isParrying && !isClashing && isThrowing == 0) {
            // movement happens on this line
            body.velocity = new Vector2(!isDropKicking ? horizontalInput * speed : 0, body.velocity.y);
          }

          // flip player back when moving right
          if (horizontalInput > 0.01f && (isGrounded || canFlipOnAir) && !isAttackingSingle) {
            transform.localScale = Vector3.one;

            if (!isDropKicking) {
              isFacingLeft = false;
            }
          }
          // flip player when moving left
          else if (horizontalInput < -0.01f && (isGrounded || canFlipOnAir) && !isAttackingSingle) {
            FlipPlayer();

            if (!isDropKicking) {
              isFacingLeft = true;
            }
          }
        }

        if (isClashing) {
          // TODO: modify the 2 to make it a multiplie based on enemy strength (?)
          body.velocity = new Vector2( (isFacingLeft ? 1 : -1) * speed * 2, body.velocity.y);
        }

        if (isHurt == 1) {
          body.velocity = new Vector2(0, body.velocity.y);
        }

        // jumping
        if (Helpers.IsKeyHeld(Controls.currentKeyboardJump) || Helpers.IsKeyHeld(Controls.currentGamepadJump)) {
          if (isGrounded) {
            if (isHoldingDown) {
              if (!isRunning) {
                isKicking = true;
                weaponCollider.SetActive(true);
              }
            } else {
              Jump();
            }
          } else {
            if (isHoldingDown && isJumping && !isFalling) {
              DropKick();
            }
          }
          // userInput += "$";
          // if (isGrounded) {
          //   if (userInput.Contains(jetpackUp)) {
          //     JetpackUp();
          //     userInput = "";
          //   } else {
          //     Jump();
          //     userInput = "";
          //   }
          // } else {
          //   if (userInput.Contains(jetpackLeft)) {
          //     JetpackHorizontal("left");
          //     userInput = "";
          //   } else if (userInput.Contains(jetpackRight)) {
          //     JetpackHorizontal("right");
          //     userInput = "";
          //   }
          // }
        }

        if (verticalInput < 0) {
          isHoldingDown = true;
        }

        if (verticalInput == 0) {
          isHoldingDown = false;
        }

        // arm 1
        if (Helpers.IsKeyDown(Controls.currentKeyboardAttack1) || Helpers.IsKeyDown(Controls.currentGamepadAttack1)) {
          DecideAttackType(arm1Equipment, 1);
        }
        if (Helpers.IsKeyUp(Controls.currentKeyboardAttack1) || Helpers.IsKeyUp(Controls.currentGamepadAttack1)) {
          DecideShieldRelease(arm1Equipment);
          isParrying = false;
        }

        // arm 2
        if (Helpers.IsKeyDown(Controls.currentKeyboardAttack2) || Helpers.IsKeyDown(Controls.currentGamepadAttack2)) {
          DecideAttackType(arm2Equipment, 2);
        }
        if (Helpers.IsKeyUp(Controls.currentKeyboardAttack2) || Helpers.IsKeyUp(Controls.currentGamepadAttack2)) {
          DecideShieldRelease(arm2Equipment);
          isParrying = false;
        }

        // if (!isGrounded && verticalSpeed < -1 && jetpackHorizontal == "") {
        if (!isGrounded && verticalSpeed < -1) {
          Fall();
        }

        // if (Input.GetKey(KeyCode.Keypad4) && currentWeapon == "projectile-auto") {
        //   isShootingAuto = true;
        // }

        // if (Input.GetKeyUp(KeyCode.Keypad4) && isShootingAuto) {
        //   isShootingAuto = false;
        // }


        // if (Input.GetKeyDown(KeyCode.Keypad7)) {
        //   PlayerHurt(1);
        // }

        // if (Input.GetKeyDown(KeyCode.Keypad8) && isGrounded) {
        //   PlayerHurt(2);
        // }

        // if (Input.GetKeyDown(KeyCode.Keypad9)) {
        //   PlayerHurt(3);
        // }

        if (isDropKicking) {
          body.velocity = new Vector2(body.velocity.x + (jumpHeight * (isFacingLeft ? -1 : 1)), -(float)(jumpHeight * 0.75));
        }

        // if (isGliding) {
        //   body.velocity = new Vector2(body.velocity.x + (jumpHeight * (isFacingLeft ? -1 : 1)), -(float)(jumpHeight * 0.25));
        // }

        // if (jetpackHorizontal != "") {
        //   body.velocity = new Vector2(body.velocity.x + (jetpackHeight * (jetpackHorizontal == "left" ? -1 : 1)), body.velocity.y);
        //   transform.position = new Vector2(transform.position.x, currentYPosition);
        //   if ((Time.time * 1000) > jetpackTime + maxJetpackTime) {
        //     jetpackHorizontal = "";
        //     jetpackTime = 0;
        //     body.velocity = new Vector2(0, 0);
        //   }
        // }

        if (isDead == 2) {
          if (!isGrounded) {
            body.velocity = new Vector2(-body.velocity.x + (jumpHeight * (isFacingLeft ? 2 : -2)), -(float)jumpHeight);
          } else {
            body.velocity = Vector2.zero;
          }
        }
      }

      // gliding
      // if (Input.GetKey(KeyCode.UpArrow)) {
      //   if (!isGrounded) {
      //     if (Input.GetKey(KeyCode.Space)) {
      //       Glide();
      //     } else {
      //       isGliding = false;
      //     }
      //   }
      // } else {
      //   isGliding = false;
      // }

      if (pauseCase == "") { // only update isRunning if it's not paused in any way
        isRunning = horizontalInput != 0 && !isJumping && !isFalling && !isAttackingSingle; // && !isJetpackUp;
      }

      // TESTING FOR PROGRAMMATIC PLAY
      // if (isRunning) {
      //   anim.Play("running-1", -1, 0f);
      // }

      if (Helpers.IsPauseKeyUp() && pauseCase == "") {
        isPaused = !isPaused;
        Helpers.TogglePause(isPaused, pauseCanvas);
      }

      if (Helpers.IsBackKeyDown() && isPaused) {
        // do not perform "BACK" if a key is being mapped
        if (Pause.canvasStatus != "mapping") {
          if (Pause.canvasStatus == "main") {
            isPaused = !isPaused;
            Helpers.TogglePause(isPaused, pauseCanvas);
          } else {
            pauseCanvas.GetComponent<Pause>().PerformBack();
          }
        }
      }
    }

    // UNCOMMENT ALL THESE TO START TESTING FOR PROGRAMMATIC PLAY - use the Hero - Copy animator
    // set animator parameters
    anim.SetBool("isRunning", isRunning);
    anim.SetBool("isGrounded", isGrounded);
    anim.SetBool("isFalling", isFalling);
    anim.SetBool("isJumping", isJumping);
    // anim.SetBool("isJetpackUp", isJetpackUp);
    anim.SetBool("horizontalCollision", horizontalCollision);
    anim.SetBool("isAttackingSingle", isAttackingSingle);
    anim.SetBool("isAirAttackSingle", isAirAttackSingle);
    anim.SetBool("isAirAttackHeavy", isAirAttackHeavy);
    anim.SetBool("isKicking", isKicking);
    anim.SetBool("isDropKicking", isDropKicking);
    anim.SetBool("isAirPunching", isAirPunching);
    // anim.SetBool("isShootingSingle", isShootingSingle);
    // anim.SetBool("isShootingAuto", isShootingAuto);
    anim.SetBool("isShootingPull", isShootingPull);
    // anim.SetBool("isAirShooting", isAirShooting);
    anim.SetBool("isAttackingHeavy", isAttackingHeavy);
    // anim.SetBool("isJetpackHorizontal", jetpackHorizontal != "");
    // anim.SetBool("isGliding", isGliding);
    anim.SetBool("isTired", (float)currentHP / (float)maxHP <= 0.2f);
    anim.SetInteger("isHurt", isHurt);
    anim.SetInteger("isDead", isDead);
    anim.SetBool("isDefending", isDefending);
    anim.SetBool("isParrying", isParrying);
    anim.SetBool("isClashing", isClashing);
  }

  void FixedUpdate() {
    if (isAutonomous) {
      if (isGrounded) {
        isRunning = true;
        body.velocity = new Vector2(5 * bossTransitionDirection, 0);
      } else {
        isFalling = true;
        anim.Play("falling-1", -1, 0f);
        if (mustTransitionOnAir) {
          body.velocity = new Vector2(5 * bossTransitionDirection, 0);
        }
      }
    } else {
      if (isHurt == 2 && isGrounded) {
        float xIncrement = hurtCounter >= Constants.HurtBTransitions.Length ? 0 : Constants.HurtBTransitions[hurtCounter];
        transform.position = new Vector2(transform.position.x + ((isFacingLeft ? 1 : -1) * xIncrement * (hurtFromBehind ? -1 : 1)), currentYPosition);
        hurtCounter++;
      }

      if (isHurt == 3) {
        if (body.gravityScale == 1) {
          body.gravityScale = 0;
        }

        if (body.interpolation == RigidbodyInterpolation2D.Interpolate) {
          body.interpolation = RigidbodyInterpolation2D.Extrapolate;
        }

        int index = hurtCounter;
        float xIncrement =  Constants.hurtCXTransitions[hurtCounter >= Constants.hurtCXTransitions.Length ? Constants.hurtCXTransitions.Length - 1 : hurtCounter];
        float yIncrement =  Constants.hurtCYTransitions[hurtCounter >= Constants.hurtCYTransitions.Length ? Constants.hurtCYTransitions.Length - 1 : hurtCounter];

        transform.position = new Vector2(currentXPosition + (xIncrement * (isFacingLeft ? -1 : 1) * (hurtFromBehind ? -1 : 1)), currentYPosition + yIncrement);

        hurtCounter++;
      }
    }
  }

  void DecideAttackType(string armEquipment, int armIndex) {
    armUsed = armIndex;

    if (isGrounded) {
        if (armEquipment == "") {
          isPunching = true;
          anim.SetTrigger("isPunching");
          weaponCollider.SetActive(true);
        } else {
          string weaponType = Objects.pauseItems[armEquipment].type;

          switch (weaponType) {
            case "single":
              isAttackingSingle = true;
              weaponCollider.SetActive(true);
            break;
            case "double":
              if (isHoldingDown) {
                isParrying = true;
              } else {
                isAttackingHeavy = true;
                // TESTING FOR PROGRAMMATIC PLAY
                // anim.Play("attack-longsword-1", -1, 0f);
                weaponCollider.SetActive(true);
              }
            break;
            case "throwable":
            case "throwable-double":
              isThrowing = armUsed;
              anim.SetTrigger("isThrowing");
            break;
            case "bow":
              isShootingPull = true;
            break;
            case "defense":
              if (currentShieldHP > 0) {
                isDefending = true;
              }
            break;
            default:
              Debug.Log("Case " + weaponType + " is not accounted for");
            break;
          }
        }
      } else if (isJumping || isFalling) {
        if (armEquipment == "") {
          isAirPunching = true;
          weaponCollider.SetActive(true);

          // if (isHoldingDown) {
          //   if (isJumping && !isFalling) {
          //     DropKick();
          //   }
          // } else {
          //   isAirPunching = true;
          // }
        } else {
          string weaponType = Objects.pauseItems[armEquipment].type;

          switch (weaponType) {
             case "single":
              isAirAttackSingle = true;
              weaponCollider.SetActive(true);
            break;
            case "double":
              isAirAttackHeavy = true;
              weaponCollider.SetActive(true);
            break;
            default:
              Debug.Log("Case " + weaponType + " is not accounted for");
            break;
          }
        }
      }
  }

  void DecideShieldRelease(string armEquipment) {
    if (Helpers.IsValueInArray(Constants.shields, armEquipment)) {
      isDefending = false;
    }
  }

  public void FlipPlayer(bool hasBeenHurt = false) {
    if (hasBeenHurt && isFacingLeft) {
      transform.localScale = Vector3.one;
    } else {
      transform.localScale = new Vector3(-1, 1, 1);
    }
  }

  void PlayerDying(bool isGrounded) {
    isDead = isGrounded ? 1 : 2;
  }

  void PlayerHurt(int hurtLevel) {
    body.velocity = Vector2.zero;
    isHurt = hurtLevel;
    hurtCounter = 0;

    if (hurtLevel > 1) {
      currentXPosition = transform.position.x;
      currentYPosition = transform.position.y;
    }

    if (hurtLevel == 3) {
      throwbackHeight = 2f;
    }
  }

  void StartThrow() {
    string throwableType = Helpers.GetPauseItemKeyByName(Objects.pauseItems[isThrowing == 1 ? arm1Equipment : arm2Equipment].name);

    float throwableX = transform.position.x + ((isFacingLeft ? -1 : 1) * heroWidth * (throwableType == "axe" ? 0 : 1));
    float throwableY = transform.position.y + (heroHeight * (throwableType == "axe" ? 0.5f : 0.75f));

    GameObject throwableWeapon = Instantiate(Objects.prefabs["throwable"], new Vector3(throwableX, throwableY, 0), Quaternion.identity);
    Throwable throwableInstance = throwableWeapon.transform.Find("Throwable").gameObject.GetComponent<Throwable>();

    throwableInstance.isFacingLeft = isFacingLeft;
    throwableInstance.type = throwableType;
  }

  public void Recover() {
    isHurt = 0;
    body.gravityScale = 1;
    body.interpolation = RigidbodyInterpolation2D.Interpolate;
  }

  void ClearPunch() {
    isPunching = false;
    weaponCollider.SetActive(false);
  }

  void ClearAirPunch() {
    isAirPunching = false;
    weaponCollider.SetActive(false);
  }

  void ClearAttackSingle() {
    isAttackingSingle = false;
    armUsed = 0;
    weaponCollider.SetActive(false);
  }

  void ClearAirAttackSingle() {
    isAirAttackSingle = false;
    armUsed = 0;
    weaponCollider.SetActive(false);
  }

  void ClearAirAttackHeavy() {
    isAirAttackHeavy = false;
    armUsed = 0;
    weaponCollider.SetActive(false);
  }

  // void ClearAirShooting() {
  //   isAirShooting = false;
  // }

  void ClearKick() {
    isKicking = false;
    weaponCollider.SetActive(false);
  }

  void ClearThrow() {
    isThrowing = 0;
  }

  // void ClearShootingSingle() {
  //   isShootingSingle = false;
  // }

  void CreateArrow() {
    GameObject currentArrow = Instantiate(Objects.prefabs["arrow"], new Vector3(arrowAnchor.transform.position.x, arrowAnchor.transform.position.y + 0.2f, arrowAnchor.transform.position.z), Quaternion.identity);
    GameObject arrowInstance = currentArrow.transform.Find("Arrow").gameObject;
    arrowScript = arrowInstance.GetComponent<Arrow>();
    arrowMask = arrowInstance.transform.Find("Mask").gameObject;

    arrowScript.isFacingLeft = isFacingLeft;
    arrowScript.type = projectileEquipment;
  }

  void ShowCurrentArrowMask() {
    arrowMask.GetComponent<SpriteMask>().enabled = true;
  }

  void FireArrow() {
    Destroy(arrowMask);
    arrowScript.hasFired = true;
  }

  void ClearShootingPull() {
    isShootingPull = false;
  }

  void ClearAttackHeavy() {
    isAttackingHeavy = false;
    armUsed = 0;
    weaponCollider.SetActive(false);
    // TESTING FOR PROGRAMMATIC PLAY
    // anim.Play("idle-1", -1, 0f);
  }

  void DropDefense() {
    isDefending = false;
  }

  public void Clash() {
    isClashing = true;
  }

  void DropParry() {
    body.velocity = Vector2.zero;

    isClashing = false;
    isParrying = false;
  }

  public void OnGUI() {
    string guiLabel = showDebug ? "HP: " + currentHP + "\n" +
                      "Defending: " + isDefending + "\n" +
                      "Parrying: " + isParrying + "\n" +
                      "Clashing: " + isClashing + "\n" +
                      "Running: " + isRunning + "\n" +
                      "Grounded: " + isGrounded + "\n" +
                      "Falling: " + isFalling + "\n" +
                      "Jumping: " + isJumping + "\n" +
                      // "Gliding: " + isGliding + "\n" +
                      // "JetpackUp: " + isJetpackUp + "\n" +
                      // "JetpackHorizontal: " + (jetpackHorizontal != "" ? jetpackHorizontal : "none") + "\n" +
                      "horizontalCollision: " + horizontalCollision + "\n" +
                      // "Equipment: " + currentWeapon + "\n" +
                      "Attack_Single: " + isAttackingSingle + "\n" +
                      "Attack_Heavy: " + isAttackingHeavy + "\n" +
                      "Air_Attack_Single: " + isAirAttackSingle + "\n" +
                      "Air_Attack_Heavy: " + isAirAttackHeavy + "\n" +
                      // "Air_Shooting: " + isAirShooting + "\n" +
                      "Kick: " + isKicking + "\n" +
                      "Drop_Kick: " + isDropKicking + "\n" +
                      "Punching: " + isPunching + "\n" +
                      "Air_Punch: " + isAirPunching + "\n" +
                      "Throwing: " + (isThrowing > 0) + "\n" +
                      // "Shooting: " + (isShootingSingle || isShootingAuto || isShootingPull || isAirShooting) + "\n" +
                      "Shooting: " + isShootingPull + "\n" +
                      "Shield HP: " + currentShieldHP + "\n" : "";
    GUI.Label(new Rect(0, 0, 200, 400), guiLabel);
  }

  private void Fall() {
    isGrounded = false;
    isFalling = true;
    // TESTING FOR PROGRAMMATIC PLAY
    // anim.Play("falling-1", -1, 0f);
    DropDefense();
  }

  public void Jump(bool clearDropKick = false) {
    if (clearDropKick) {
      isDropKicking = false;
      isFalling = false;
      canFlipOnAir = true;
      weaponCollider.SetActive(false);
    }

    body.velocity = new Vector2(body.velocity.x, jumpHeight);

    isJumping = true;
    // TESTING FOR PROGRAMMATIC PLAY
    // anim.Play("jumping-1", -1, 0f);
    isGrounded = false;
  }

  // private void Glide() {
  //   isGliding = true;
  // }

  // private void JetpackUp() {
  //   body.velocity = new Vector2(body.velocity.x, jetpackHeight);
  //   isJetpackUp = true;
  //   isJumping = false;
  //   isGrounded = false;
  // }

  // private void JetpackHorizontal(string direction) {
  //   jetpackHorizontal = direction;
  //   jetpackTime = Time.time * 1000;
  //   currentYPosition = transform.position.y;
  //   isJumping = false;
  //   isGrounded = false;
  // }

  private void DropKick() {
    canFlipOnAir = false;
    isDropKicking = true;
    weaponCollider.SetActive(true);
  }

  private void OnCollisionEnter2D(Collision2D col) {
    Collider2D collider = col.collider;
    Collider2D otherCollider = col.otherCollider;
    GameObject objectCollided = col.gameObject;

    if (Helpers.IsValueInArray(Constants.landingObjects, objectCollided.tag)) {
      if (otherCollider.tag == "Hero") {
        if (!isHorizontalCollision(otherCollider, collider)) {
          isGrounded = true;
          isFalling = false;
          isJumping = false;
          // isJetpackUp = false;
          horizontalCollision = false;
          isDropKicking = false;

          if (isHurt == 3) {
            Recover();
          }

          // disable air attack animations if these haven't finished when player hits ground
          isAirPunching = false;
          // isAirShooting = false;
          isAirAttackSingle = false;
          isAirAttackHeavy = false;

          weaponCollider.SetActive(false);
        } else {
          horizontalCollision = true;

          if (isBottomCollision(otherCollider, collider)) {
            horizontalCollision = false;
            ClearAirAttackSingle();
          }
        }
      }
    }

    // TODO: test further to understand better
    if (col.collider.tag == "Breakable") {
      // if (isGrounded) {
        Physics2D.IgnoreCollision(col.collider, GetComponent<BoxCollider2D>());
      // } else {
      //   isGrounded = true;
      // }
    }

    if (objectCollided.tag != "Item") {
      collisionCounter++;
    }
  }

  public void ReceiveThrowable(GameObject throwable, Vector2 contactPoint) {
    float currentX = transform.position.x;
    float throwableX = throwable.transform.position.x;

    hurtFromBehind = (currentX < throwableX && isFacingLeft) || (currentX > throwableX && !isFacingLeft);
    bool mustTakeDamage = (!isDefending || (isDefending && hurtFromBehind)) && (!isParrying || (isParrying && hurtFromBehind));

    if (hurtFromBehind) {
      FlipPlayer(true);
    }

    int throwableDamage = Damages.weaponDamages[throwable.GetComponent<Throwable>().type].damage;

    if (mustTakeDamage) {
      int damage = (stamina + (int)equippedSTA) - throwableDamage;
      TakeDamage(damage < 0 ? Math.Abs(damage) : Constants.minimumDamageDealt, contactPoint);

      if (currentHP > 0) {
        PlayerHurt(isGrounded ? 2 : 3);
      } else {
        PlayerDying(isGrounded);
      }
    } else {
      if (isDefending) {
        int shieldDefense = armUsed == 1 ? equippedDEF1 : equippedDEF2;

        if (throwableDamage <= shieldDefense) {
          currentShieldHP--;
        } else {
          DropDefense();
          currentShieldHP--;
          int damage = (stamina + (int)equippedSTA + shieldDefense) - throwableDamage;
          TakeDamage(damage < 0 ? Math.Abs(damage) :  Constants.minimumDamageDealt, contactPoint);

          if (currentHP > 0) {
            PlayerHurt(isGrounded ? 2 : 3);
          } else {
            PlayerDying(isGrounded);
          }
        }
      }

      if (currentShieldHP == 0) {
        shieldDropTime = Time.time * 1000;
        DropDefense();
      }
      if (isParrying) {
        Clash();
      }
    }
  }

  public void ReceiveAttack(GameObject enemy, Vector2 contactPoint) {
    Enemy enemyScript = enemy.GetComponent<Enemy>();

    if (enemyScript.isAttacking) {
      float currentX = transform.position.x;
      float enemyX = enemyScript.transform.position.x;

      hurtFromBehind = (currentX < enemyX && isFacingLeft) || (currentX > enemyX && !isFacingLeft);

      bool mustTakeDamage = (!isDefending || (isDefending && hurtFromBehind)) && (!isParrying || (isParrying && hurtFromBehind));

      if (hurtFromBehind) {
        FlipPlayer(true);
      }

      if (mustTakeDamage) {
        bool isCritical = Helpers.IsCritical(enemyScript.criticalRate);
        int damage = (stamina + (int)equippedSTA) - (enemyScript.atk * (isCritical ? 2 : 1));
        TakeDamage(damage < 0 ? Math.Abs(damage) : Constants.minimumDamageDealt, contactPoint, isCritical);

        if (currentHP > 0) {
          PlayerHurt(isGrounded ? 2 : 3);
        } else {
          PlayerDying(isGrounded);
        }
      } else {
        if (isDefending) {
          int shieldDefense = armUsed == 1 ? equippedDEF1 : equippedDEF2;

          if (enemyScript.atk <= shieldDefense) {
            currentShieldHP--;
          } else {
            DropDefense();
            currentShieldHP--;
            bool isCritical = Helpers.IsCritical(enemyScript.criticalRate);
            int damage = (stamina + (int)equippedSTA + shieldDefense) - (enemyScript.atk * (isCritical ? 2 : 1));
            TakeDamage(damage < 0 ? Math.Abs(damage) :  Constants.minimumDamageDealt, contactPoint, isCritical);

            if (currentHP > 0) {
              PlayerHurt(isGrounded ? 2 : 3);
            } else {
              PlayerDying(isGrounded);
            }
          }
        }

        if (currentShieldHP == 0) {
          shieldDropTime = Time.time * 1000;
          DropDefense();
        }
        if (isParrying) {
          Clash();
          enemyScript.stunOnAttack = true;
        }
      }
    }
  }

  public void ReceiveSmashWave(int damage, Vector2? damagePosition = null) {
    int damageReceived = (stamina + (int)equippedSTA) - damage;
    int actualDamage = damageReceived < 0 ? Math.Abs(damageReceived) : Constants.minimumDamageDealt;
    if (currentHP > actualDamage) {
      TakeDamage(actualDamage, damagePosition);
      PlayerHurt(3);
    } else {
      PlayerDying(false);
    }
  }

  public void TakeDamage(int damage, Vector2? damagePosition = null, bool? isCritical = false) {
    currentHP -= damage;

    if (currentHP < 0) {
      currentHP = 0;
    }

    GameObject barDecrement = Instantiate(Objects.prefabs["bar-decrement"], Vector2.zero, Quaternion.identity);
    barDecrement.transform.SetParent(hpBarContainer.transform, false);
    barDecrement.GetComponent<BarDecrement>().width = maxHP > Constants.maxHPDisplayableLimit ? (int)(Constants.hpContainerMaxWidth * ((float)damage/(float)maxHP)) : damage;
    barDecrement.GetComponent<BarDecrement>().type = "hp";

    if (Settings.showDamage) {
      // TODO: consider changing the Hero colliders for damage so the connecting point is higher
      Vector2 position = damagePosition == null ? new Vector2(transform.position.x, transform.position.y + heroHeight / 2) : new Vector2(damagePosition.Value.x, damagePosition.Value.y + heroHeight / 2);

      GameObject damageObject = Instantiate(Objects.prefabs["damage-container"], position, Quaternion.identity);
      damageObject.transform.SetParent(null);
      damageObject.GetComponent<DamageContainer>().damage = damage;
      damageObject.GetComponent<DamageContainer>().isCritical = isCritical ?? false;
    }

    // TODO: for testing purposes. Remove once magic can be spent by other means
    SpendMagic(damage);
  }

  public void SpendMagic(int value) {
    currentMP -= value;

    if (currentMP < 0) {
      currentMP = 0;
    }

    GameObject barDecrement = Instantiate(Objects.prefabs["bar-decrement"], Vector2.zero, Quaternion.identity);
    barDecrement.transform.SetParent(mpBarContainer.transform, false);
    barDecrement.GetComponent<BarDecrement>().width = maxMP > Constants.maxMPDisplayableLimit ? (int)(Constants.mpContainerMaxWidth * ((float)value/(float)maxMP)) : value;
    barDecrement.GetComponent<BarDecrement>().type = "mp";
  }

  private bool isBottomCollision(Collider2D collider1, Collider2D collider2) {
    int c1BottomEdge = (int) collider1.bounds.max.y;
    int c2TopEdge = (int) collider2.bounds.min.y;

    return c1BottomEdge == c2TopEdge;
  }

  private bool isHorizontalCollision(Collider2D collider1, Collider2D collider2) {
    int c1RightEdge = (int) collider1.bounds.max.x;
    int c1LeftEdge = (int) collider1.bounds.min.x;

    int c2RightEdge = (int) collider2.bounds.max.x;
    int c2LeftEdge = (int) collider2.bounds.min.x;

    return (c1RightEdge == c2LeftEdge) || (c1LeftEdge == c2RightEdge);
  }

  private void OnCollisionExit2D(Collision2D collision) {
    if (collision.gameObject.tag != "Item") {
      collisionCounter--;
    }

    if (collisionCounter == 0) {
      isGrounded = false;
      DropDefense();
    }
  }

  public void CheckLevel() {
    if (exp >= next) {
      LevelUp();
    }
  }

  public void LevelUp() {
    // TODO: update stats here

    playerLevel++;
    next = Helpers.NextLevelEXP(playerLevel + 1);
    levelUpCanvas.SetActive(true);
    SetPauseCase("level-up");
    levelUpCanvas.GetComponent<LevelUpCanvas>().ShowLevelUp();
  }

  public void PlayerDeath() {
    SetPauseCase("death");
    fadeOutCanvas.SetActive(true);
  }

  public void SetPauseCase(string newPauseCase) {
    pauseCase = newPauseCase;
    Time.timeScale = 0;
  }

  public void ClearPauseCase() {
    pauseCase = "";
    Time.timeScale = 1;
  }
}
