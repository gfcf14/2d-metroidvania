using UnityEditor;
using UnityEngine;
using System.Linq;

public class InGame : MonoBehaviour {
  void Start() {}
  void Update() {}

  public void InstantiatePrefab(string prefab, string key, Transform trans, SpriteRenderer spr) {
    Vector2 itemOrigin = new Vector2(trans.position.x, trans.position.y + (spr.bounds.size.y / 2));
    GameObject droppedItem = Instantiate(Objects.prefabs[prefab], itemOrigin, Quaternion.identity);
    Droppable droppedObject = droppedItem.transform.Find("GameObject").GetComponent<Droppable>();
    droppedObject.key = key;
    droppedObject.isDropped = true;
  }
}
