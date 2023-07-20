using UnityEngine;
using UnityEngine.Tilemaps;

public class InGame : MonoBehaviour {
  private Tilemap groundTiles;
  private Tilemap detailTiles;
  void Start() {
    groundTiles = GameObject.Find("Ground").GetComponent<Tilemap>();
    detailTiles = GameObject.Find("Detail").GetComponent<Tilemap>();
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
}
