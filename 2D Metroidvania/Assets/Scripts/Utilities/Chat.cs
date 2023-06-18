using System.Collections.Generic;

public class Chat {
  public static Dictionary<string, Dictionary<string, ChatNode>> chatNodes = new Dictionary<string, Dictionary<string, ChatNode>> {
    {"peasant-girl", new Dictionary<string, ChatNode> {
      {"", new ChatNode() {
        nodeCondition = new Condition() {
          conditionCheck = "",
          conditionValue = ""
        },
        nodeLines = new ChatLine[] {
          new ChatLine() {
            character = "Peasant Girl",
            emotion = "meh",
            line = "Hi! Nice to meet ya! I'm a peasant girl!"
          }
        },
        nextNode = "emotion-dialogue",
        fallbackNode = ""
      }},
      {"emotion-dialogue", new ChatNode() {
        nodeCondition = new Condition() {
          conditionCheck = "items",
          conditionValue = "draco-shield"
        },
        nodeLines = new ChatLine[] {
          new ChatLine() {
            character = "Peasant Girl",
            emotion = "meh",
            line = "I have no emotions yet!"
          }
        },
        nextNode = "emotion-dialogue",
        fallbackNode = "ask-draco-shield"
      }},
      {"ask-draco-shield", new ChatNode() {
        nodeCondition = new Condition() {
          conditionCheck = "",
          conditionValue = ""
        },
        nodeLines = new ChatLine[] {
          new ChatLine() {
            character = "Peasant Girl",
            emotion = "meh",
            line = "If you bring me the draco shield I'll show you my array of emotions..."
          }
        },
        nextNode = "emotion-dialogue",
        fallbackNode = ""
      }},
    }}
  };
}
