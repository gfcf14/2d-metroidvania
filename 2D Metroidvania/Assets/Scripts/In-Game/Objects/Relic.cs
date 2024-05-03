using System.Collections.Generic;
using UnityEngine;

public class Relic : MonoBehaviour {
  [SerializeField] public string key;
  [System.NonSerialized] SpriteRenderer relicSprite;

  private List<Vector2> positions = new List<Vector2>();
  private int maxAttempts = 100;
  void Start() {
    relicSprite = GetComponent<SpriteRenderer>();

    relicSprite.sprite = Sprites.relicSprites[key];

    for (int i = 0; i < Constants.relicSparkleLimit; i++) {
    Vector2 sparklePosition = Vector2.zero;
    bool validPosition = false;
    int attemptCount = 0; // Counter to track the number of attempts

    do {
        if (attemptCount > maxAttempts) { // Constants.maxAttempts should be a reasonable number
            Debug.LogError("Failed to place all sparkles within the distance constraints.");
            break; // Break out of the loop if too many attempts
        }

        float randomX = Random.Range(Constants.sparkleRelativeMin, Constants.sparkleRelativeMax) / 1000f * (Random.value > 0.5f ? 1 : -1);
        float randomY = Random.Range(Constants.sparkleRelativeMin, Constants.sparkleRelativeMax) / 1000f * (Random.value > 0.5f ? 1 : -1);
        sparklePosition = new Vector2(randomX, randomY);
        validPosition = true;

        foreach (Vector2 currPosition in positions) {
            if (Vector2.Distance(currPosition, sparklePosition) < Constants.sparkleDistanceRadius) {
                validPosition = false;
                break;
            }
        }

        attemptCount++;
    } while (!validPosition);

    if (validPosition) {
      Debug.Log($"Accepted Point ({sparklePosition.x:F3}, {sparklePosition.y:F3})");
      positions.Add(sparklePosition);
      GameObject sparkle = Instantiate(Objects.prefabs["sparkle"]);
      sparkle.transform.localPosition = sparklePosition;
      sparkle.transform.SetParent(transform, false);
    }
}

  }

  void Update() {}
}
