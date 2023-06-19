using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatCanvas : MonoBehaviour {
  [SerializeField] GameObject characterObject;
  [SerializeField] GameObject textObject;
  [SerializeField] GameObject continuePrompt;
  [SerializeField] public ChatLine[] chatLines;
  [SerializeField] public string startingNPC;
  [SerializeField] public string nextNode;
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
      // if the entire text is on screen, get the next line
      if (textComponent.text == chatLines[lineIndex].line) {
        NextLine();
      } else { // otherwise, show the entire line right away
        StopAllCoroutines();
        textComponent.text = chatLines[lineIndex].line;
        continuePrompt.SetActive(true);
      }
    }
  }

  public void StartChat() {
    lineIndex = 0;
    SetCharacter(chatLines[lineIndex].character);
    SetEmotion(chatLines[lineIndex].character, chatLines[lineIndex].emotion);
    ClearText();
    if (textComponent != null) {
      StartCoroutine(ShowLine());
    }
  }

  // adds a line character by character based on the textSpeed
  IEnumerator ShowLine() {
    foreach (char c in chatLines[lineIndex].line.ToCharArray()) {
      textComponent.text += c;
      if(textComponent.text.Length == chatLines[lineIndex].line.Length) {
        continuePrompt.SetActive(true);
      } else {
        GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().UpdateChatNode(startingNPC, nextNode);
        continuePrompt.SetActive(false);
      }
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
      characterComponent.text = Helpers.KebabToCharacter(character) + ":";
    }
  }

  void SetEmotion(string character, string emotion) {
    GameObject.Find(Helpers.KebabToObject(character)).GetComponent<SpriteRenderer>().sprite = Sprites.emotions[character][emotion];
  }

  void NextLine() {
    if (lineIndex < chatLines.Length - 1) {
      lineIndex++;
      SetCharacter(chatLines[lineIndex].character);
      SetEmotion(chatLines[lineIndex].character, chatLines[lineIndex].emotion);
      ClearText();
      StartCoroutine(ShowLine());
    } else { // if there are no more lines, hide the chat window
      gameObject.SetActive(false);
    }
  }
}
