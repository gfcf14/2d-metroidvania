using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEnergy : MonoBehaviour {
  [System.NonSerialized] Animator anim;
  [SerializeField] public string summonKey;
  void Start() {
    anim = GetComponent<Animator>();
    Vector2 summonDimensions = Objects.enemyDimensions[summonKey];
    GetComponent<SpriteRenderer>().size = new Vector2(summonDimensions.x * 1.5f, summonDimensions.y * 1.5f);
  }
  void Update() {}

  public void SummonCurtain() {
    anim.Play("summon-curtain");
  }

  public void SummonEnemy() {
    GameObject summonEnemy = Instantiate(Objects.prefabs["enemy"], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
  }

  public void DestroySummon() {
    Destroy(gameObject);
  }
}
