using UnityEngine;
using UnityEngine.Tilemaps;

public class InGame : MonoBehaviour {
  private Tilemap groundTiles;
  private Tilemap detailTiles;
  private Hero hero;
  void Start() {
    groundTiles = GameObject.Find("Ground").GetComponent<Tilemap>();
    detailTiles = GameObject.Find("Detail").GetComponent<Tilemap>();
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
  }
  void Update() {}

  public void InstantiatePrefab(string prefab, string key, GameObject room, Transform trans, SpriteRenderer spr) {
    // mainly so items instantiated from stacked breakables do not overlap fully
    float randomOffset = UnityEngine.Random.Range(-0.2f, 0.2f);

    Vector2 itemOrigin = new Vector2(trans.position.x + randomOffset, trans.position.y + (spr.bounds.size.y / 2));
    GameObject droppedItem = Instantiate(Objects.prefabs[prefab], itemOrigin, Quaternion.identity);
    Droppable droppableScript = droppedItem.transform.Find("GameObject").GetComponent<Droppable>();
    droppableScript.key = key;
    droppableScript.isDropped = true;
    droppableScript.room = room;

    // adds flicker effect
    droppableScript.gameObject.AddComponent<Flicker>().enabled = false;
  }

  public string GetGroundMaterial(string tileName) {
    // gets the material given that tilebase name convension is "tiles-material_number"
    string material = tileName.Split('_')[0].Split('-')[1];

    switch (material) {
      case "meadows":
        return "grass";
      default:
        Debug.Log("Material (" + material + ") not accounted for, using tile " + tileName);
        return null;
    }
  }

  public string GetTileMaterial(Vector3 objectPosition) {
    Vector3Int groundTileCoordinates = groundTiles.WorldToCell(objectPosition);
    Vector3Int groundTileBelowCoordinates = groundTileCoordinates + new Vector3Int(0, -1, 0);

    Vector3Int detailTileCoordinates = detailTiles.WorldToCell(objectPosition);
    Vector3Int detailTileBelowCoordinates = detailTileCoordinates + new Vector3Int(0, -1, 0);

    TileBase groundTileBelowPlayer = groundTiles.GetTile(groundTileBelowCoordinates);
    TileBase detailTileBelowPlayer = detailTiles.GetTile(detailTileBelowCoordinates);

    if (detailTileBelowPlayer != null) {
      int detailTileIndex = int.Parse(detailTileBelowPlayer.name.Replace("tiles-details_", ""));

      if (Helpers.IsValueInArray(Constants.detailDirt, detailTileIndex)) {
        return "dirt";
      } else {
        return GetGroundMaterial(groundTileBelowPlayer.name);
      }
    } else {
      return GetGroundMaterial(groundTileBelowPlayer.name);
    }
  }

  public bool IsInRoom(string roomName) {
    return roomName == hero.currentRoom.name;
  }

  // Checks the name of the provided parent if it's a room. If not a room, get its parent and recheck. If null, return blank
  public string FindRoom(Transform currentParentCheck) {
    if (currentParentCheck == null) {
      return "";
    }

    if (currentParentCheck.gameObject.name.Contains("Room")) {
      return currentParentCheck.gameObject.name;
    }

    return FindRoom(currentParentCheck.parent);
  }

  // draws a rectangle based on parameter given
  public void DrawRectangle(Vector2 center, Vector2 size) {
    Vector2 halfSize = size / 2f;

    Vector3 topLeft = new Vector3(center.x - halfSize.x, center.y + halfSize.y, 0f);
    Vector3 topRight = new Vector3(center.x + halfSize.x, center.y + halfSize.y, 0f);
    Vector3 bottomRight = new Vector3(center.x + halfSize.x, center.y - halfSize.y, 0f);
    Vector3 bottomLeft = new Vector3(center.x - halfSize.x, center.y - halfSize.y, 0f);

    Debug.DrawLine(topLeft, topRight, Color.red);
    Debug.DrawLine(topRight, bottomRight, Color.red);
    Debug.DrawLine(bottomRight, bottomLeft, Color.red);
    Debug.DrawLine(bottomLeft, topLeft, Color.red);
  }
}
