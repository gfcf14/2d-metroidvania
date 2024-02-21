using UnityEngine;

public class EnemySpawner : MonoBehaviour {
  [Header("EnemyKey. If set, enemy spawned won't be random")]
  [SerializeField] public string enemyKey = "";

  [Header("SpecificDrop. If set, enemy will drop only the item specified")]
  [SerializeField] public string specificDrop = "";
  void Start() {}
  void Update() {}
}
