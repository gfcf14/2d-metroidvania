using UnityEngine;

public class PlayerStopper : MonoBehaviour {
  [SerializeField] GameObject bossStatusCanvas;
  void Start() {
    GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
  }

  void Update() {}

  private void OnTriggerEnter2D(Collider2D col) {
    if (col.gameObject.tag == "Hero") {
      Hero hero = col.gameObject.GetComponent<Hero>();

      if (hero.isAutonomous) {
        hero.isAutonomous = false;
        hero.isRunning = false;
        col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Sets bounds around the room so player doesn't leave until boss is killed
        GameObject bounds = hero.currentRoom.transform.Find("Bounds").gameObject;
        if (bounds) {
          bounds.SetActive(true);
        }

        // Gets boss name and level to activate the boss status canvas
        Enemy roomBoss = transform.parent.Find("Boss").gameObject.GetComponent<Enemy>();
        bossStatusCanvas.GetComponent<BossBarsCanvas>().boss = roomBoss;
        bossStatusCanvas.GetComponent<BossBarsCanvas>().bossName = roomBoss.EnemyName;
        bossStatusCanvas.SetActive(true);
      }
    }
  }
}
