using TMPro;
using UnityEngine;

public class BarCanvasMini : MonoBehaviour {
  [SerializeField] GameObject hpObject;
  [SerializeField] GameObject mpObject;

  [System.NonSerialized] Hero hero;
  [System.NonSerialized] int hp = -1;
  [System.NonSerialized] int hpDifference = 0;
  [System.NonSerialized] int mp = -1;
  [System.NonSerialized] int mpDifference = 0;

  void Start() {
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
    hp = hero.currentHP;
    mp = hero.currentMP;

    hpObject.GetComponent<TextMeshProUGUI>().text = hp.ToString();
    mpObject.GetComponent<TextMeshProUGUI>().text = mp.ToString();

    UpdateColor(hpObject, hp, "hp");
    UpdateColor(mpObject, mp, "mp");
  }

  void Update() {
    UpdateHP();
    UpdateMP();
  }

  public void UpdateHP() {
    if (hp != hero.currentHP && hpDifference == 0) {
      hpDifference = hp - hero.currentHP;
    }

    if (hpDifference != 0) {
      int difference = hpDifference > 0 ? -1 : 1;

      hpDifference += difference;
      hp += difference;
      hpObject.GetComponent<TextMeshProUGUI>().text = hp.ToString();

      UpdateColor(hpObject, hp, "hp");
    }
  }

  public void UpdateMP() {
    if (mp != hero.currentMP && mpDifference == 0) {
      mpDifference = mp - hero.currentMP;
    }

    if (mpDifference != 0) {
      int difference = mpDifference > 0 ? -1 : 1;

      mpDifference += difference;
      mp += difference;
      mpObject.GetComponent<TextMeshProUGUI>().text = mp.ToString();

      UpdateColor(mpObject, mp, "mp");
    }
  }

  public void UpdateColor(GameObject statObject, int stat, string type) {
    float percentage = (float)stat / (type == "hp" ? hero.maxHP : hero.maxMP);

    if (percentage == 1) {
        statObject.GetComponent<TextMeshProUGUI>().color = Colors.miniHPFull;
      } else if (percentage > 0.2f) {
        statObject.GetComponent<TextMeshProUGUI>().color = Colors.miniHPNotFull;
      } else {
        statObject.GetComponent<TextMeshProUGUI>().color = Colors.miniHPBelow20;
      }
  }
}
