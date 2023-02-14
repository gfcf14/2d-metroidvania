using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
  void Start() {
    GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
  }
  void Update() {}
}
