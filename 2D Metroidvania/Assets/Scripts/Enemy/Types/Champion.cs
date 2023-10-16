using UnityEngine;

public class Champion : MonoBehaviour {
  // Components
    Enemy enemy;

  // Raycast properties
    public float forwardCastLength = 5f;
    public float playerCastLength = 0.1f;

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
                Vector2 beginEdgeCast = new Vector2(transform.position.x + ((enemy.enemyWidth / 2) * direction), transform.position.y + enemy.enemyHeight / 4);
                Vector2 edgeCastDirection = transform.TransformDirection(new Vector2(direction * 2, -1));

                RaycastHit2D edgeCast = Physics2D.Raycast(beginEdgeCast, edgeCastDirection, enemy.edgeCastLength / 2);
                Debug.DrawRay(beginEdgeCast, edgeCastDirection.normalized * enemy.edgeCastLength, Colors.raycastColors["edge"]);

                if (edgeCast.collider && edgeCast.collider.name.Contains("EnemyFlipper")) {
                  enemy.isFacingLeft = !enemy.isFacingLeft;
                  if (enemy.playerFound) {
                    enemy.playerFound = false;
                  }
                }

              // FOUND PLAYER
                if (!enemy.playerFound) {
                  RaycastHit2D searchCast = Physics2D.Raycast(beginForwardCast, forwardCastDirection, forwardCastLength);
                  Debug.DrawRay(beginForwardCast, forwardCastDirection.normalized * forwardCastLength, Colors.raycastColors["search"]);

                  // PLAYER NEARBY
                  if (searchCast && searchCast.collider.tag == "Hero") {
                    enemy.playerFound = true;

                    if (enemy.level >= 10 && !enemy.isDefending) {
                      enemy.isThrowingWeapon = true;
                    }
                  } else {
                    if (wanderStart == 0) {
                      if (enemy.level >= 50) {
                        enemy.isSummoning = true;
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
                  Vector2 beginPlayerCast = new Vector2(transform.position.x + ((enemy.enemyWidth * enemy.reach) * direction), transform.position.y + enemy.enemyHeight / 2);

                  RaycastHit2D playerCast = Physics2D.Raycast(beginPlayerCast, forwardCastDirection, playerCastLength);
                  Debug.DrawRay(beginPlayerCast, forwardCastDirection.normalized * playerCastLength, Colors.raycastColors["player"]);

                  // ATTACK
                    if (playerCast && playerCast.collider.tag == "Hero") {
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
