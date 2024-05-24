using UnityEngine;

public class RoomTraverser : MonoBehaviour {
  Hero hero;
  void Start() {
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
  }

  void Update() {}

  public void OnTriggerEnter2D(Collider2D col) {
    if (col.tag == "Room") {
      // if entering room, move based on speed to scape the previous room's bounds. Avoid displacements if on inclines
      float xDisp = ((hero.heroWidth / 4) * hero.body.velocity.x) * (hero.groundType == "level" ? 1 : 0);
      float yDisp = ((hero.heroHeight / 4) * hero.body.velocity.y) * (hero.groundType == "level" ? 1 : 0);

      hero.transform.position = new Vector2(hero.transform.position.x + xDisp, hero.transform.position.y + yDisp);
    }
  }
}
