using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds {
  public static Dictionary<string, AudioClip[]> materialRunningSounds = new Dictionary<string, AudioClip[]> {
    {"dirt", new AudioClip[] {
      Resources.Load<AudioClip>("SFX/running/dirt/dirt-1"),
      Resources.Load<AudioClip>("SFX/running/dirt/dirt-2"),
      Resources.Load<AudioClip>("SFX/running/dirt/dirt-3"),
      Resources.Load<AudioClip>("SFX/running/dirt/dirt-4"),
      Resources.Load<AudioClip>("SFX/running/dirt/dirt-5"),
    }},
    {"grass", new AudioClip[] {
      Resources.Load<AudioClip>("SFX/running/grass/grass-1"),
      Resources.Load<AudioClip>("SFX/running/grass/grass-2"),
      Resources.Load<AudioClip>("SFX/running/grass/grass-3"),
      Resources.Load<AudioClip>("SFX/running/grass/grass-4"),
      Resources.Load<AudioClip>("SFX/running/grass/grass-5"),
    }}
  };

  public static Dictionary<string, AudioClip[]> breakableSounds = new Dictionary<string, AudioClip[]> {
    {"box", new AudioClip[] {
      Resources.Load<AudioClip>("SFX/breakables/box/breaking/breaking-1"),
      Resources.Load<AudioClip>("SFX/breakables/box/breaking/breaking-2"),
      Resources.Load<AudioClip>("SFX/breakables/box/breaking/breaking-3"),
    }}
  };

  public static Dictionary<string, Dictionary<string, AudioClip[]>> fallingSounds = new Dictionary<string, Dictionary<string, AudioClip[]>> {
    {"box", new Dictionary<string, AudioClip[]> {
      {"box", new AudioClip[] {
        Resources.Load<AudioClip>("SFX/breakables/box/falling/on-box-1"),
        Resources.Load<AudioClip>("SFX/breakables/box/falling/on-box-2"),
      }},
      {"grass", new AudioClip[] {
        Resources.Load<AudioClip>("SFX/breakables/box/falling/on-grass-1"),
        Resources.Load<AudioClip>("SFX/breakables/box/falling/on-grass-2"),
      }},
      {"item", new AudioClip[] {
        Resources.Load<AudioClip>("SFX/items/falling/on-box"),
      }}
    }}
  };

  public static Dictionary<string, AudioClip> droppableFallingSounds = new Dictionary<string, AudioClip> {
    {"grass", Resources.Load<AudioClip>("SFX/items/falling/on-grass")},
  };
}
