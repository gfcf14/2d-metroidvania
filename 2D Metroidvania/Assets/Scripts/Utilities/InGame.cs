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

  public void InstantiatePrefab(string prefab, string key, string rarity, GameObject room, Transform trans, SpriteRenderer spr) {
    // mainly so items instantiated from stacked breakables do not overlap fully
    float randomOffset = UnityEngine.Random.Range(-0.2f, 0.2f);

    Vector2 itemOrigin = new Vector2(trans.position.x + randomOffset, trans.position.y + (spr.bounds.size.y / 2));
    GameObject droppedItem = Instantiate(Objects.prefabs[prefab], itemOrigin, Quaternion.identity);
    Droppable droppableScript = droppedItem.transform.Find("GameObject").GetComponent<Droppable>();
    droppableScript.key = key;
    droppableScript.rarity = rarity;
    droppableScript.isDropped = true;
    droppableScript.room = room;

    // adds flicker effect
    droppableScript.gameObject.AddComponent<Flicker>().enabled = false;
  }

  public string GetGroundMaterial(string tileName) {
    if (tileName == "" || tileName == null) {
      return null;
    }

    // gets the material given that tilebase name convension is "tiles-material_number"
    string material = tileName.Split('_')[0].Split('-')[1];

    return Helpers.GetMaterial(material, tileName);
  }

  public string GetTileName(Vector3 objectPosition) {
    Vector3Int groundTileCoordinates = groundTiles.WorldToCell(objectPosition);
    Vector3 tileCenter = groundTiles.GetCellCenterWorld(groundTileCoordinates);
    Vector3 tileHalfSize = groundTiles.cellSize / 2;
    Vector3Int groundTileBelowCoordinates = groundTileCoordinates;
    TileBase groundTileBelowPlayer = groundTiles.GetTile(groundTileBelowCoordinates);

    // if not found, get the tile below
    if (groundTileBelowPlayer == null) {
      groundTileCoordinates = groundTileCoordinates + new Vector3Int(0, -1, 0);
      tileCenter = groundTiles.GetCellCenterWorld(groundTileCoordinates);
      groundTileBelowCoordinates = groundTileCoordinates;
      groundTileBelowPlayer = groundTiles.GetTile(groundTileBelowCoordinates);
    }

    // Draw lines around the boundaries of the selected tile
    Debug.DrawLine(tileCenter + new Vector3(-tileHalfSize.x, -tileHalfSize.y), tileCenter + new Vector3(tileHalfSize.x, -tileHalfSize.y), Color.red);
    Debug.DrawLine(tileCenter + new Vector3(tileHalfSize.x, -tileHalfSize.y), tileCenter + new Vector3(tileHalfSize.x, tileHalfSize.y), Color.red);
    Debug.DrawLine(tileCenter + new Vector3(tileHalfSize.x, tileHalfSize.y), tileCenter + new Vector3(-tileHalfSize.x, tileHalfSize.y), Color.red);
    Debug.DrawLine(tileCenter + new Vector3(-tileHalfSize.x, tileHalfSize.y), tileCenter + new Vector3(-tileHalfSize.x, -tileHalfSize.y), Color.red);

    return groundTileBelowPlayer ? groundTileBelowPlayer.name : "";
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
        return GetGroundMaterial(groundTileBelowPlayer == null ? "" : groundTileBelowPlayer.name);
      }
    } else {
      return GetGroundMaterial(groundTileBelowPlayer == null ? "" : groundTileBelowPlayer.name);
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

  public void DrawDamage(Vector2 position, int damage, bool? isCritical, string soundType = "") {
    GameObject damageObject = Instantiate(Objects.prefabs["damage-container"], position, Quaternion.identity);
    damageObject.transform.SetParent(null);
    DamageContainer damageScript = damageObject.GetComponent<DamageContainer>();
    damageScript.damage = damage;
    damageScript.isCritical = isCritical ?? false;

    if (soundType != "") {
      damageScript.soundType = soundType;
    }
  }

  // Plays a sound by creating a sound prefab that lives only until it is done playing
  public void PlaySound(AudioClip clip, Vector3 position) {
    GameObject sound = Instantiate(Objects.prefabs["sound"], position, Quaternion.identity);
    Sound soundInstance = sound.GetComponent<Sound>();
    soundInstance.PlaySound(clip);
  }

  // instantiates a defense/block sprite on a contact point
  public void Block(Vector2 position, bool isFacingLeft) {
    GameObject defenseEffect = Instantiate(Objects.prefabs["defense"], position, Quaternion.identity);
    defenseEffect.GetComponent<Defense>().isFacingLeft = isFacingLeft;
  }
}
