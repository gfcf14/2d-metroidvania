using UnityEngine;

public class EnemySpawner : MonoBehaviour {
  [SerializeField] public string enemyType = "";
  [SerializeField] public string enemyKey = "";
  [SerializeField] public string specificDrop = "";
  [SerializeField] public bool isMiniBoss = false;
  void Start() {}
  void Update() {}

  public void Spawn() {
    GameObject enemySpawned = Instantiate(Objects.prefabs["enemy"], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    enemySpawned.transform.SetParent(transform);
    Enemy enemyScript = enemySpawned.GetComponent<Enemy>();
    enemyScript.key = enemyKey != "" ? enemyKey : Constants.meadowEnemies[UnityEngine.Random.Range(0, Constants.meadowEnemies.Length)];

    if (isMiniBoss) {
      enemyScript.isOnCamera = true;
    }

    // Specifies a type for the enemy. If none, default to patroller
    enemyScript.type = enemyType ?? "patroller";

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
