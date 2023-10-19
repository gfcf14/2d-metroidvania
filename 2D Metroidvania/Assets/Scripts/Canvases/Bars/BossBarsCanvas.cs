using UnityEngine.UI;
using UnityEngine;

public class BossBarsCanvas : MonoBehaviour {
  [SerializeField] GameObject hpBackground;
  [SerializeField] GameObject hpForeground;
  [SerializeField] GameObject hpBar;
  [SerializeField] GameObject bossLabel;

  [System.NonSerialized] public Enemy boss;
  [System.NonSerialized] int currentHPWidth = -1;
  [System.NonSerialized] int maxHPWidth = -1;
  [System.NonSerialized] public string bossName = "";
  void Start() {
    bossLabel.GetComponent<Text>().text = bossName;
    Vector2 containerDimension = new Vector2((Constants.maxHPDisplayableLimit * Constants.containerMultiplier), 36);
    hpBackground.GetComponent<RectTransform>().sizeDelta = containerDimension;

    hpForeground.GetComponent<RectTransform>().sizeDelta = containerDimension;
  }
  void Update() {
    UpdateHPContainer();
    UpdateHPBar();
  }

  public void UpdateHPContainer() {
    if (maxHPWidth != boss.maxHP) {
      maxHPWidth = boss.maxHP;
    }
  }

  public void UpdateHPBar() {
    if (currentHPWidth != boss.currentHP) {
      currentHPWidth = boss.currentHP;
      hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2((Constants.maxHPDisplayableLimit * Constants.containerMultiplier) * ((float)currentHPWidth / (float)maxHPWidth), 27);

      float healthPercentage = (float)boss.currentHP / (float)boss.maxHP;

      if (healthPercentage >= 0.4f) {
        hpBar.GetComponent<Image>().color = Colors.barHPAbove40;
      } else if (healthPercentage > 0.2f) {
        hpBar.GetComponent<Image>().color = Colors.barHPAbove20;
      } else {
        hpBar.GetComponent<Image>().color = Colors.barHPBelow20;
      }
    }
  }
}
