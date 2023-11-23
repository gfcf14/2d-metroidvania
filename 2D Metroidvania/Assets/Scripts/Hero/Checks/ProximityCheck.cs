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

  private void SetNPCAction(NPC npc) {
    if (npc.actionAvailable != "") {
      if (!heroScript.isOnChat) {
        heroScript.actionCanvas.SetActive(true);
      }
      heroScript.SetNPCAction(npc.actionAvailable);
      heroScript.NPCnearbyAction = npc.actionAvailable;
    }
  }

  private void OnTriggerEnter2D(Collider2D col) {
    string colTag = col.gameObject.tag;

    if (colTag == "NPC") {
      heroScript.NPCnearby = col.gameObject.name;
      SetNPCAction(col.gameObject.GetComponent<NPC>());
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

  // gets the first gameobject the proximity check overlaps with
  public GameObject OverlapsWith(string tag) {
    BoxCollider2D proximityCollider = GetComponent<BoxCollider2D>();

    Bounds bounds = proximityCollider.bounds;

    Collider2D[] colliders = Physics2D.OverlapBoxAll(bounds.center, bounds.size, 0f);
    foreach (Collider2D col in colliders) {
      if (col.CompareTag(tag)) {
        return col.gameObject;
      }
    }

    return null;
  }

  public void DecideActionShow() {
    GameObject overlappedObject = OverlapsWith("NPC");

    if (overlappedObject != null) {
      heroScript.NPCnearby = overlappedObject.name;
      SetNPCAction(overlappedObject.GetComponent<NPC>());
    }
  }
}
