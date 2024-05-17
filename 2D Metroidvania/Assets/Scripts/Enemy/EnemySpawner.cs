using UnityEngine;

public class EnemySpawner : MonoBehaviour {
  [SerializeField] public string enemyKey = "";
  [SerializeField] public string specificDrop = "";
  void Start() {}
  void Update() {}

  public void Spawn() {
    GameObject enemySpawned = Instantiate(Objects.prefabs["enemy"], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    enemySpawned.transform.SetParent(transform);
    Enemy enemyScript = enemySpawned.GetComponent<Enemy>();
    enemyScript.key = enemyKey != "" ? enemyKey : Constants.meadowEnemies[UnityEngine.Random.Range(0, Constants.meadowEnemies.Length)];

    if (specificDrop != "") {
      enemyScript.specificDrop = specificDrop;
      enemyScript.spawnedFrom = gameObject;
    }

    // TODO: implement a better way to assign level values
    enemyScript.level = 1;
  }

  // Destroys all enemies/droppables originated
  public void Cleanse() {
    foreach (Transform child in transform) {
      Destroy(child.gameObject);
    }
  }
}
