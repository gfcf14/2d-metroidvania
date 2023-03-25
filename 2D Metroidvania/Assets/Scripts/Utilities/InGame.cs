using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour {
  void Start() {}
  void Update() {}

  public void InstantiatePrefab(string prefab, Transform trans, SpriteRenderer spr) {
      Vector2 itemOrigin = new Vector2(trans.position.x, trans.position.y + (spr.bounds.size.y / 2));
      GameObject droppedItem = Instantiate(Objects.prefabs[prefab], itemOrigin, Quaternion.identity);
      Droppable droppedObject = droppedItem.transform.Find("GameObject").GetComponent<Droppable>();
      droppedObject.key = "moonlight-pendant"; // TODO: come up with a random system to add the item instead of hardcoding
      droppedObject.isDropped = true;
  }
}
