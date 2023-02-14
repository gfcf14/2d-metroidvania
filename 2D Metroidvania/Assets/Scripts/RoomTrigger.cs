using UnityEngine;

public class RoomTrigger : MonoBehaviour {
  [SerializeField] GameObject virtualCam;

  private void OnTriggerEnter2D(Collider2D col) {
    if (col.CompareTag("RoomTraverser")) {
      virtualCam.SetActive(true);
      foreach(Transform child in gameObject.transform) {
        if (child.tag == "EnemySpawner") {
          GameObject enemySpawned = Instantiate(Objects.prefabs["enemy"], new Vector3(child.transform.position.x, child.transform.position.y, 0), Quaternion.identity);
          enemySpawned.transform.SetParent(transform);
        } else if (child.tag == "Enemy" && child.name == "Boss") {
          child.gameObject.GetComponent<Enemy>().isOnCamera = true;
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
  }
}
