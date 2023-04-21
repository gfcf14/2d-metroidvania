using UnityEngine;

// Made by Bartha Szabolcs of GameDevJourney

public class Patroller : MonoBehaviour {
  // Components
    Enemy enemy;

  // Raycast properties
    public float diagonalForwardCastLength = 1f;
    public float forwardCastLength = 2f;
    public float proximityCastLength = 0.1f;

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
            // if (!diagonalForwardCast && diagonalForwardCast.collider.tag == "Ground") {
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

            // if (!heroIsDead) {
              Vector2 beginForwardCast = new Vector2(transform.position.x + ((enemy.enemyWidth / 2) * direction), transform.position.y + enemy.enemyHeight / 2);
              Vector2 forwardCastDirection = transform.TransformDirection(new Vector2(1 * (direction), 0));

            // FOUND PLAYER
              if (!enemy.playerFound) {
                RaycastHit2D forwardCast = Physics2D.Raycast(beginForwardCast, forwardCastDirection, forwardCastLength);
                Debug.DrawRay(beginForwardCast, forwardCastDirection.normalized * forwardCastLength, Color.red);

                // PLAYER NEARBY
                if (forwardCast && forwardCast.collider.tag == "Hero") {
                  enemy.playerFound = true;
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
        if (enemy.isDead && enemy.diesFlying) {
          int index = enemy.deadAnimationIncrement;
          float xIncrement = Constants.enemyDeathXTransitions[index >= Constants.enemyDeathXTransitions.Length ? Constants.enemyDeathXTransitions.Length - 1 : index];
          float yIncrement = Constants.enemyDeathYTransitions[index >= Constants.enemyDeathYTransitions.Length ? Constants.enemyDeathYTransitions.Length - 1 : index];

          transform.position = new Vector2(enemy.deadPosition.x + (xIncrement * (enemy.isFacingLeft ? -1 : 1) * (enemy.attackedFromBehind ? -1 : 1)), enemy.deadPosition.y + yIncrement);

          enemy.deadAnimationIncrement++;
        }
    }
  }
}
