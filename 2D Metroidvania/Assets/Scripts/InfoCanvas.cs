using UnityEngine;
using UnityEngine.UI;

public class InfoCanvas : MonoBehaviour {
  [SerializeField] GameObject textObject;
  [SerializeField] GameObject infoContainerObject;

  [System.NonSerialized] float maxDisplayTime = 2000;
  [System.NonSerialized] public float startTime = 0;
  void Start() {}

  void Update() {
    float currentTime = Time.time * 1000;

    if (currentTime > startTime + maxDisplayTime) {
      gameObject.SetActive(false);
    }
  }

  public void Display(string text) {
    int textAndContainerWidth = text.Length > 2 ? text.Length * Constants.characterWidth : (text.Length == 1 ? 40 : 48);

    gameObject.SetActive(false);

    textObject.GetComponent<RectTransform>().sizeDelta = new Vector2(textAndContainerWidth, 37.9686f);
    infoContainerObject.GetComponent<RectTransform>().sizeDelta = new Vector2(textAndContainerWidth, 128);
    textObject.GetComponent<Text>().text = text;

    gameObject.SetActive(true);
    startTime = Time.time * 1000;
  }
}
