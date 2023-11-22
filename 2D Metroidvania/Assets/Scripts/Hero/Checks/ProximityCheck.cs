using UnityEngine;

public class ProximityCheck : MonoBehaviour {
  [SerializeField] GameObject hero;
  [System.NonSerialized] Hero heroScript;
  void Start() {
    heroScript = hero.GetComponent<Hero>();
  }
  void Update() {}

  private void OnCollisionEnter2D(Collision2D col) {
    if (Helpers.IsValueInArray(Constants.proximityCheckNonColliderTags, col.collider.tag) || col.collider.name == "Grounder") {
      Physics2D.IgnoreCollision(col.collider, GetComponent<BoxCollider2D>());
    } else {
      Debug.Log("colliding with " + col.collider.name);
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    string colTag = col.gameObject.tag;

    if (colTag == "NPC") {
      NPC npcFound = col.gameObject.GetComponent<NPC>();
      heroScript.NPCnearby = col.gameObject.name;

      if (npcFound.actionAvailable != "") {
        heroScript.actionCanvas.SetActive(true);
        heroScript.SetNPCAction(npcFound.actionAvailable);
        heroScript.NPCnearbyAction = npcFound.actionAvailable;
      }
    }
  }

  private void OnTriggerExit2D(Collider2D col) {
    string colTag = col.gameObject.tag;

    if (colTag == "NPC") {
      heroScript.actionCanvas.SetActive(false);
      heroScript.actionCanvas.GetComponent<ActionCanvas>().ClearSpecs();
      heroScript.NPCnearby = "";
      heroScript.NPCnearbyAction = "";
    }
  }
}
