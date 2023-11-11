using TMPro;
using UnityEngine;

public class DamageContainer : MonoBehaviour {
  [System.NonSerialized] public int damage;
  [System.NonSerialized] public bool isCritical;
  [System.NonSerialized] public string soundType;

  private AudioSource audioSource;
  private bool mustDestroy = false;
  void Start() {
    audioSource = GetComponent<AudioSource>();

    if (soundType != null && Settings.playSFX) {
      audioSource.PlayOneShot(Sounds.impactSounds[soundType][isCritical ? "critical" : "normal"]);
    }

    GetComponent<Animator>().enabled = true;

    TextMeshPro textElement = transform.Find("DamageText").gameObject.GetComponent<TextMeshPro>();
    textElement.text = damage.ToString() + (isCritical ? "!" : "");
    if (isCritical) {
      transform.Find("DamageText").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1.5f, 0.5645f);
      textElement.font = Objects.fonts["levi-rebrushed"];
      textElement.UpdateFontAsset();
    }

    if (isCritical) {
      textElement.colorGradient = new VertexGradient(Colors.criticalColorTop, Colors.criticalColorTop, Colors.criticalColorBottom, Colors.criticalColorBottom);
      textElement.fontSize = 8;
      textElement.outlineWidth = 0.2f;
      textElement.outlineColor = Colors.criticalColorOutline;
    }
  }

  void Update() {
    if (!audioSource.isPlaying && mustDestroy) {
      Destroy(gameObject);
    }
  }

  public void DestroyDamageContainer() {
    mustDestroy = true;
  }
}
