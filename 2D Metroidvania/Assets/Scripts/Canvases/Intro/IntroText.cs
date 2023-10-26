using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroText : MonoBehaviour {
  [SerializeField] float textSpeed;
  private Text textComponent;
  private string introText;

  void Start() {
    textComponent = GetComponent<Text>();
    introText = GameData.introText;
    StartIntroText();
  }

  public void StartIntroText() {
    StartCoroutine(ShowText());
  }

  IEnumerator ShowText() {
    foreach (char c in introText.ToCharArray()) {
      if (textComponent.text.Length < introText.Length) {
        textComponent.text += c;
        yield return new WaitForSeconds(textSpeed);
      }
    }
  }

  public void ShowCompleteText() {
    textComponent.text = introText;
  }

  void Update() {}
}
