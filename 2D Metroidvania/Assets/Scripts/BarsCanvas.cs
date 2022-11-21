using UnityEngine.UI;
using UnityEngine;

public class BarsCanvas : MonoBehaviour {
  [SerializeField] GameObject hpBackground;
  [SerializeField] GameObject hpForeground;
  [SerializeField] GameObject hpBar;

  [SerializeField] GameObject mpBackground;
  [SerializeField] GameObject mpForeground;
  [SerializeField] GameObject mpBar;

  [System.NonSerialized] Hero hero;
  [System.NonSerialized] int currentHPWidth = -1;
  [System.NonSerialized] int currentMPWidth = -1;
  [System.NonSerialized] int maxHPWidth = -1;
  [System.NonSerialized] int maxMPWidth = -1;
  void Start() {
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
  }
  void Update() {
    UpdateHPContainer();
    UpdateHPBar();

    UpdateMPContainer();
    UpdateMPBar();
  }

  public void UpdateHPContainer() {
    if (maxHPWidth != hero.maxHP) {
      maxHPWidth = hero.maxHP;

      Vector2 containerDimension = new Vector2(maxHPWidth - Constants.existingHPWidth, 36);
      Vector2 containerPosition = new Vector2(Constants.hpBarLeft, Constants.hpBarTop);

      hpBackground.GetComponent<RectTransform>().sizeDelta = containerDimension;
      hpForeground.GetComponent<RectTransform>().sizeDelta = containerDimension;
    }
  }

  public void UpdateHPBar() {
    if (currentHPWidth != hero.currentHP) {
      Debug.Log("currentHPWidth: " + currentHPWidth + ", hero.currentHP: " + hero.currentHP);
      currentHPWidth = hero.currentHP;

      hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(currentHPWidth * Constants.hpMultiplier, 27);
      Debug.Log(currentHPWidth * Constants.hpMultiplier);

      float healthPercentage = (float)hero.currentHP / (float)hero.maxHP;

      if (healthPercentage >= 0.4f) {
        hpBar.GetComponent<Image>().color = Colors.hpAbove40;
      } else if (healthPercentage > 0.2f) {
        hpBar.GetComponent<Image>().color = Colors.hpAbove20;
      } else {
        hpBar.GetComponent<Image>().color = Colors.hpBelow20;
      }
    }
  }

  public void UpdateMPContainer() {
    if (maxMPWidth != hero.maxMP) {
      maxMPWidth = hero.maxMP;

      Vector2 containerDimension = new Vector2(maxMPWidth - Constants.existingMPWidth, 16);

      mpBackground.GetComponent<RectTransform>().sizeDelta = containerDimension;
      mpForeground.GetComponent<RectTransform>().sizeDelta = containerDimension;
    }
  }

  public void UpdateMPBar() {
    if (currentMPWidth != hero.currentMP) {
      currentMPWidth = hero.currentMP;

      mpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(currentMPWidth * Constants.mpMultiplier, 9);
    }
  }
}
