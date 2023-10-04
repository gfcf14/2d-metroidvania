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
      int currentHP = maxHPWidth - Constants.existingHPWidth;

      Vector2 containerDimension = new Vector2(maxHPWidth > Constants.maxHPDisplayableLimit ? Constants.hpContainerMaxWidth : currentHP, 36);

      hpBackground.GetComponent<RectTransform>().sizeDelta = containerDimension;
      hpForeground.GetComponent<RectTransform>().sizeDelta = containerDimension;
    }
  }

  public void UpdateHPBar() {
    if (currentHPWidth != hero.currentHP) {
      currentHPWidth = hero.currentHP;
      float calculatedHPDisplay = maxHPWidth > Constants.maxHPDisplayableLimit ? Constants.maxHPDisplayableLimit * ((float)currentHPWidth / (float)maxHPWidth) : currentHPWidth;
      hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(calculatedHPDisplay - Constants.hpAdjustDifference, 27);

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
      int currentMP = maxMPWidth - Constants.existingMPWidth;

      Vector2 containerDimension = new Vector2(maxMPWidth > Constants.maxMPDisplayableLimit ? Constants.mpContainerMaxWidth : currentMP, 16);

      mpBackground.GetComponent<RectTransform>().sizeDelta = containerDimension;
      mpForeground.GetComponent<RectTransform>().sizeDelta = containerDimension;
    }
  }

  public void UpdateMPBar() {
    if (currentMPWidth != hero.currentMP) {
      currentMPWidth = hero.currentMP;
      float calculatedMPDisplay = maxMPWidth > Constants.maxMPDisplayableLimit ? Constants.maxMPDisplayableLimit * ((float)currentMPWidth / (float)maxMPWidth) : currentMPWidth;
      mpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(calculatedMPDisplay - Constants.mpAdjustDifference, 9);
    }
  }
}
