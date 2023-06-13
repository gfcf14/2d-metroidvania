using UnityEngine;

public class BossEntryCheck : MonoBehaviour {
  [SerializeField] GameObject hero;
  [System.NonSerialized] Hero heroScript;
  void Start() {
    heroScript = hero.GetComponent<Hero>();
  }
  void Update() {}

  private void OnCollisionEnter2D(Collision2D col) {
    if (Helpers.IsValueInArray(Constants.bossEntryCheckNonColliderTags, col.collider.tag) || col.collider.name == "Grounder") {
      Physics2D.IgnoreCollision(col.collider, GetComponent<BoxCollider2D>());
    } else {
      Debug.Log("colliding with " + col.collider.name);
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    string colTag = col.gameObject.tag;

    if (colTag == "NPC") {
      NPC npcFound = col.gameObject.GetComponent<NPC>();

      if (npcFound.actionAvailable != "") {
        heroScript.SetNPCAction(npcFound.actionAvailable);
        heroScript.actionCanvas.SetActive(true);
      }
    }
  }

  private void OnTriggerExit2D(Collider2D col) {
    string colTag = col.gameObject.tag;

    if (colTag == "NPC") {
      heroScript.actionCanvas.SetActive(false);
    }
  }
}
