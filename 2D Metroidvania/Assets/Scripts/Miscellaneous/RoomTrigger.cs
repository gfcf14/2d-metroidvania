using System;
using System.Collections;
using UnityEngine;

public class RoomTrigger : MonoBehaviour {
  [SerializeField] GameObject virtualCam;

  private GameObject hero;
  private Hero heroScript;
  private InGame inGame;

  void Start() {
    hero = GameObject.FindGameObjectWithTag("Hero");
    heroScript = hero.GetComponent<Hero>();
    inGame = GameObject.Find("InGame").gameObject.GetComponent<InGame>();
  }

  private void OnTriggerEnter2D(Collider2D col) {
    if (col.CompareTag("RoomTraverser")) {
      virtualCam.SetActive(true);

      heroScript.currentRoom = gameObject;
      foreach(Transform child in gameObject.transform) {
        if (child.tag == "EnemySpawner") {
          child.gameObject.GetComponent<EnemySpawner>().Spawn();
        } else if (child.tag == "Enemy" && child.name == "Boss") {
          child.gameObject.GetComponent<Enemy>().isOnCamera = true;
        }
      }

      // if the chat canvas is active, close it
      if (heroScript.chatCanvas.activeSelf) {
        heroScript.chatCanvas.GetComponent<ChatCanvas>().FinishChat(playerLeft: true);
      }

    }

    // if the player entes a room with a boss
    if (col.gameObject.name == "ProximityCheck") {
      foreach(Transform child in gameObject.transform) {
        if (child.tag == "Enemy" && child.name == "Boss") {
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
        if (child.tag == "EnemySpawner") {
          child.gameObject.GetComponent<EnemySpawner>().Cleanse();
        } else if (child.name == "Boss") {
          child.gameObject.GetComponent<Enemy>().isOnCamera = false;
        } else if (child.name.Contains("Droppable")) {
          Droppable droppableInstance = child.Find("GameObject").gameObject.GetComponent<Droppable>();

          // only spawned items get destroyed; items that are part of the Scene will only destroy when grabbed
          if (droppableInstance.room != null) {
            GameObject.Destroy(child.gameObject);
          }
        } else if (child.name.Contains("ArrowBurn")) { // destroys arrow burns so they don't infinitely harm enemies when player exits and enters repeatedly
          GameObject.Destroy(child.gameObject);
        }
      }
    }

    if (col.gameObject.name == "ProximityCheck") {
      if (heroScript.isAutonomous && heroScript.mustTransitionOnAir) {
        heroScript.mustTransitionOnAir = false;
        hero.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
      }
    }
  }

  IEnumerator PauseRoomWhileOnBossEntry() {
    inGame.ToggleSoundtrack(isPaused: false, restart: true);

    yield return new WaitForSecondsRealtime(3);

    heroScript.ClearPauseCase();
    heroScript.isFightingBoss = true;
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
