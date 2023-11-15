using System.Collections.Generic;

public class Chat {
  public static Condition blankCondition = new Condition() {
    conditionCheck = "",
    conditionValue = ""
  };

  public static Dictionary<string, Dictionary<string, ChatNode>> chatNodes = new Dictionary<string, Dictionary<string, ChatNode>> {
    {"peasant-girl", new Dictionary<string, ChatNode> {
      {"", new ChatNode() {
        nodeCondition = blankCondition,
        nodeLines = new ChatLine[] {
          new ChatLine() {
            character = "peasant-girl",
            emotion = "default",
            line = "Hi! Nice to meet ya! I'm a peasant girl! Here! Have a Pineapple and a Watermelon!",
            outcome = new Outcome() {
              outcomeCase = "give",
              outcomeValue = "pineapple,watermelon"
            }
          }
        },
        // nextNode = "emotion-dialogue",
        nextNode = "pay-dialogue",
        fallbackNode = ""
      }},
      {"pay-dialogue", new ChatNode() {
        nodeCondition = new Condition() {
          conditionCheck = "money",
          conditionValue = "1000"
        },
        nodeLines = new ChatLine[] {
          new ChatLine() {
            character = "peasant-girl",
            emotion = "happy",
            line = "Thank you! Here's an elixir",
            outcome = new Outcome() {
              outcomeCase = "trade",
              outcomeValue = "money-1000|elixir"
            }
          }
        },
        nextNode = "emotion-dialogue",
        fallbackNode = "no-pay-dialogue"
      }},
      {"no-pay-dialogue", new ChatNode() {
        nodeCondition = blankCondition,
        nodeLines = new ChatLine[] {
          new ChatLine() {
            character = "peasant-girl",
            emotion = "default",
            line = "If you bring me $1000 I'll give you something special",
            outcome = new Outcome() {
              outcomeCase = "",
              outcomeValue = ""
            }
          }
        },
        nextNode = "pay-dialogue",
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
            line = "Hi again! I'm a happy peasant girl!!",
            outcome = new Outcome() {
              outcomeCase = "trade",
              outcomeValue = "draco-shield|money-5000"
            }
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "confused",
            line = "I'm not sure if I'm ever going to be given a name...",
            outcome = new Outcome() {
              outcomeCase = "",
              outcomeValue = ""
            }
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "sad",
            line = "It would make me sad if I become just another random NPC,",
            outcome = new Outcome() {
              outcomeCase = "",
              outcomeValue = ""
            }
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "lonely",
            line = "Heck, in fact, it would make me feel very lonely...",
            outcome = new Outcome() {
              outcomeCase = "",
              outcomeValue = ""
            }
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "ashamed",
            line = "That's not anachronical, is it? I'd be embarrassed if it were!",
            outcome = new Outcome() {
              outcomeCase = "",
              outcomeValue = ""
            }
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "afraid",
            line = "I fear that if I fall out of line, I'd be deleted...",
            outcome = new Outcome() {
              outcomeCase = "",
              outcomeValue = ""
            }
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "angry",
            line = "Why should I care? The game will miss out if they don't have me!!!",
            outcome = new Outcome() {
              outcomeCase = "",
              outcomeValue = ""
            }
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "default",
            line = "But enough about that. What should I tell you about myself?",
            outcome = new Outcome() {
              outcomeCase = "",
              outcomeValue = ""
            }
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "surprised",
            line = "I think there's plans to make me a bigger character! Will it be moral support? Comic relief?",
            outcome = new Outcome() {
              outcomeCase = "",
              outcomeValue = ""
            }
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "affective",
            line = "I would looove to be involved in some form of romance, not sure if with the main character...",
            outcome = new Outcome() {
              outcomeCase = "",
              outcomeValue = ""
            }
          },
          new ChatLine() {
            character = "peasant-girl",
            emotion = "jealous",
            line = "I just hope with all my heart that I'm not part of some form of love triangle!",
            outcome = new Outcome() {
              outcomeCase = "give",
              outcomeValue = "apple"
            }
          }
        },
        nextNode = "emotion-dialogue",
        fallbackNode = "ask-draco-shield"
      }},
      {"ask-draco-shield", new ChatNode() {
        nodeCondition = blankCondition,
        nodeLines = new ChatLine[] {
          new ChatLine() {
            character = "peasant-girl",
            emotion = "default",
            line = "If you bring me the Draco Shield I'll show you my array of emotions...",
            outcome = new Outcome() {
              outcomeCase = "",
              outcomeValue = ""
            }
          }
        },
        nextNode = "emotion-dialogue",
        fallbackNode = ""
      }},
    }}
  };
}
