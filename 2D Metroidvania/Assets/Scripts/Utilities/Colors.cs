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
  public static Color hpDecrement = new Color(1, 0, 0);
  public static Color barHPAbove40 = new Color(25f/255f, 159f/255f, 19f/255f);
  public static Color barHPAbove20 = new Color(197f/255f, 214f/255f, 94f/255f);
  public static Color barHPBelow20 = new Color(173f/255f, 45f/255f, 51f/255f);

  public static Color miniHPFull = new Color(1, 1, 1);
  public static Color miniHPNotFull = new Color(195f/255f, 200f/255f, 150f/255f);
  public static Color miniHPBelow20 = new Color(165f/255f, 100f/255f, 115f/255f);

  public static Color criticalColorBottom = new Color(1, 0 , 0);
  public static Color criticalColorOutline = new Color(1, 1, 1);
  public static Color criticalColorTop = new Color(0, 0 , 0);

  public static Dictionary<string, Color> vaseColors = new Dictionary<string, Color> {
    {"gold", new Color(1, 1, 0.5f)},
    {"bronze", new Color(0.31f, 0.2f, 0.08f)},
    {"brass", new Color(0.88f, 0.76f, 0.43f)},
    {"silver", new Color(0.75f, 0.75f, 0.75f)}
  };

  public static Dictionary<string, Color> chestColors = new Dictionary<string, Color> {
    {"brown", new Color(148f/255f, 107f/255f, 80f/255f)},
    {"green", new Color(47f/255f, 141f/255f, 69f/255f)},
    {"blue", new Color(0, 89f/255f, 155f/255f)},
    {"purple", new Color(121f/255f, 89f/255f, 183f/255f)},
    {"red", new Color(158f/255f, 31f/255f, 45f/255f)},
    {"cyan", new Color(1f/255f, 139f/255f, 252f/255f)},
    {"ash", new Color(104f/255f, 100f/255f, 126f/255f)},
    {"ruby", new Color(237f/255f, 73f/255f, 78f/255f)},
    {"zaphire", new Color(28f/255f, 187f/255f, 1)},
    {"opal", new Color(250f/255f, 172f/255f, 15f/255f)},
    {"rock", new Color(50f/255f, 50f/255f, 50f/255f)},
    {"emerald", new Color(28f/255f, 208f/255f, 2f/255f)}
    // {"", new Color()}
  };
}
