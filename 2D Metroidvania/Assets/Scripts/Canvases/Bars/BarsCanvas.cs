using UnityEngine.UI;
using UnityEngine;

public class BarsCanvas : MonoBehaviour {
  [SerializeField] GameObject hpBackground;
  [SerializeField] GameObject hpForeground;
  [SerializeField] GameObject hpBar;

  [SerializeField] GameObject mpBackground;
  [SerializeField] GameObject mpForeground;
  [SerializeField] GameObject mpBar;

  [Header("MP Display Container")]
  [SerializeField] GameObject mpPanelBackground;
  [SerializeField] GameObject mpPanelForeground;
  [SerializeField] GameObject mpBarContainer;
  [SerializeField] bool mpBarDisplaying = false;

  [System.NonSerialized] Hero hero;
  [System.NonSerialized] int currentHPWidth = -1;
  [System.NonSerialized] int currentMPWidth = -1;
  [System.NonSerialized] int maxHPWidth = -1;
  [System.NonSerialized] int maxMPWidth = -1;
  void Start() {
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
  }
  void Update() {
    CheckMPBarDisplay();

    UpdateHPContainer();
    UpdateHPBar();

    UpdateMPContainer();
    UpdateMPBar();
  }

  public void CheckMPBarDisplay() {
    if (hero.canCastMagic && !mpBarDisplaying) {
      DisplayMPBar();
      mpBarDisplaying = true;
    }
  }

  public void UpdateHPContainer() {
    if (maxHPWidth != hero.maxHP) {
      maxHPWidth = hero.maxHP;

      // obtains a dimension that is dependent on the hp value
      Vector2 containerDimension = new Vector2((maxHPWidth > Constants.maxHPDisplayableLimit ? Constants.maxHPDisplayableLimit : maxHPWidth) * Constants.containerMultiplier, 36);
      containerDimension.x -= Constants.hpAdjustDifference;

      hpBackground.GetComponent<RectTransform>().sizeDelta = containerDimension;
      hpForeground.GetComponent<RectTransform>().sizeDelta = containerDimension;
    }
  }

  public void UpdateHPBar() {
    if (currentHPWidth != hero.currentHP) {
      currentHPWidth = hero.currentHP;
      float calculatedHPDisplay = maxHPWidth > Constants.maxHPDisplayableLimit ? Constants.maxHPDisplayableLimit * ((float)currentHPWidth / (float)maxHPWidth) : currentHPWidth;
      Vector2 hpDisplayVector = new Vector2(calculatedHPDisplay, 27);
      hpDisplayVector.x *= Constants.containerMultiplier;
      hpBar.GetComponent<RectTransform>().sizeDelta = hpDisplayVector;

      float healthPercentage = (float)hero.currentHP / (float)hero.maxHP;

      if (healthPercentage >= 0.4f) {
        hpBar.GetComponent<Image>().color = Colors.barHPAbove40;
      } else if (healthPercentage > 0.2f) {
        hpBar.GetComponent<Image>().color = Colors.barHPAbove20;
      } else {
        hpBar.GetComponent<Image>().color = Colors.barHPBelow20;
      }
    }
  }

  public void UpdateMPContainer() {
    if (maxMPWidth != hero.maxMP) {
      maxMPWidth = hero.maxMP;

      // obtains a dimension that is dependent on the mp value
      Vector2 containerDimension = new Vector2((maxMPWidth > Constants.maxMPDisplayableLimit ? Constants.maxMPDisplayableLimit : maxMPWidth) * Constants.containerMultiplier, 16);
      containerDimension.x -= Constants.mpAdjustDifference;

      mpBackground.GetComponent<RectTransform>().sizeDelta = containerDimension;
      mpForeground.GetComponent<RectTransform>().sizeDelta = containerDimension;
    }
  }

  public void UpdateMPBar() {
    if (currentMPWidth != hero.currentMP) {
      currentMPWidth = hero.currentMP;
      float calculatedMPDisplay = maxMPWidth > Constants.maxMPDisplayableLimit ? Constants.maxMPDisplayableLimit * ((float)currentMPWidth / (float)maxMPWidth) : currentMPWidth;
      Vector2 mpDisplayVector = new Vector2(calculatedMPDisplay, 9);
      mpDisplayVector.x *= Constants.containerMultiplier;
      mpBar.GetComponent<RectTransform>().sizeDelta = mpDisplayVector;
    }
  }

  // The bar has to be displayed with alpha 1 to simulate it shows
  public void DisplayMPBar() {
    GameObject[] MPDisplayContainers = {mpPanelBackground, mpPanelForeground, mpBarContainer};
    foreach(GameObject currMPDisplayContainer in MPDisplayContainers) {
      foreach(Transform child in currMPDisplayContainer.transform) {
        Color currColor = child.GetComponent<Image>().color;
        Color newColor = new Color(currColor.r, currColor.g, currColor.b, 1);
        child.GetComponent<Image>().color = newColor;
      }
    }
  }
}
