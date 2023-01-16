using UnityEngine;
using UnityEngine.UI;

public class InfoCanvas : MonoBehaviour {
  [SerializeField] GameObject textObject;
  [SerializeField] GameObject infoContainerObject;

  [SerializeField] GameObject enemyHPContainer;
  [SerializeField] GameObject enemyHPBackground;
  [SerializeField] GameObject enemyHPForeground;

  [System.NonSerialized] float maxDisplayTime = 2000;
  [System.NonSerialized] public float startTime = 0;

  void Start() {}

  void Update() {
    if (Helpers.ExceedsTime(startTime, maxDisplayTime)) {
      gameObject.SetActive(false);
      enemyHPContainer.SetActive(false);
    }
  }

  public void Display(string text, EnemyHealth enemyHealth = null) {
    int textAndContainerWidth = text.Length > 2 ? text.Length * Constants.characterWidth : (text.Length == 1 ? 40 : 48);

    gameObject.SetActive(false);
    enemyHPContainer.SetActive(false);

    textObject.GetComponent<RectTransform>().sizeDelta = new Vector2(textAndContainerWidth, 37.9686f);
    infoContainerObject.GetComponent<RectTransform>().sizeDelta = new Vector2(textAndContainerWidth, 128);
    textObject.GetComponent<Text>().text = text;

    gameObject.SetActive(true);
    startTime = Time.time * 1000;

    if (enemyHealth != null) {
      int enemyHPBackgroundWidth = 80 + textAndContainerWidth; // 80 being the info bar edge widths

      enemyHPBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(enemyHPBackgroundWidth, 10);
      enemyHPForeground.GetComponent<RectTransform>().sizeDelta = new Vector2(enemyHPBackgroundWidth * ((float)enemyHealth.current / (float)enemyHealth.maximum), 10);
      enemyHPContainer.SetActive(true);
    }
  }
}
