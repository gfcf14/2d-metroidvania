using System;
using System.Collections;
using UnityEngine;

public class RoomTrigger : MonoBehaviour {
  [SerializeField] GameObject virtualCam;

  private void OnTriggerEnter2D(Collider2D col) {
    if (col.CompareTag("RoomTraverser")) {
      virtualCam.SetActive(true);
      GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().currentRoom = gameObject;
      foreach(Transform child in gameObject.transform) {
        if (child.tag == "EnemySpawner") {
          GameObject enemySpawned = Instantiate(Objects.prefabs["enemy"], new Vector3(child.transform.position.x, child.transform.position.y, 0), Quaternion.identity);
          enemySpawned.transform.SetParent(transform);
          enemySpawned.GetComponent<Enemy>().key = Constants.meadowEnemies[UnityEngine.Random.Range(0, Constants.meadowEnemies.Length)];
        } else if (child.tag == "Enemy" && child.name == "Boss") {
          child.gameObject.GetComponent<Enemy>().isOnCamera = true;
        }
      }

    }

    if (col.gameObject.name == "BossEntryCheck") {
      foreach(Transform child in gameObject.transform) {
        if (child.tag == "Enemy" && child.name == "Boss") {
          GameObject hero = GameObject.FindGameObjectWithTag("Hero");
          Rigidbody2D heroBody = hero.GetComponent<Rigidbody2D>();
          Hero heroScript = hero.GetComponent<Hero>();

          heroScript.SetPauseCase("boss-room-entry");
          heroScript.bossTransitionDirection = (int)(heroBody.velocity.x / Math.Abs(heroBody.velocity.x));
          StartCoroutine(PauseRoomWhileOnBossEntry());
        }
      }
    }
  }

  private void OnTriggerExit2D(Collider2D col) {
    if (col.CompareTag("RoomTraverser")) {
      virtualCam.SetActive(false);
      foreach (Transform child in gameObject.transform) {
        if (child.tag == "Enemy" && child.name != "Boss") {
          if (child.name == "Boss") {
            child.gameObject.GetComponent<Enemy>().isOnCamera = false;
          } else {
            GameObject.Destroy(child.gameObject);
          }
        }
      }
    }

    if (col.gameObject.name == "BossEntryCheck") {
      Hero hero = GameObject.FindGameObjectWithTag("Hero").gameObject.GetComponent<Hero>();
      if (hero.isAutonomous && hero.mustTransitionOnAir) {
        hero.mustTransitionOnAir = false;
        hero.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
      }
    }
  }

  IEnumerator PauseRoomWhileOnBossEntry() {
    yield return new WaitForSecondsRealtime(3);

    GameObject hero = GameObject.FindGameObjectWithTag("Hero");
    Hero heroScript = hero.GetComponent<Hero>();

    heroScript.ClearPauseCase();
    heroScript.isAutonomous = true;
    hero.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    if (heroScript.isFacingLeft && heroScript.bossTransitionDirection == 1) {
      hero.transform.localScale = Vector3.one;
      heroScript.isFacingLeft = false;
    } else if (!heroScript.isFacingLeft && heroScript.bossTransitionDirection == -1) {
      hero.transform.localScale = new Vector3(-1, 1, 1);
      heroScript.isFacingLeft = true;
    }

    if (!heroScript.isGrounded) {
      heroScript.mustTransitionOnAir = true;
    }
  }
}
