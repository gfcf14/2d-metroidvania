using UnityEngine;

public class Hero : MonoBehaviour {
  [SerializeField] public float speed;
  [SerializeField] private float jumpHeight;
  [SerializeField] private float jetpackHeight;
  private Rigidbody2D body;
  private Animator anim;
  private SpriteRenderer heroRenderer;

  private float heroHeight;
  private float heroWidth;

  public bool isRunning;
  public bool isGrounded;
  public bool canFlipOnAir;
  public bool isFalling;
  public bool isJumping;

  public bool isJetpackUp;

  public string jetpackHorizontal = "";
  public float maxJetpackTime = 1500;
  public float jetpackTime = 0;

  public float currentYPosition = 0;
  public float currentXPosition = 0;

  public float throwbackHeight = 0;

  public int isHurt = 0;
  public int isDead = 0;

  public bool isDefending = false;
  public bool isParrying = false;
  public bool isClashing = false;

  public bool isGliding;
  public bool isFacingLeft;

  public bool isAttackingSingle;
  public bool isAttackingHeavy;

  public bool isAirAttackSingle;
  public bool isAirAttackHeavy;

  public bool isKicking;
  public bool isDropKicking;

  public bool isPunching;
  public bool isAirPunching;
  public bool isAirShooting;

  public bool isThrowing;
  public bool startThrow = false;

  public bool isShootingSingle;

  public bool isShootingAuto;

  public bool isShootingPull;

  public bool horizontalCollision;

  public int collisionCounter = 0;

  public float horizontalInput = 0;

  public string[] weapons = new string[] {"fists", "single", "heavy", "throwables", "projectile-single", "projectile-heavy", "projectile-auto", "projectile-pull"};

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

  [System.NonSerialized] public string playerEquipment = "equipment-1";
  [System.NonSerialized] public int playerLevel = 1;
  [System.NonSerialized] public int currentHP = 80;
  [System.NonSerialized] public int maxHP = 100;
  [System.NonSerialized] public int currentMP = 10;
  [System.NonSerialized] public int maxMP = 20;
  [System.NonSerialized] public string status = "good";
  [System.NonSerialized] public int exp = 343;
  [System.NonSerialized] public int next = 350;
  [System.NonSerialized] public int gold = 650;
  [System.NonSerialized] public int atk1 = 5;
  [System.NonSerialized] public int atk2 = 10;
  [System.NonSerialized] public int def1 = 5;
  [System.NonSerialized] public int def2 = 10;
  [System.NonSerialized] public float luckPercentage = 0.2f;
  [System.NonSerialized] public float criticalPercentage = 0.5f;
  [System.NonSerialized] public string location = "meadows";
  [System.NonSerialized] public string[] magicResistances = new string[] {"earth", "air", "water", "fire", "lightning", "ice", "light", "dark"};
  // [System.NonSerialized] public string[] magicResistances = new string[] {"lightning", "dark", "earth"};
  // [System.NonSerialized] public string[] magicResistances = new string[] {};

  public int tiredThreshold = 40;

  private int maxShieldHP = 0;
  private int currentShieldHP = 0;
  private float currentShieldRecoverTime = 0;
  private float shieldDropTime = 0;

  // TODO: remove these variables and add a new dictionary when adding another shield sprite
  public int dummyShieldHP = 5;
  public float dummyShieldRecoverTime = 2000;

  private GameObject currentArrow;
  private GameObject arrowAnchor;
  private Arrow arrowInstance;
  private GameObject arrowMask;

  public bool isPaused;
  [SerializeField] GameObject pauseCanvas;

  [System.NonSerialized] bool canMap = false;

  // called when script is loaded
  private void Awake() {
    body = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    heroRenderer = GetComponent<SpriteRenderer>();

    currentWeapon = weapons[weaponIndex % weapons.Length];

    heroHeight = heroRenderer.bounds.size.y;
    heroWidth = heroRenderer.bounds.size.x;

    // TODO: move this to shield equipment change once equipment options are available
    maxShieldHP = dummyShieldHP;
    currentShieldHP = maxShieldHP;
    currentShieldRecoverTime = dummyShieldRecoverTime;

    arrowAnchor = transform.Find("ArrowAnchor").gameObject;
  }

