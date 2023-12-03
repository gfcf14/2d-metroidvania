using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class InGame : MonoBehaviour {
  private Tilemap groundTiles;
  private Tilemap detailTiles;
  private AudioSource soundtrack;
  private float fadeDuration = 0.25f;

  [SerializeField] float soundtrackPausedTime = 0f; // Stores the soundtrack paused time position
  [SerializeField] float miniBossTrackPausedTime = 0f; // Stores the min boss track paused time position

  public Hero hero;
  public GameObject mainOverlay;
  [SerializeField] GameObject mainCamera;

  void Start() {
    SetComponents();
  }

  public void SetComponents() {
    groundTiles = GameObject.Find("Ground").GetComponent<Tilemap>();
    detailTiles = GameObject.Find("Detail").GetComponent<Tilemap>();
    mainOverlay = GameObject.Find("MainOverlay");
    soundtrack = GetComponent<AudioSource>();
    soundtrack.volume = Settings.maxSoundtrackVolume;
    soundtrack.loop = true;

    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
  }
  void Update() {}

  public void PlaySoundtrack(string key) {
    soundtrack.clip = Sounds.soundtracks[key];

    if (Settings.playSoundtrack) {
      soundtrack.Play();
    }
  }

  // finds the camera which is at the room where the player is,
  // then activate it and set the main camera to center at the room where the player is
  public void ActivateCurrentCam() {
    if (hero == null) {
      SetComponents();
    }

    BoxCollider2D heroCollider = hero.GetComponent<BoxCollider2D>();
    Bounds bounds = heroCollider.bounds;

    Collider2D[] colliders = Physics2D.OverlapBoxAll(bounds.center, bounds.size, 0f);
    foreach (Collider2D col in colliders) {
      if (col.gameObject.name == "VCam") {
        col.gameObject.SetActive(true);
        Transform camParentTransform = col.gameObject.transform.parent;
        // TODO: this assumes we'll always start the game in a room with default dimensions (16 x 9). Consider how to do this for rooms with non-default dimensions if needed
        mainCamera.transform.position = new Vector3(camParentTransform.position.x + (Constants.defaultRoomWidth / 2), camParentTransform.position.y - (Constants.defaultRoomHeight / 2), -10);
        break;
      }
    }
  }

  public void SwitchFromMiniBossTrack(string key) {
    soundtrack.Stop();
    soundtrack.clip = Sounds.soundtracks[key];
    StartCoroutine(FadeIn(wait: true));
    miniBossTrackPausedTime = 0;
  }

  public void ToggleSoundtrack(bool isPaused, bool restart = false, bool wait = false) {
    if (isPaused) {
      StartFadeIn(wait);
    } else {
      if (soundtrack.isPlaying) { // must check if soundtrack is playing; when not playing, time is 0 thus choosing to mute backgrounds would "reset" soundtrack position
        if (hero.isFightingBoss) {
          miniBossTrackPausedTime = soundtrack.time;
        } else {
          soundtrackPausedTime = soundtrack.time;
        }
      }

      // ensures that, when switching soundtracks, the new soundtrack starts from the beginning
      if (restart) {
        soundtrack.time = 0f;
      }

      StartFadeOutAndPause();
    }
  }

  public void StartFadeOutAndPause() {
    StartCoroutine(FadeOutAndPause());
  }

  public void StartFadeIn(bool wait = false) {
    StartCoroutine(FadeIn(wait));
  }

  private IEnumerator FadeOutAndPause() {
    float startVolume = soundtrack.volume;

    while (soundtrack.volume > 0f) {
      soundtrack.volume -= startVolume * Time.unscaledDeltaTime / fadeDuration;
      yield return null;
    }

    soundtrack.Stop();
    soundtrack.volume = startVolume;
  }

  private IEnumerator FadeIn(bool wait = false) {
    if (wait) {
      yield return new WaitForSeconds(1);
    }

    soundtrack.time = hero.isFightingBoss ? miniBossTrackPausedTime : soundtrackPausedTime;
    soundtrack.volume = 0;
    if (Settings.playSoundtrack) {
      soundtrack.Play();
    }

    while (soundtrack.volume < Settings.maxSoundtrackVolume) {
      soundtrack.volume += Time.unscaledDeltaTime / fadeDuration;
      yield return null;
    }

    soundtrack.volume = Settings.maxSoundtrackVolume;
  }

  public void SetPauseCase(string pauseCase) {
    hero.SetPauseCase(pauseCase);
  }

  public void ClearPauseCase() {
    hero.ClearPauseCase();
  }

  public void Cover() {
    mainOverlay.GetComponent<Image>().color = new Color(0, 0, 0, 1);
  }

  public void InstantiatePrefab(string prefab, string key, string rarity, GameObject room, Vector2 position, SpriteRenderer spr) {
    // mainly so items instantiated from stacked breakables do not overlap fully
    float randomOffset = UnityEngine.Random.Range(-0.2f, 0.2f);

    Vector2 itemOrigin = new Vector2(position.x + randomOffset, position.y + (spr.bounds.size.y / 2));
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
