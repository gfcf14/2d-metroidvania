using UnityEngine;

public class DirectionCheck : MonoBehaviour {
  [SerializeField] public string direction = "";
  Hero hero;
  void Start() {
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
  }
  void Update() {}

  private void OnTriggerEnter2D(Collider2D col) {
    if (col.tag == "Floor" || col.tag == "Wall") {
      hero.SetCollisionDirection(direction, true);
    }
  }

  private void OnTriggerExit2D(Collider2D col) {
    if (col.tag == "Floor" || col.tag == "Wall") {
      hero.SetCollisionDirection(direction, false);
    }
  }
}
