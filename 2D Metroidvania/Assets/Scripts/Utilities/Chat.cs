using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat {
  // TODO: change dictionary value to ChatNode[]
  public static Dictionary<string, ChatLine[]> chatNodes = new Dictionary<string, ChatLine[]> {
    {"peasant-girl", new ChatLine[]{
      new ChatLine(){character="Peasant Girl", emotion="meh", line="Hi! Nice to meet ya! I'm a peasant girl!"},
      new ChatLine(){character="Peasant Girl", emotion="meh", line="How can I help you today?"}
    }}
  };
}