  // called on every frame of the game
  private void Update() {
    horizontalInput = Input.GetAxis("Horizontal");
    float verticalSpeed = body.velocity.y;

    if (shieldDropTime != 0) {
      float currentTime = Time.time * 1000;

      if (currentTime > (shieldDropTime + currentShieldRecoverTime)) {
        currentShieldHP = maxShieldHP;
        shieldDropTime = 0;
      }
    }

    // x axis movement
    if (!horizontalCollision && isHurt < 1) {
      if (!isDefending && !isParrying && !isClashing && !isThrowing) {
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

    if (isHurt == 2 && isGrounded) {
      if (hurtFromBehind) {
        transform.position = new Vector2(transform.position.x + (isFacingLeft ? -1 : 1) * 0.01f, currentYPosition);
      } else {
        transform.position = new Vector2(transform.position.x + (isFacingLeft ? 1 : -1) * 0.01f, currentYPosition);
      }
    }

    if (isHurt == 3) {
      if (throwbackHeight != 0 && transform.position.y > (currentYPosition - throwbackHeight)) {
        transform.position = new Vector2(transform.position.x + ((isFacingLeft ? 1 : -1) * 0.05f), transform.position.y + 0.05f);
      } else {
        throwbackHeight = 0;
        transform.position = new Vector2(transform.position.x + ((isFacingLeft ? 1 : -1) * 0.05f), transform.position.y - 0.05f);
      }
    }

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

    // Debug.Log(KeyCode.Space);

    // jumping
    // if (Input.GetKey(KeyCode.Space)) {
    // if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), Controls.currentJump))) {
    if (Helpers.IsKeyHeld(Controls.currentKeyboardJump) || Helpers.IsKeyHeld(Controls.currentGamepadJump)) {
      userInput += "$";
      if (isGrounded) {
        if (userInput.Contains(jetpackUp)) {
          JetpackUp();
          userInput = "";
        } else {
          Jump();
          userInput = "";
        }
      } else {
        if (userInput.Contains(jetpackLeft)) {
          JetpackHorizontal("left");
          userInput = "";
        } else if (userInput.Contains(jetpackRight)) {
          JetpackHorizontal("right");
          userInput = "";
        }
      }
    }

    // gliding
    if (Input.GetKey(KeyCode.UpArrow)) {
      if (!isGrounded) {
        if (Input.GetKey(KeyCode.Space)) {
          Glide();
        } else {
          isGliding = false;
        }
      }
    } else {
      isGliding = false;
    }

    isRunning = horizontalInput != 0 && !isJumping && !isFalling && !isAttackingSingle && !isJetpackUp;

    if (!isGrounded && verticalSpeed < -1 && jetpackHorizontal == "") {
      Fall();
    }

    if (Input.GetKey(KeyCode.Keypad4) && currentWeapon == "projectile-auto") {
      isShootingAuto = true;
    }

    if (Input.GetKeyUp(KeyCode.Keypad4) && isShootingAuto) {
      isShootingAuto = false;
    }

    if (Input.GetKeyDown(KeyCode.Keypad4)) {
      if (isGrounded) {
        if (currentWeapon == "fists" || currentWeapon == "projectile-single") {
          isPunching = true;
        } else if (currentWeapon == "single") {
          isAttackingSingle = true;
        } else if (currentWeapon == "heavy") {
          isAttackingHeavy = true;
        } else if (currentWeapon == "throwables") {
          isThrowing = true;
        } else if (currentWeapon == "projectile-heavy") {
          isShootingSingle = true;
        } else if (currentWeapon == "projectile-pull") {
          isShootingPull = true;
        }
      } else if (isJumping || isFalling) {
        if (currentWeapon == "fists") {
          isAirPunching = true;
        } else if (currentWeapon == "single") {
          isAirAttackSingle = true;
        } else if (currentWeapon == "heavy") {
          isAirAttackHeavy = true;
        } else if (currentWeapon == "projectile-single") {
          isAirShooting = true;
        }
      }
    }

    if (Input.GetKeyDown(KeyCode.Keypad5)) {
      if (isGrounded && !isRunning) {
        isKicking = true;
      } else if (isJumping && !isFalling) {
        DropKick();
      }
    }

    if (Input.GetKeyDown(KeyCode.RightControl)) {
      weaponIndex++;
      currentWeapon = weapons[weaponIndex % weapons.Length];
    }

    if (Input.GetKeyDown(KeyCode.Keypad7)) {
      SimulateHurt(1);
    }

    if (Input.GetKeyDown(KeyCode.Keypad8) && isGrounded) {
      SimulateHurt(2);
    }

    if (Input.GetKeyDown(KeyCode.Keypad9)) {
      SimulateHurt(3);
    }

    if (Input.GetKeyDown(KeyCode.Keypad3)) {
      if (currentShieldHP > 0) {
        isDefending = true;
      }
    }

    if (Input.GetKeyUp(KeyCode.Keypad3)) {
      DropDefense();
    }

    if (Input.GetKeyDown(KeyCode.Keypad2) && weapons[weaponIndex] == "heavy") {
      isParrying = true;
    }

    if (Input.GetKeyUp(KeyCode.Keypad2)) {
      isParrying = false;
    }

    if (Input.GetKeyDown(KeyCode.Backspace)) {
      SimulateDeath(isGrounded);
    }

    if (Helpers.IsPauseKeyUp()) {
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

    if (isDropKicking) {
      body.velocity = new Vector2(body.velocity.x + (jumpHeight * (isFacingLeft ? -1 : 1)), -(float)(jumpHeight * 0.75));
    }

    if (isGliding) {
      body.velocity = new Vector2(body.velocity.x + (jumpHeight * (isFacingLeft ? -1 : 1)), -(float)(jumpHeight * 0.25));
    }

    if (jetpackHorizontal != "") {
      body.velocity = new Vector2(body.velocity.x + (jetpackHeight * (jetpackHorizontal == "left" ? -1 : 1)), body.velocity.y);
      transform.position = new Vector2(transform.position.x, currentYPosition);
      if ((Time.time * 1000) > jetpackTime + maxJetpackTime) {
        jetpackHorizontal = "";
        jetpackTime = 0;
        body.velocity = new Vector2(0, 0);
      }
    }

    if (isDead == 2) {
      if (!isGrounded) {
        body.velocity = new Vector2(-body.velocity.x + (jumpHeight * (isFacingLeft ? 2 : -2)), -(float)jumpHeight);
      } else {
        body.velocity = new Vector2(0, 0);
      }
    }

    // set animator parameters
    anim.SetBool("isRunning", isRunning);
    anim.SetBool("isGrounded", isGrounded);
    anim.SetBool("isFalling", isFalling);
    anim.SetBool("isJumping", isJumping);
    anim.SetBool("isJetpackUp", isJetpackUp);
    anim.SetBool("horizontalCollision", horizontalCollision);
    anim.SetBool("isAttackingSingle", isAttackingSingle);
    anim.SetBool("isAirAttackSingle", isAirAttackSingle);
    anim.SetBool("isAirAttackHeavy", isAirAttackHeavy);
    anim.SetBool("isKicking", isKicking);
    anim.SetBool("isDropKicking", isDropKicking);
    anim.SetBool("isPunching", isPunching);
    anim.SetBool("isAirPunching", isAirPunching);
    anim.SetBool("isThrowing", isThrowing);
    anim.SetBool("isShootingSingle", isShootingSingle);
    anim.SetBool("isShootingAuto", isShootingAuto);
    anim.SetBool("isShootingPull", isShootingPull);
    anim.SetBool("isAirShooting", isAirShooting);
    anim.SetBool("isAttackingHeavy", isAttackingHeavy);
    anim.SetBool("isJetpackHorizontal", jetpackHorizontal != "");
    anim.SetBool("isGliding", isGliding);
    anim.SetBool("isTired", currentHP < tiredThreshold);
    anim.SetInteger("isHurt", isHurt);
    anim.SetInteger("isDead", isDead);
    anim.SetBool("isDefending", isDefending);
    anim.SetBool("isParrying", isParrying);
    anim.SetBool("isClashing", isClashing);
  }

  void FlipPlayer(bool hasBeenHurt = false) {
    if (hasBeenHurt && isFacingLeft) {
      transform.localScale = Vector3.one;
    } else {
      transform.localScale = new Vector3(-1, 1, 1);
    }
  }

  void SimulateDeath(bool isGrounded) {
    isDead = isGrounded ? 1 : 2;
  }

  void SimulateHurt(int hurtLevel) {
    body.velocity = new Vector2(0, 0);
    isHurt = hurtLevel;

    if (hurtLevel > 1) {
      currentXPosition = transform.position.x;
      currentYPosition = transform.position.y;
    }

    if (hurtLevel == 3) {
      throwbackHeight = 15f;
    }
  }

  void StartThrow() {
    // TODO: use equipped weapon instead, once equipment screen and options are prepared
    // current test throwables: lance, bomb, knife, kunai, shuriken-4, shuriken-6, hatchet, axe
    string throwableType = "lance";

    ThrowableObject currentThrowable = Objects.throwableObjects[throwableType];
    float throwableX = transform.position.x + ((isFacingLeft ? -1 : 1) * heroWidth * currentThrowable.startX);
    float throwableY = transform.position.y + (heroHeight * currentThrowable.startY);

    GameObject throwableWeapon = Instantiate(Objects.prefabs["throwable"], new Vector3(throwableX, throwableY, 0), Quaternion.identity);
    Throwable throwableInstance = throwableWeapon.GetComponent<Throwable>();

    throwableInstance.isFacingLeft = isFacingLeft;
    throwableInstance.maxDistance = currentThrowable.maxDistance;
    throwableInstance.startX = throwableX;
    throwableInstance.startY = throwableY;
    throwableInstance.type = throwableType;
  }

  void Recover() {
    isHurt = 0;
  }

  void ClearPunch() {
    isPunching = false;
  }

  void ClearAirPunch() {
    isAirPunching = false;
  }

  void ClearAttackSingle() {
    isAttackingSingle = false;
  }

  void ClearAirAttackSingle() {
    isAirAttackSingle = false;
  }

  void ClearAirAttackHeavy() {
    isAirAttackHeavy = false;
  }

  void ClearAirShooting() {
    isAirShooting = false;
  }

  void ClearKick() {
    isKicking = false;
  }

  void ClearThrow() {
    isThrowing = false;
  }

  void ClearShootingSingle() {
    isShootingSingle = false;
  }

  void CreateArrow() {
    currentArrow = Instantiate(Objects.prefabs["arrow"], arrowAnchor.transform.position, Quaternion.identity);
    arrowInstance = currentArrow.GetComponent<Arrow>();
    arrowMask = currentArrow.transform.Find("Mask").gameObject;

    arrowInstance.type = "arrow-poison";
  }

  void ShowCurrentArrowMask() {
    arrowMask.GetComponent<SpriteMask>().enabled = true;
  }

  void FireArrow() {
    Destroy(arrowMask);
    arrowInstance.hasFired = true;
  }

  void ClearShootingPull() {
    isShootingPull = false;
  }

  void ClearAttackHeavy() {
    isAttackingHeavy = false;
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
    string guiLabel = "HP: " + currentHP + "\n" +
                      "Defending: " + isDefending + "\n" +
                      "Parrying: " + isParrying + "\n" +
                      "Clashing: " + isClashing + "\n" +
                      "Running: " + isRunning + "\n" +
                      "Grounded: " + isGrounded + "\n" +
                      "Falling: " + isFalling + "\n" +
                      "Jumping: " + isJumping + "\n" +
                      "Gliding: " + isGliding + "\n" +
                      "JetpackUp: " + isJetpackUp + "\n" +
                      "JetpackHorizontal: " + (jetpackHorizontal != "" ? jetpackHorizontal : "none") + "\n" +
                      "horizontalCollision: " + horizontalCollision + "\n" +
                      "Equipment: " + currentWeapon + "\n" +
                      "Attack_Single: " + isAttackingSingle + "\n" +
                      "Attack_Heavy: " + isAttackingHeavy + "\n" +
                      "Air_Attack_Single: " + isAirAttackSingle + "\n" +
                      "Air_Attack_Heavy: " + isAirAttackHeavy + "\n" +
                      "Air_Shooting: " + isAirShooting + "\n" +
                      "Kick: " + isKicking + "\n" +
                      "Drop_Kick: " + isDropKicking + "\n" +
                      "Punching: " + isPunching + "\n" +
                      "Air_Punch: " + isAirPunching + "\n" +
                      "Throwing: " + isThrowing + "\n" +
                      "Shooting: " + (isShootingSingle || isShootingAuto || isShootingPull || isAirShooting) + "\n";
    GUI.Label(new Rect(0, 0, 200, 400), guiLabel);
  }

  private void Fall() {
    isGrounded = false;
    isFalling = true;
    DropDefense();
  }

  public void Jump(bool clearDropKick = false) {
    if (clearDropKick) {
      isDropKicking = false;
      isFalling = false;
      canFlipOnAir = true;
    }

    body.velocity = new Vector2(body.velocity.x, jumpHeight);

    isJumping = true;
    isGrounded = false;
  }

  private void Glide() {
    isGliding = true;
  }

  private void JetpackUp() {
    body.velocity = new Vector2(body.velocity.x, jetpackHeight);
    isJetpackUp = true;
    isJumping = false;
    isGrounded = false;
  }

  private void JetpackHorizontal(string direction) {
    jetpackHorizontal = direction;
    jetpackTime = Time.time * 1000;
    currentYPosition = transform.position.y;
    isJumping = false;
    isGrounded = false;
  }

  private void DropKick() {
    canFlipOnAir = false;
    isDropKicking = true;
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    Collider2D collider = collision.collider;
    Collider2D otherCollider = collision.otherCollider;
    GameObject objectCollided = collision.gameObject;

    if (objectCollided.tag == "Ground") {
      if (otherCollider.tag == "Hero") {
        if (!isHorizontalCollision(otherCollider, collider)) {
          isGrounded = true;
          isFalling = false;
          isJumping = false;
          isJetpackUp = false;
          horizontalCollision = false;
          isDropKicking = false;

          if (isHurt == 3) {
            Recover();
          }

          // disable air attack animations if these haven't finished when player hits ground
          isAirPunching = false;
          isAirShooting = false;
          isAirAttackSingle = false;
          isAirAttackHeavy = false;
        } else {
          horizontalCollision = true;

          if (isBottomCollision(otherCollider, collider)) {
            horizontalCollision = false;
            ClearAirAttackSingle();
          }
        }
      }
    }

    if (objectCollided.tag == "Enemy") {
      Patroller enemyCollided = objectCollided.GetComponent<Patroller>();

      if (enemyCollided.isAttacking) {
        float currentX = transform.position.x;
        float enemyX = enemyCollided.transform.position.x;

        hurtFromBehind = (currentX < enemyX && isFacingLeft) || (currentX > enemyX && !isFacingLeft);

        bool mustTakeDamage = (!isDefending || (isDefending && hurtFromBehind)) && (!isParrying || (isParrying && hurtFromBehind));

        if (hurtFromBehind) {
          FlipPlayer(true);
        }

        if (mustTakeDamage) {
          currentHP -= enemyCollided.standardDamage;

          if (currentHP > 0) {
            SimulateHurt(2);
          } else {
            SimulateDeath(isGrounded);
          }
        } else {
          currentShieldHP--;

          if (currentShieldHP == 0) {
            shieldDropTime = Time.time * 1000;
            DropDefense();
          }
          if (isParrying) {
            Clash();
            enemyCollided.stunOnAttack = true;
          }
        }
      }
    }

    collisionCounter++;
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
    collisionCounter--;

    if (collisionCounter == 0) {
      isGrounded = false;
      DropDefense();
    }
  }
}
