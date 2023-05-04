using UnityEngine;

public class EnemySpawner : MonoBehaviour {
  [Header("EnemyKey. If set, enemy spawned won't be random")]
  [SerializeField] public string enemyKey = "";
  void Start() {
    GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
  }
  void Update() {}
}
