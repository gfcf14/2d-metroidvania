using UnityEngine;

public class PlayerStopper : MonoBehaviour {
  [SerializeField] GameObject bossStatusCanvas;

  private InGame inGame;
  void Start() {
    inGame = GameObject.Find("InGame").gameObject.GetComponent<InGame>();
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
        EnemySpawner bossSpawner = transform.parent.Find("EnemySpawner").gameObject.GetComponent<EnemySpawner>(); // .gameObject.GetComponent<Enemy>();

        if (bossSpawner.isMiniBoss) {
          GameObject roomBoss = Helpers.FindChildWithNameContaining(bossSpawner.transform, "Enemy");

          if (roomBoss != null) {
            Enemy bossScript = roomBoss.GetComponent<Enemy>();

            bossStatusCanvas.GetComponent<BossBarsCanvas>().boss = bossScript;
            bossStatusCanvas.GetComponent<BossBarsCanvas>().bossName = bossScript.enemyName;
            bossStatusCanvas.SetActive(true);
          }
        }

        // starts playing the miniboss soundtrack
        inGame.PlaySoundtrack("miniboss");
      }
    }
  }
}
