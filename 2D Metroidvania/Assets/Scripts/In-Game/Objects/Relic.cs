using System;
using UnityEngine;

public class Relic : MonoBehaviour {
  [SerializeField] public string key;
  [System.NonSerialized] SpriteRenderer relicSprite;
  [System.NonSerialized] PolygonCollider2D relicCollider;

  Vector2 GetRandomPoint(Vector2[] corners) {
    float minX = Mathf.Min(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
    float maxX = Mathf.Max(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
    float minY = Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
    float maxY = Mathf.Max(corners[0].y, corners[1].y, corners[2].y, corners[3].y);

    float randomX = UnityEngine.Random.Range(minX, maxX);
    float randomY = UnityEngine.Random.Range(minY, maxY);

    return new Vector2(randomX, randomY);
  }

  Vector2 RotatePointAroundPivot(Vector2 point, Vector2 pivot, float angle) {
    angle = angle * Mathf.Deg2Rad; // Convert angle to radians
    Vector2 dir = point - pivot; // Get point direction relative to pivot
    dir = new Vector2(
        dir.x * Mathf.Cos(angle) - dir.y * Mathf.Sin(angle),
        dir.x * Mathf.Sin(angle) + dir.y * Mathf.Cos(angle)
    ); // Rotate it
    point = dir + pivot; // Calculate rotated point
    return point;
  }
  void Start() {
    relicCollider = gameObject.AddComponent<PolygonCollider2D>();
    relicCollider.autoTiling = true;
    relicCollider.isTrigger = true;

    relicSprite = GetComponent<SpriteRenderer>();
    relicSprite.sprite = Sprites.relicSprites[key];

    // These variables are to define a rectangle's corners located at a specific distance from the relic's center (the offset)
    // up to a distance from that offset, ensuring a specific location for a sparkle prefab to be defined
    float offsetXToCenter = (Constants.sparkleParentOffset * (float)(Math.Sqrt(3))) / 2;
    float offsetYToCenter = Constants.sparkleParentOffset / 2;
    float distanceXToOffset = ((Constants.sparkleParentOffset + Constants.sparkleOffsetDistance) * (float)(Math.Sqrt(3))) / 2;

    Vector2[] rectangleCorners = {
      new Vector2(offsetXToCenter, offsetYToCenter),
      new Vector2(distanceXToOffset, offsetYToCenter),
      new Vector2(offsetXToCenter, -offsetYToCenter),
      new Vector2(distanceXToOffset, -offsetYToCenter)
    };

    for (int i = 0; i < Constants.relicSparkleLimit; i++) {
      // gets a random point from the prefab above
      Vector2 sparklePosition = GetRandomPoint(rectangleCorners);
      // creates the sparkle as a child of the relic
      GameObject sparkle = Instantiate(Objects.prefabs["sparkle"], transform);

      // since the random position of a sparkle is limited by an imaginary rectangle to the right of the relic's center, to
      // ensure these sparkles (up to 6) surround the relic each subsequent sparkle is rotated 60 degress, ensuring each sparkle
      // covers 1/6th of the area around the relic, enhancing its perceived brightness
      sparkle.transform.localPosition = RotatePointAroundPivot(sparklePosition, Vector2.zero, i * 60);
    }

  }

  void Update() {}

  private void OnTriggerEnter2D(Collider2D col) {
    string gameObjectTag = col.gameObject.tag;
    if (gameObjectTag == "Hero") {
      Hero heroScript = col.gameObject.GetComponent<Hero>();

      heroScript.AddToRelics(key);
      heroScript.GetRelic();

      Destroy(transform.parent.gameObject);
    }
  }
}
