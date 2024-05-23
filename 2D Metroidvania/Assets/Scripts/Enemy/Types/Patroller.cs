using UnityEngine;

// Made by Bartha Szabolcs of GameDevJourney

public class Patroller : MonoBehaviour {
  // Components
    Enemy enemy;

  // Raycast properties
    public float forwardCastLength = 2f;

  void Start() {
    enemy = GetComponent<Enemy>();
  }

  void Update() {
    if (enemy.hero != null && enemy.hero.pauseCase == "") {
      // PATROLLER MOVEMENT
        if (!enemy.needsCoolDown) {
          if (enemy.isWalking && !enemy.isAttacking) {
            int direction = enemy.isFacingLeft ? -1 : 1;

            // MOVEMENT
              if (enemy.ShouldMove()) {
                enemy.body.velocity = new Vector2(direction * enemy.speed, enemy.body.velocity.y);
              } else {
                enemy.body.velocity = Vector2.zero;
              }

            // There's floor forward
            // if (!diagonalForwardCast && diagonalForwardCast.collider.tag == "Floor") {
            // FOUND EDGE
              Vector2 beginEdgeCast = new Vector2(transform.position.x, transform.position.y);
              Vector2 edgeCastDirection = transform.TransformDirection(new Vector2(direction * 2, -1));

              RaycastHit2D edgeCast = Physics2D.Raycast(beginEdgeCast, edgeCastDirection, enemy.edgeCastLength);
              Debug.DrawRay(beginEdgeCast, edgeCastDirection.normalized * enemy.edgeCastLength, Colors.raycastColors["edge"]);

              if (edgeCast.collider && edgeCast.collider.name.Contains("EnemyFlipper")) {
                enemy.isFacingLeft = !enemy.isFacingLeft;
                if (enemy.playerFound) {
                  enemy.playerFound = false;
                }
              }

            // if (!heroIsDead) {
              Vector2 beginForwardCast = new Vector2(transform.position.x + ((enemy.enemyWidth / 2) * direction), transform.position.y + enemy.enemyHeight / 2);
              Vector2 forwardCastDirection = transform.TransformDirection(new Vector2(direction, 0));

            // FOUND PLAYER
              if (!enemy.playerFound) {
                RaycastHit2D searchCast = Physics2D.Raycast(beginForwardCast, forwardCastDirection, forwardCastLength);
                Debug.DrawRay(beginForwardCast, forwardCastDirection.normalized * forwardCastLength, Colors.raycastColors["search"]);

                // PLAYER NEARBY
                if (searchCast && searchCast.collider.tag == "Hero") {
                  enemy.playerFound = true;
                }
              } else {
                Vector2 beginProximityCast = new Vector2(transform.position.x, transform.position.y + enemy.enemyHeight / 2);

                RaycastHit2D playerCast = Physics2D.Raycast(beginProximityCast, forwardCastDirection, (enemy.enemyWidth / 2) + enemy.reach);
                Debug.DrawRay(beginProximityCast, forwardCastDirection.normalized * enemy.reach, Colors.raycastColors["player"]);

                // ATTACK
                  if (playerCast && playerCast.collider.tag == "Hero" && !playerCast.collider.GetComponent<Hero>().isInvulnerable) {
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
    }
  }

  void FixedUpdate() {
    if (enemy.hero != null && enemy.hero.pauseCase == "") {
      // PATROLLER DEAD
        if (enemy.isDead && (!enemy.isBurning || !enemy.isDeadByBurning || !enemy.isDeadByPoison) && enemy.diesFlying) {
          int index = enemy.deadAnimationIncrement;
          float xIncrement = Constants.enemyDeathXTransitions[index >= Constants.enemyDeathXTransitions.Length ? Constants.enemyDeathXTransitions.Length - 1 : index];
          float yIncrement = Constants.enemyDeathYTransitions[index >= Constants.enemyDeathYTransitions.Length ? Constants.enemyDeathYTransitions.Length - 1 : index];

          transform.position = new Vector2(enemy.deadPosition.x + (xIncrement * (enemy.isFacingLeft ? -1 : 1) * (enemy.attackedFromBehind ? -1 : 1)), enemy.deadPosition.y + yIncrement);

          enemy.deadAnimationIncrement++;
        }
    }
  }
}
