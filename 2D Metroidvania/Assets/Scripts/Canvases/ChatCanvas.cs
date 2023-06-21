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

  void RunOutcome(Outcome outcome) {
    switch (outcome.outcomeCase) {
      case "":
        // do nothing
        return;
      break;
      case "give":
        Hero heroScript = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
        string itemKey = outcome.outcomeValue;

        Item currItem = Helpers.GetItemFromList(heroScript.items, itemKey);

        if (currItem == null) { // if not found, the item must be added
          heroScript.items.Add(new Item(itemKey, 1));
        } else { // if found, the item is incremented
          currItem.amount++;
        }

        if (Settings.showItemInfo) {
          heroScript.infoCanvas.GetComponent<InfoCanvas>().Display(itemKey.Contains("money") ? Objects.moneyItems[itemKey].text : Objects.pauseItems[itemKey].name);
        }
      break;
      default:
        Debug.Log("Unknown outcome case: case=" + outcome.outcomeCase + "value=" + outcome.outcomeValue);
        break;
    }
  }

  void NextLine() {
    if (lineIndex < chatLines.Length - 1) {
      lineIndex++;
      SetCharacter(chatLines[lineIndex].character);
      SetEmotion(chatLines[lineIndex].character, chatLines[lineIndex].emotion);
      RunOutcome(chatLines[lineIndex].outcome);
      ClearText();
      StartCoroutine(ShowLine());
    } else { // if there are no more lines, hide the chat window
      FinishChat(playerLeft: false);
    }
  }

  // Finishes the chat, but only sets the next node if the player left, otherwise a line could potentially not be read by player
  public void FinishChat(bool playerLeft = false) {
    if (!playerLeft) {
      GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>().UpdateChatNode(startingNPC, nextNode);
    }

    SetEmotion(startingNPC, "default");
    gameObject.SetActive(false);
  }
}
