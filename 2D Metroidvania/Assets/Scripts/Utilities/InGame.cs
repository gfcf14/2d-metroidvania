using UnityEditor;
using UnityEngine;
using System.Linq;

public class InGame : MonoBehaviour {
  void Start() {}
  void Update() {}

  public void InstantiatePrefab(string prefab, string key, GameObject room, Transform trans, SpriteRenderer spr) {
    Vector2 itemOrigin = new Vector2(trans.position.x, trans.position.y + (spr.bounds.size.y / 2));
    GameObject droppedItem = Instantiate(Objects.prefabs[prefab], itemOrigin, Quaternion.identity);
    Droppable droppableScript = droppedItem.transform.Find("GameObject").GetComponent<Droppable>();
    droppableScript.key = key;
    droppableScript.isDropped = true;
    droppableScript.room = room;

    // adds flicker effect
    droppableScript.gameObject.AddComponent<Flicker>().enabled = false;
  }
}
