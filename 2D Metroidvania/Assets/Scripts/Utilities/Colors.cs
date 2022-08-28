using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors {
  public static Dictionary<string, Color> elementResistancesColors = new Dictionary<string, Color> {
    {"fire", new Color(0.9f, 0, 0)},
    {"poison", new Color(0.7f, 0.86f, 0.19f)}
  };

  public static Dictionary<string, Color> statusColors = new Dictionary<string, Color> {
    {"burned", new Color(0.3f, 0.3f, 0.3f)},
    {"poisoned", new Color(0.4f, 0, 0.4f)}
  };
}
