using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds {
  public static Dictionary<string, Dictionary<string, AudioClip[]>> runningSounds = new Dictionary<string, Dictionary<string, AudioClip[]>> {
    {"dirt", new Dictionary<string, AudioClip[]> {
      {"barefoot", new AudioClip[] {
        Resources.Load<AudioClip>("SFX/running/dirt/barefoot-1"),
        Resources.Load<AudioClip>("SFX/running/dirt/barefoot-2"),
        Resources.Load<AudioClip>("SFX/running/dirt/barefoot-3"),
        Resources.Load<AudioClip>("SFX/running/dirt/barefoot-4"),
        Resources.Load<AudioClip>("SFX/running/dirt/barefoot-5"),
      }},
      {"boots", new AudioClip[] {
        Resources.Load<AudioClip>("SFX/running/dirt/boots-1"),
        Resources.Load<AudioClip>("SFX/running/dirt/boots-2"),
        Resources.Load<AudioClip>("SFX/running/dirt/boots-3"),
        Resources.Load<AudioClip>("SFX/running/dirt/boots-4"),
        Resources.Load<AudioClip>("SFX/running/dirt/boots-5"),
      }}
    }},
    {"grass", new Dictionary<string, AudioClip[]> {
      {"barefoot", new AudioClip[] {
        Resources.Load<AudioClip>("SFX/running/grass/barefoot-1"),
        Resources.Load<AudioClip>("SFX/running/grass/barefoot-2"),
        Resources.Load<AudioClip>("SFX/running/grass/barefoot-3"),
        Resources.Load<AudioClip>("SFX/running/grass/barefoot-4"),
        Resources.Load<AudioClip>("SFX/running/grass/barefoot-5"),
      }},
      {"boots", new AudioClip[] {
        Resources.Load<AudioClip>("SFX/running/grass/boots-1"),
        Resources.Load<AudioClip>("SFX/running/grass/boots-2"),
        Resources.Load<AudioClip>("SFX/running/grass/boots-3"),
        Resources.Load<AudioClip>("SFX/running/grass/boots-4"),
        Resources.Load<AudioClip>("SFX/running/grass/boots-5"),
      }}
    }},
  };

  public static AudioClip[] woodBreakingSounds = new AudioClip[] {
    Resources.Load<AudioClip>("SFX/breakables/box/breaking/breaking-1"),
    Resources.Load<AudioClip>("SFX/breakables/box/breaking/breaking-2"),
    Resources.Load<AudioClip>("SFX/breakables/box/breaking/breaking-3"),
  };

  public static Dictionary<string, AudioClip[]> breakableSounds = new Dictionary<string, AudioClip[]> {
    {"barrel", woodBreakingSounds},
    {"box", woodBreakingSounds}
  };

  public static Dictionary<string, AudioClip[]> woodFallingSounds = new Dictionary<string, AudioClip[]> {
    {"barrel", new AudioClip[] {
      Resources.Load<AudioClip>("SFX/breakables/box/falling/on-box-1"),
      Resources.Load<AudioClip>("SFX/breakables/box/falling/on-box-2"),
    }},
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
  };

  // falling sounds for anything which base material is not changeable
  public static Dictionary<string, Dictionary<string, AudioClip[]>> fallingSounds = new Dictionary<string, Dictionary<string, AudioClip[]>> {
    {"barrel", woodFallingSounds},
    {"box", woodFallingSounds},
    {"character", new Dictionary<string, AudioClip[]> {
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

  public static Dictionary<string, Dictionary<string, AudioClip>> characterFallingSounds = new Dictionary<string, Dictionary<string, AudioClip>> {
    {"dirt", new Dictionary<string, AudioClip> {
      {"boots", Resources.Load<AudioClip>("SFX/falling/dirt/boots")}
    }},
    {"grass", new Dictionary<string, AudioClip> {
      {"boots", Resources.Load<AudioClip>("SFX/falling/grass/boots")}
    }},
    {"box", new Dictionary<string, AudioClip> {
      {"boots", Resources.Load<AudioClip>("SFX/falling/breakables/box/boots")}
    }},
  };

  public static Dictionary<string, AudioClip> droppableFallingSounds = new Dictionary<string, AudioClip> {
    {"dirt", Resources.Load<AudioClip>("SFX/falling/dirt/barefoot")},
    {"grass", Resources.Load<AudioClip>("SFX/items/falling/on-grass")},
    {"interactable", Resources.Load<AudioClip>("SFX/breakables/box/falling/on-box-1")}
  };

  // TODO: since not all sounds of a type will involve weapons of the same material (e.g. not all singles will be swords),
  // at some point some differentiation will be needed
  public static Dictionary<string, AudioClip> attackSounds = new Dictionary<string, AudioClip> {
    {"bow", Resources.Load<AudioClip>("SFX/weapons/bow/basic")},
    {"double", Resources.Load<AudioClip>("SFX/weapons/double/sword")},
    {"single", Resources.Load<AudioClip>("SFX/weapons/single/sword")},
    {"throwable-small", Resources.Load<AudioClip>("SFX/weapons/throwable/small")},
    {"throwable-middle", Resources.Load<AudioClip>("SFX/weapons/throwable/middle")},
    {"throwable-double-large", Resources.Load<AudioClip>("SFX/weapons/throwable-double/large")},
    {"punch", Resources.Load<AudioClip>("SFX/unequipped/punch")},
    {"kick", Resources.Load<AudioClip>("SFX/unequipped/kick")},
  };

  public static Dictionary<string, Dictionary<string, AudioClip>> impactSounds = new Dictionary<string, Dictionary<string, AudioClip>> {
    {"punch", new Dictionary<string, AudioClip> {
      {"normal", Resources.Load<AudioClip>("SFX/hitting/normal/punch")},
      {"critical", Resources.Load<AudioClip>("SFX/hitting/critical/punch")}
    }},
    {"kick", new Dictionary<string, AudioClip> {
      {"normal", Resources.Load<AudioClip>("SFX/hitting/normal/kick")},
      {"critical", Resources.Load<AudioClip>("SFX/hitting/critical/kick")}
    }},
    {"sword", new Dictionary<string, AudioClip> {
      {"normal", Resources.Load<AudioClip>("SFX/hitting/normal/sword")},
      {"critical", Resources.Load<AudioClip>("SFX/hitting/critical/sword")}
    }},
    {"throwable-small", new Dictionary<string, AudioClip> {
      {"normal", Resources.Load<AudioClip>("SFX/hitting/normal/throwable-small")},
      {"critical", Resources.Load<AudioClip>("SFX/hitting/critical/throwable-small")}
    }},
    {"throwable-middle", new Dictionary<string, AudioClip> {
      {"normal", Resources.Load<AudioClip>("SFX/hitting/normal/throwable-middle")},
      {"critical", Resources.Load<AudioClip>("SFX/hitting/critical/throwable-middle")}
    }},
    {"throwable-double-large", new Dictionary<string, AudioClip> {
      {"normal", Resources.Load<AudioClip>("SFX/hitting/normal/throwable-double-large")},
      {"critical", Resources.Load<AudioClip>("SFX/hitting/critical/throwable-double-large")}
    }},
    {"arrow", new Dictionary<string, AudioClip> {
      {"normal", Resources.Load<AudioClip>("SFX/hitting/normal/arrow")},
      {"critical", Resources.Load<AudioClip>("SFX/hitting/critical/arrow")}
    }},
    {"blunt", new Dictionary<string, AudioClip> {
      {"normal", Resources.Load<AudioClip>("SFX/hitting/normal/blunt")},
      {"critical", Resources.Load<AudioClip>("SFX/hitting/critical/blunt")}
    }}
  };

  public static Dictionary<string, AudioClip> blockSounds = new Dictionary<string, AudioClip> {
    {"basic", Resources.Load<AudioClip>("SFX/hitting/block/basic")}
  };

  public static Dictionary<string, AudioClip> explosionSounds = new Dictionary<string, AudioClip> {
    {"arrow", Resources.Load<AudioClip>("SFX/explosions/arrow")},
    {"basic", Resources.Load<AudioClip>("SFX/explosions/basic")},
    {"enemy", Resources.Load<AudioClip>("SFX/explosions/enemy")}
  };

  public static Dictionary<string, AudioClip> menuSounds = new Dictionary<string, AudioClip> {
    {"attention", Resources.Load<AudioClip>("SFX/menu/attention")},
    {"back", Resources.Load<AudioClip>("SFX/menu/back")},
    {"move", Resources.Load<AudioClip>("SFX/menu/move")},
    {"select", Resources.Load<AudioClip>("SFX/menu/select")},
    {"use", Resources.Load<AudioClip>("SFX/menu/use")}
  };

  public static Dictionary<string, AudioClip> itemPickSounds = new Dictionary<string, AudioClip> {
    {"money", Resources.Load<AudioClip>("SFX/items/pick/money")},
    {"normal", Resources.Load<AudioClip>("SFX/items/pick/normal")},
    {"rare", Resources.Load<AudioClip>("SFX/items/pick/rare")}
  };

  public static Dictionary<string, AudioClip> chestSounds = new Dictionary<string, AudioClip> {
    {"chest-small", Resources.Load<AudioClip>("SFX/chests/small")},
    {"chest-large", Resources.Load<AudioClip>("SFX/chests/large")},
  };

  public static Dictionary<string, AudioClip> notificationSounds = new Dictionary<string, AudioClip> {
    {"levelup", Resources.Load<AudioClip>("SFX/notifications/levelup")},
  };

  public static Dictionary<string, AudioClip> poisonSounds = new Dictionary<string, AudioClip> {
    {"basic", Resources.Load<AudioClip>("SFX/poison/basic")},
  };

  public static Dictionary<string, AudioClip> meleeSounds = new Dictionary<string, AudioClip> {
    {"smash", Resources.Load<AudioClip>("SFX/melee/smash")},
  };

  public static Dictionary<string, AudioClip> loops = new Dictionary<string, AudioClip> {
    {"arrow-burn", Resources.Load<AudioClip>("SFX/loops/arrow-burn")},
  };

  public static Dictionary<string, AudioClip> introSounds = new Dictionary<string, AudioClip> {
    {"last", Resources.Load<AudioClip>("SFX/intro/last")},
    {"suspense", Resources.Load<AudioClip>("SFX/intro/suspense")}
  };
}
