using UnityEngine;

public class DamageFlicker : MonoBehaviour {
  public float flickerSpeed = 16f;
  private SpriteRenderer characterRenderer;
  private MonoBehaviour mainScript;
  void Start() {
    characterRenderer = GetComponent<SpriteRenderer>();
    if (gameObject.tag == "hero") {
        mainScript = GetComponent<Hero>();
    }
  }

  void Update() {
    // if the character received damage, it will be invulnerable, only then should the flicker happen
    // if (mainScript.isInvulnerable) {
        // Calculate the alpha value based on time.
        float alpha = Mathf.PingPong(Time.time * flickerSpeed, 1f);

        // Update the alpha value of the sprite renderer.
        characterRenderer.color = new Color(1f, 1f, 1f, alpha);
    // }
  }
}
