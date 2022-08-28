using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprites {
  public static Sprite[] elements = Resources.LoadAll<Sprite>("Pause/elements");

  public static Dictionary<string, Sprite> magicResistances = new Dictionary<string, Sprite> {
    {"air", elements[0]},
    {"dark", elements[1]},
    {"earth", elements[2]},
    {"fire", elements[3]},
    {"ice", elements[4]},
    {"light", elements[5]},
    {"lightning", elements[6]},
    {"water", elements[7]}
  };

  public static Dictionary<string, Sprite> locationImages = new Dictionary<string, Sprite> {
    {"meadows", Resources.Load<Sprite>("Pause/Locations/meadows")}
  };

  public static Dictionary<string, Sprite> pauseAvatars = new Dictionary<string, Sprite> {
    {"equipment-1", Resources.Load<Sprite>("Pause/PlayerAvatars/equipment-1")}
  };

  public static Dictionary<string, Sprite> throwableSprites = new Dictionary<string, Sprite> {
    {"lance", Resources.Load<Sprite>("Sprites/lance")},
    {"knife", Resources.Load<Sprite>("Sprites/knife")},
    {"kunai", Resources.Load<Sprite>("Sprites/kunai")},
    {"shuriken-4", Resources.Load<Sprite>("Sprites/shuriken-4")},
    {"shuriken-6", Resources.Load<Sprite>("Sprites/shuriken-6")},
    {"hatchet", Resources.Load<Sprite>("Sprites/hatchet")},
    {"axe", Resources.Load<Sprite>("Sprites/axe")}
  };
}
