using UnityEngine;

public class DamageFlicker : MonoBehaviour {
  public float flickerSpeed = 16f;
  private SpriteRenderer characterRenderer;
  private MonoBehaviour mainScript;
  private string characterTag;
  void Start() {
    characterRenderer = GetComponent<SpriteRenderer>();
    characterTag  = gameObject.tag;

    if (characterTag == "Hero") {
        mainScript = GetComponent<Hero>();
    }
  }

  void ChangeColor() {
    // Calculate the alpha value based on time.
    float alpha = Mathf.PingPong(Time.time * flickerSpeed, 1f);

    // Update the alpha value of the sprite renderer.
    characterRenderer.color = new Color(1f, 1f, 1f, alpha);
  }

  void Update() {
    // if the character received damage, it will be invulnerable, only then should the flicker happen
    if (characterTag == "Hero" && ((Hero)mainScript).isInvulnerable) { // TODO: add logic for other classes that would require invulnerability (like maybe NPCs?)
      ChangeColor();
    }
  }
}
