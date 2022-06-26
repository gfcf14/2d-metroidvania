using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {
    private Animator anim;
    Hero hero;

    [System.NonSerialized] public float startX;
    [System.NonSerialized] public float startY;
    [System.NonSerialized] public float maxDistance;
    [System.NonSerialized] public int increment = 0;
    [System.NonSerialized] public float distanceMultiplier = 0.0125f;
    [System.NonSerialized] public bool isFacingLeft;

    float initialAngle;
    float rotationAngle;
    int direction;

    float parabolaValue(float x) {
      return (-0.5f * Mathf.Pow(x, 2)) + (direction * maxDistance * x);
    }

    void Start() {
      anim = GetComponent<Animator>();
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
      float newX = direction * distanceMultiplier * increment;
      float newAngle = initialAngle - ((rotationAngle * increment));

      transform.position = new Vector2(startX + newX, startY + parabolaValue(newX));
      transform.rotation = Quaternion.Euler(0, 0, newAngle);

      if (newAngle <= -90 || newAngle >= 90) {
        Destroy(gameObject);
      }

      increment++;
    }
}
