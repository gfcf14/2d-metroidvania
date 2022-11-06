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

  public static Dictionary<string, Color> pauseStatsColors = new Dictionary<string, Color> {
    {"higher", new Color(0, 1, 0)},
    {"lower", new Color(1, 0, 0)}
  };

  public static Color pauseBackground = new Color(20f/255f, 44f/255f, 36f/255f);

  public static Dictionary<string, Color> vaseColors = new Dictionary<string, Color> {
    {"gold", new Color(1, 0.84f, 0)},
    {"bronze", new Color(0.31f, 0.2f, 0.08f)},
    {"aluminum", new Color(0.52f, 0.53f, 0.54f)},
    {"brass", new Color(0.88f, 0.76f, 0.43f)},
    {"steel", new Color(0.79f, 0.8f, 0.81f)},
    {"silver", new Color(0.75f, 0.75f, 0.75f)}
  };
}
