using UnityEngine;

public class EnemyCollider : MonoBehaviour {
  Enemy enemy;

  void Start() {
    enemy = gameObject.transform.parent.gameObject.GetComponent<Enemy>();
  }

  void Update() {}

  private void OnCollisionEnter2D(Collision2D col) {
    if (col.gameObject.name == "EnemyCollider") {
      Physics2D.IgnoreCollision(col.gameObject.GetComponent<CapsuleCollider2D>(), GetComponent<CapsuleCollider2D>());
    } else {
      enemy.Collision(col);
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    if (col.gameObject.tag == "Weapon" && col.gameObject.name != "ThrowableCollider" && col.gameObject.name != "ArrowCollider") {
      if (!col.gameObject.GetComponent<Weapon>().triggeredObjects.Contains(gameObject)) {
        enemy.Trigger(col);
        col.gameObject.GetComponent<Weapon>().triggeredObjects.Add(gameObject);
      }
    } else {
      enemy.Trigger(col);
    }
  }
}
