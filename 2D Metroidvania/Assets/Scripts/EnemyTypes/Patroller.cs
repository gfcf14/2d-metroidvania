using UnityEngine;

// Made by Bartha Szabolcs of GameDevJourney

public class Patroller : MonoBehaviour {
  // Components
    Enemy enemy;

  // Raycast properties
    public float diagonalForwardCastLength = 1f;
    public float forwardCastLength = 2f;
    public float proximityCastLength = 0.1f;

  // Player Related properties

  void Start() {
    enemy = GetComponent<Enemy>();
  }

  void Update() {
    if (enemy.hero != null && enemy.hero.pauseCase == "") {
      if (!enemy.isPoisoned && !enemy.isStunned) {
        enemy.enemyRenderer.color = enemy.enemyColor;
      }

      // heroIsDead = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().isDead != 0;

      if (enemy.isBurning) {
        enemy.enemyColor = Colors.statusColors["burned"];

        if (Helpers.ExceedsTime(enemy.burnTime, enemy.burningDuration)) {
          enemy.isBurning = false;
          enemy.isDeadByBurning = true;
        }
      }

      if (enemy.isPoisoned) {
        float currentTime = Time.time * 1000;
        float nextPoisonAttackTime = enemy.poisonTime + (enemy.poisonAttackInterval * enemy.poisonAttackCounter);

        if (currentTime > enemy.poisonEffectTime + enemy.poisonEffectDuration) {
          if (!enemy.isStunned) {
            enemy.enemyRenderer.color = enemy.enemyColor;
          }

          if (enemy.poisonAttackCounter == enemy.maxPoisonAttacks + 1) {
            enemy.isPoisoned = false;
            enemy.poisonAttackCounter = 0;
          }
        }

        if (currentTime > nextPoisonAttackTime)  {
          enemy.TakeDamage(Constants.arrowPoisonDamage);
          enemy.poisonEffectTime = Time.time * 1000;
          enemy.enemyRenderer.color = Colors.statusColors["poisoned"];
          enemy.poisonAttackCounter++;

          if (enemy.currentHP <= 0) {
            enemy.isDeadByPoison = true;
            enemy.isWalking = false;
            enemy.body.velocity = Vector2.zero;

            if (!enemy.isDead) { // avoids getting double exp if dying from poison after being attacked
              enemy.hero.exp += enemy.exp;
              enemy.hero.CheckLevel();
            }
          }
        }
      }

      if (enemy.isDead) {
        if (enemy.attackedFromBehind) {
          // transform.position = new Vector2(transform.position.x + (isFacingLeft ? -1 : 1) * 0.05f, transform.position.y + 0.02f);
          transform.position = new Vector3(enemy.deadPosition.x + enemy.deadAnimationIncrement * 0.025f * (enemy.isFacingLeft ? -1 : 1), enemy.deadPosition.y + 0.01f * enemy.deadAnimationIncrement);
        } else {
          // transform.position = new Vector2(transform.position.x + (isFacingLeft ? 1 : -1) * 0.05f, transform.position.y + 0.02f);
          transform.position = new Vector3(enemy.deadPosition.x + enemy.deadAnimationIncrement * 0.025f * (enemy.isFacingLeft ? 1 : -1), enemy.deadPosition.y + 0.01f * enemy.deadAnimationIncrement);
        }

        enemy.deadAnimationIncrement++;
      }

      if (!enemy.needsCoolDown) {
        if (enemy.isWalking && !enemy.isAttacking) {
          int direction = enemy.isFacingLeft ? -1 : 1;

          if (!enemy.isDead && !enemy.isDeadByBurning && !enemy.isDeadByPoison && !enemy.isBurning && !enemy.isStunned) {
            enemy.body.velocity = new Vector2(direction * enemy.speed, enemy.body.velocity.y);
          } else {
            enemy.body.velocity = Vector2.zero;
          }

          Vector2 beginDiagonalForwardCast = new Vector2(transform.position.x + ((enemy.enemyWidth / 2) * direction), transform.position.y + enemy.enemyHeight / 4);
          Vector2 diagonalForwardCastDirection = transform.TransformDirection(new Vector2(0, -1));

          RaycastHit2D diagonalForwardCast = Physics2D.Raycast(beginDiagonalForwardCast, diagonalForwardCastDirection, diagonalForwardCastLength);
          Debug.DrawRay(beginDiagonalForwardCast, diagonalForwardCastDirection.normalized * diagonalForwardCastLength, Color.green);

          // There's floor forward
          // if (!diagonalForwardCast && diagonalForwardCast.collider.tag == "Ground") {
          if (!diagonalForwardCast) {
            enemy.isFacingLeft = !enemy.isFacingLeft;
          }

          // if (!heroIsDead) {
            Vector2 beginForwardCast = new Vector2(transform.position.x + ((enemy.enemyWidth / 2) * direction), transform.position.y + enemy.enemyHeight / 2);
            Vector2 forwardCastDirection = transform.TransformDirection(new Vector2(1 * (direction), 0));

            if (!enemy.playerFound) {
              RaycastHit2D forwardCast = Physics2D.Raycast(beginForwardCast, forwardCastDirection, forwardCastLength);
              Debug.DrawRay(beginForwardCast, forwardCastDirection.normalized * forwardCastLength, Color.red);

              // Player is nearby
              if (forwardCast && forwardCast.collider.tag == "Hero") {
                enemy.playerFound = true;
              }
            } else {
              Vector2 beginProximityCast = new Vector2(transform.position.x + (enemy.enemyWidth * direction), transform.position.y + enemy.enemyHeight / 2);

              RaycastHit2D proximityCast = Physics2D.Raycast(beginProximityCast, forwardCastDirection, proximityCastLength);
              Debug.DrawRay(beginProximityCast, forwardCastDirection.normalized * proximityCastLength, Color.magenta);

              if (proximityCast && proximityCast.collider.tag == "Hero") {
                enemy.isAttacking = true;
                enemy.body.velocity = Vector2.zero;
              }
            }
          // }
        }
      } else {
        if (Helpers.ExceedsTime(enemy.coolDownStart, enemy.coolDownTime)) {
            enemy.coolDownStart = 0;
            enemy.needsCoolDown = false;
            enemy.playerFound = false;
          }
      }

      if (!enemy.isBurning) {
        if (enemy.isFacingLeft) {
          transform.localScale = new Vector3(-1, 1, 1);
        } else {
          transform.localScale = Vector3.one;
        }
      }
    }
  }
}
