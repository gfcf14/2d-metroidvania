using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatCanvas : MonoBehaviour {
  [SerializeField] GameObject characterObject;
  [SerializeField] GameObject textObject;
  [SerializeField] public ChatLine[] chatLines;
  [SerializeField] float textSpeed;

  private int lineIndex;
  private Text characterComponent;
  private Text textComponent;
  void Start() {
    characterComponent = characterObject.GetComponent<Text>();
    textComponent = textObject.GetComponent<Text>();

    StartChat();
  }
  void Update() {
    if (Helpers.IsKeyUp(Controls.currentKeyboardAction) || Helpers.IsKeyUp(Controls.currentGamepadAction)) {
      if (textComponent.text == chatLines[lineIndex].line) {
        NextLine();
      } else {
        StopAllCoroutines();
        textComponent.text = chatLines[lineIndex].line;
      }
    }
  }

  public void StartChat() {
    lineIndex = 0;
    SetCharacter(chatLines[lineIndex].character);
    ClearText();
    if (textComponent != null) {
      StartCoroutine(ShowLine());
    }
  }

  IEnumerator ShowLine() {
    foreach (char c in chatLines[lineIndex].line.ToCharArray()) {
      textComponent.text += c;
      yield return new WaitForSeconds(textSpeed);
    }
  }

  void ClearText() {
    if (textComponent != null) {
      textComponent.text = "";
    }
  }

  void SetCharacter(string character) {
    if (characterComponent != null) {
      characterComponent.text = character + ":";
    }
  }

  void NextLine() {
    if (lineIndex < chatLines.Length - 1) {
      lineIndex++;
      SetCharacter(chatLines[lineIndex].character);
      ClearText();
      StartCoroutine(ShowLine());
    } else {
      gameObject.SetActive(false);
    }
  }
}
