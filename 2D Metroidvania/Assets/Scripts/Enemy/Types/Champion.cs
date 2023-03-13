using UnityEngine;

public class Champion : MonoBehaviour {
  // Components
    Enemy enemy;

  // Raycast properties
    public float diagonalForwardCastLength = 1f;
    public float forwardCastLength = 5f;
    public float proximityCastLength = 0.1f;

  // Champion properties
    public float wanderStart = 0;
    public float wanderTime = 5000;

  void Start() {
    enemy = GetComponent<Enemy>();
  }

  void Update() {
    if (!enemy.hero.isAutonomous && ((gameObject.name == "Boss" && enemy.isOnCamera) || gameObject.name != "Boss")) {
      if (enemy.hero != null && enemy.hero.pauseCase == "") {
        // CHAMPION MOVEMENT
          if (!enemy.needsCoolDown) {
            if (enemy.isWalking && !enemy.isAttacking) {
              int direction = enemy.isFacingLeft ? -1 : 1;

              // MOVEMENT
                if (enemy.ShouldMove()) {
                  enemy.body.velocity = new Vector2(direction * enemy.speed, enemy.body.velocity.y);
                } else {
                  enemy.body.velocity = Vector2.zero;
                }

              Vector2 beginForwardCast = new Vector2(transform.position.x + ((enemy.enemyWidth / 2) * direction), transform.position.y + enemy.enemyHeight / 2);
              Vector2 forwardCastDirection = transform.TransformDirection(new Vector2(1 * (direction), 0));

              // FOUND EDGE
                Vector2 beginDiagonalForwardCast = new Vector2(transform.position.x + ((enemy.enemyWidth / 2) * direction), transform.position.y + enemy.enemyHeight / 4);
                Vector2 diagonalForwardCastDirection = transform.TransformDirection(new Vector2(0, -1));

                RaycastHit2D diagonalForwardCast = Physics2D.Raycast(beginDiagonalForwardCast, diagonalForwardCastDirection, diagonalForwardCastLength);
                Debug.DrawRay(beginDiagonalForwardCast, diagonalForwardCastDirection.normalized * diagonalForwardCastLength, Color.green);

                if (!diagonalForwardCast) {
                  enemy.isFacingLeft = !enemy.isFacingLeft;
                  if (enemy.playerFound) {
                    enemy.playerFound = false;
                  }
                }

              // FOUND PLAYER
                if (!enemy.playerFound) {
                  RaycastHit2D forwardCast = Physics2D.Raycast(beginForwardCast, forwardCastDirection, forwardCastLength);
                  Debug.DrawRay(beginForwardCast, forwardCastDirection.normalized * forwardCastLength, Color.red);

                  // PLAYER NEARBY
                  if (forwardCast && forwardCast.collider.tag == "Hero") {
                    enemy.playerFound = true;

                    if (enemy.level >= 10 && !enemy.isDefending) {
                      enemy.isThrowingWeapon = true;
                      enemy.distanceToPlayer = forwardCast.distance;
                    }
                  } else {
                    if (wanderStart == 0) {
                      if (enemy.level >= 50) {
                        // enemy.isSummoning = true;
                      }
                      wanderStart = Time.time * 1000;
                      enemy.isFacingLeft = UnityEngine.Random.Range(0, 2) != 0;
                    } else {
                      if (Helpers.ExceedsTime(wanderStart, wanderTime)) {
                        wanderStart = 0;
                      }
                    }
                  }
                } else {
                  Vector2 beginProximityCast = new Vector2(transform.position.x + ((enemy.enemyWidth * enemy.reach) * direction), transform.position.y + enemy.enemyHeight / 2);

                  RaycastHit2D proximityCast = Physics2D.Raycast(beginProximityCast, forwardCastDirection, proximityCastLength);
                  Debug.DrawRay(beginProximityCast, forwardCastDirection.normalized * proximityCastLength, Color.magenta);

                  // ATTACK
                    if (proximityCast && proximityCast.collider.tag == "Hero") {
                      enemy.isAttacking = true;
                      enemy.body.velocity = Vector2.zero;
                    }
                }
            }
        } else {
          if (Helpers.ExceedsTime(enemy.coolDownStart, enemy.coolDownTime)) {
            enemy.coolDownStart = 0;
            enemy.needsCoolDown = false;
            enemy.playerFound = false;
          }
        }
      }
    }
  }
}
