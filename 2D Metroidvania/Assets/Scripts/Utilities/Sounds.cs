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
    }},
    {"hero", new Dictionary<string, AudioClip[]> {
      {"dirt", new AudioClip[] {
        Resources.Load<AudioClip>("SFX/falling/dirt/boots"),
      }},
      {"grass", new AudioClip[] {
        Resources.Load<AudioClip>("SFX/falling/grass/boots"),
      }},
      {"box", new AudioClip[] {
        Resources.Load<AudioClip>("SFX/falling/breakables/box/boots"),
      }}
    }}
  };

  public static Dictionary<string, AudioClip> droppableFallingSounds = new Dictionary<string, AudioClip> {
    {"grass", Resources.Load<AudioClip>("SFX/items/falling/on-grass")},
  };

  // TODO: since not all sounds of a type will involve weapons of the same material (e.g. not all singles will be swords),
  // at some point some differentiation will be needed
  public static Dictionary<string, AudioClip> weaponSounds = new Dictionary<string, AudioClip> {
    {"double", Resources.Load<AudioClip>("SFX/weapons/double/sword")},
    {"single", Resources.Load<AudioClip>("SFX/weapons/single/sword")},
  };
}
