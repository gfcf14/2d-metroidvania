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
            character = "peasant-girl",
            emotion = "default",
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
            character = "peasant-girl",
            emotion = "happy",
            line = "Hi again! I'm a happy peasant girl!!"
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "confused",
            line = "I'm not sure if I'm ever going to be given a name..."
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "sad",
            line = "It would make me sad if I become just another random NPC,"
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "lonely",
            line = "Heck, in fact, it would make me feel very lonely..."
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "ashamed",
            line = "That's not anachronical, is it? I'd be embarrassed if it were!"
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "afraid",
            line = "I fear that if I fall out of line, I'd be deleted..."
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "angry",
            line = "Why should I care? The game will miss out if they don't have me!!!"
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "default",
            line = "But enough about that. What should I tell you about myself?"
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "surprised",
            line = "I think there's plans to make me a bigger character! Will it be moral support? Comic relief?"
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "affective",
            line = "I would looove to be involved in some form of romance, not sure if with the main character..."
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "jealous",
            line = "I just hope with all my heart that I'm not part of some form of love triangle!"
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
            character = "peasant-girl",
            emotion = "default",
            line = "If you bring me the Draco Shield I'll show you my array of emotions..."
          }
        },
        nextNode = "emotion-dialogue",
        fallbackNode = ""
      }},
    }}
  };
}
