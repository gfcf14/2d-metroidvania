using UnityEngine;
using UnityEngine.UI;

public class InfoCanvas : MonoBehaviour {
  [SerializeField] GameObject textObject;
  [SerializeField] GameObject layoutObject;
  [SerializeField] GameObject infoContainerObject;

  [SerializeField] GameObject enemyHPContainer;
  [SerializeField] GameObject enemyHPBackground;
  [SerializeField] GameObject enemyHPForeground;

  [System.NonSerialized] float maxDisplayTime = 2000;
  [System.NonSerialized] public float startTime = 0;

  private Hero heroScript;
  private HorizontalLayoutGroup infoLayout;
  private RectTransform infoRect;

  void Start() {
    SetComponents();
  }

  public void SetComponents() {
    infoLayout = layoutObject.GetComponent<HorizontalLayoutGroup>();
    infoRect = infoContainerObject.GetComponent<RectTransform>();
    heroScript = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
  }

  void Update() {
    if (Helpers.ExceedsTime(startTime, maxDisplayTime)) {
      gameObject.SetActive(false);
      enemyHPContainer.SetActive(false);
    }
  }

  public void Display(string text, EnemyHealth enemyHealth = null) {
    if (!heroScript) {
      SetComponents();
    }

    int textAndContainerWidth = 0;

    foreach(char currCharacter in text) {
      textAndContainerWidth += Helpers.GetCharacterDisplayWidth(currCharacter);
    }

    gameObject.SetActive(false);
    enemyHPContainer.SetActive(false);

    textObject.GetComponent<RectTransform>().sizeDelta = new Vector2(textAndContainerWidth, 37.9686f);
    infoRect.sizeDelta = new Vector2(textAndContainerWidth, 128);
    textObject.GetComponent<Text>().text = text;

    // immediately align right if the action canvas is already visible
    if (heroScript.actionCanvas.activeSelf) {
      AlignRight();
    }

    gameObject.SetActive(true);
    startTime = Time.time * 1000;

    if (enemyHealth != null) {
      int enemyHPBackgroundWidth = 80 + textAndContainerWidth; // 80 being the info bar edge widths

      enemyHPBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(enemyHPBackgroundWidth, 10);
      enemyHPForeground.GetComponent<RectTransform>().sizeDelta = new Vector2(enemyHPBackgroundWidth * ((float)enemyHealth.current / (float)enemyHealth.maximum), 10);
      enemyHPContainer.SetActive(true);
    }
  }

  public void AlignRight() {
    RectOffset newPadding = infoLayout.padding;
    newPadding.right = (int)(infoRect.sizeDelta.x) + Constants.infoCanvasRightAlignOffset;
    infoLayout.padding = newPadding;

    infoLayout.childAlignment = TextAnchor.LowerRight;
  }

  public void AlignLeft() {
    infoLayout.childAlignment = TextAnchor.LowerLeft;
  }
}
