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

  private Hero hero;

  void Chat() {
    SetCharacter(chatLines[lineIndex].character);
    SetEmotion(chatLines[lineIndex].character, chatLines[lineIndex].emotion);
    RunOutcome(chatLines[lineIndex].outcome);
    ClearText();
  }
  void Start() {
    characterComponent = characterObject.GetComponent<Text>();
    textComponent = textObject.GetComponent<Text>();
    hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();

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
    if (characterComponent != null) {
      lineIndex = 0;
      Chat();
      if (textComponent != null) {
        StartCoroutine(ShowLine());
      }
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

  void GiveItem(Hero heroScript, string itemKey) {
    Item currItem = Helpers.GetItemFromList(heroScript.items, itemKey);

    if (itemKey.Contains("money")) {
      heroScript.gold += Objects.moneyItems[itemKey].increment;
    } else {
      if (currItem == null) { // if not found, the item must be added
        heroScript.items.Add(new Item(itemKey, 1));
      } else { // if found, the item is incremented
        currItem.amount++;
      }
    }

    if (Settings.showItemInfo) {
      bool displayMoney = itemKey.Contains("money");
      heroScript.infoCanvas.GetComponent<InfoCanvas>().Display(displayMoney ? Objects.moneyItems[itemKey].text : Objects.pauseItems[itemKey].name);
    }
  }

  void TakeItem(Hero heroScript, string itemKey) {
    Vector2 lossPosition = new Vector2((heroScript.transform.position.x + (heroScript.heroWidth / 2)) * heroScript.direction, heroScript.transform.position.y + heroScript.heroHeight);

    if (itemKey.Contains("money")) { // if there is money involved, remove from the gold value
      string moneyValue = itemKey.Split('-')[1];
      heroScript.gold -= int.Parse(moneyValue);

      heroScript.InstantiateLoss(lossPosition, "money-loss", isItem: false, moneyValue, null);
    } else { // if there is no money involved, remove from the hero item list
      Item currItem = Helpers.GetItemFromList(heroScript.items, itemKey);

      if (currItem.amount > 1) { // if more than one, just decrement
        currItem.amount--;
      } else { // otherwise, remove it from the item list
        heroScript.RemoveItem(Helpers.GetItemIndex(heroScript.items, itemKey));
      }

      // TODO: if at some point the player has to give more than 2 of the same item, the multiplier text should reflect this
      heroScript.InstantiateLoss(lossPosition, "item-loss", isItem: true, "", Objects.pauseItems[itemKey].thumbnail);
    }
  }

  void RunOutcome(Outcome outcome) {
    Hero heroScript = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();

    switch (outcome.outcomeCase) {
      case "":
        // do nothing
      return;
      case "give":
        string[] itemKeys = outcome.outcomeValue.Split(',');

        foreach (string itemKey in itemKeys) {
          GiveItem(heroScript, itemKey);
        }
      break;
      case "trade":
        string[] outcomeValues = outcome.outcomeValue.Split('|'); // splits the outcome value by | in two, where the left part is what the hero gives, and the right side is what the NPC gives
        string[] heroItems = outcomeValues[0].Split(',');
        string[] npcItems = outcomeValues[1].Split(',');

        foreach (string item in heroItems) {
          TakeItem(heroScript, item);
        }

        foreach(string item in npcItems) {
          GiveItem(heroScript, item);
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
      Chat();
      StartCoroutine(ShowLine());
    } else { // if there are no more lines, hide the chat window
      FinishChat(playerLeft: false);
    }
  }

  // Finishes the chat, but only sets the next node if the player left, otherwise a line could potentially not be read by player
  public void FinishChat(bool playerLeft = false) {
    if (!playerLeft) {
      hero.UpdateChatNode(startingNPC, nextNode);
    }

    SetEmotion(startingNPC, "default");
    hero.CloseChat();
  }
}
