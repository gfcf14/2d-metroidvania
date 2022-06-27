using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {
    private Animator anim;
    private SpriteRenderer objectRenderer;

    Hero hero;

    [System.NonSerialized] public bool isFacingLeft;
    [System.NonSerialized] public bool hasCollided;

    [System.NonSerialized] public int parabolaIncrement = 0;

    [System.NonSerialized] public float startX;
    [System.NonSerialized] public float startY;
    [System.NonSerialized] public float maxDistance;
    [System.NonSerialized] public float distanceMultiplier = 0.0125f;
    [System.NonSerialized] public float collideTime;
    [System.NonSerialized] public float maxEllapsedCollideTime = 1500f;

    float initialAngle;
    float rotationAngle;
    int direction;

    float parabolaValue(float x) {
      return (-0.5f * Mathf.Pow(x, 2)) + (direction * maxDistance * x);
    }

    void Start() {
      anim = GetComponent<Animator>();
      objectRenderer = GetComponent<SpriteRenderer>();
      hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();

      direction = isFacingLeft ? -1 : 1;
      initialAngle = direction * 30f;

      transform.rotation = Quaternion.Euler(0, 0, initialAngle);

      rotationAngle = (maxDistance / initialAngle);

      if (isFacingLeft) {
        transform.localScale = new Vector3(-1, 1, 1);
      }
    }

    void Update() {
      if (!hasCollided) {
        float newX = direction * distanceMultiplier * parabolaIncrement;
        float newAngle = initialAngle - ((rotationAngle * parabolaIncrement));

        transform.position = new Vector2(startX + newX, startY + parabolaValue(newX));
        transform.rotation = Quaternion.Euler(0, 0, newAngle);

        // if (newAngle <= -90 || newAngle >= 90) {
        //   Destroy(gameObject);
        // }

        parabolaIncrement++;
      } else {
        float ellapsedCollideTime = (Time.time * 1000) - collideTime;

        if (ellapsedCollideTime < maxEllapsedCollideTime) {
          objectRenderer.color = new Color(255, 255, 255, 1 - (ellapsedCollideTime / maxEllapsedCollideTime));
        } else {
          Destroy(gameObject);
        }
      }
    }
}
