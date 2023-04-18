using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Objects {
  public static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject> {
    {"arrow", Resources.Load("Prefabs/Arrow") as GameObject},
    {"arrow-burn", Resources.Load("Prefabs/ArrowBurn") as GameObject},
    {"arrow-explosion", Resources.Load("Prefabs/ArrowExplosion") as GameObject},
    {"bar-decrement", Resources.Load("Prefabs/BarDecrement") as GameObject},
    {"boss", Resources.Load("Prefabs/Boss") as GameObject},
    {"breakable", Resources.Load("Prefabs/Breakable") as GameObject},
    {"chest-large", Resources.Load("Prefabs/ChestLarge") as GameObject},
    {"chest-small", Resources.Load("Prefabs/ChestSmall") as GameObject},
    {"damage-container", Resources.Load("Prefabs/DamageContainer") as GameObject},
    {"defense", Resources.Load("Prefabs/Defense") as GameObject},
    {"droppable", Resources.Load("Prefabs/Droppable") as GameObject},
    {"enemy", Resources.Load("Prefabs/Enemy") as GameObject},
    {"enemy-explosion", Resources.Load("Prefabs/EnemyExplosion") as GameObject},
    {"item-button", Resources.Load("Prefabs/ItemButton") as GameObject},
    {"pierce", Resources.Load("Prefabs/Pierce") as GameObject},
    {"smash-wave", Resources.Load("Prefabs/SmashWave") as GameObject},
    {"summon-energy", Resources.Load("Prefabs/SummonEnergy") as GameObject},
    {"throwable", Resources.Load("Prefabs/Throwable") as GameObject}
  };

  public static Dictionary<string, Sprite> arrows = new Dictionary<string, Sprite> {
    {"arrow-fire", Resources.Load<Sprite>("Sprites/arrow-fire")},
    {"arrow-poison", Resources.Load<Sprite>("Sprites/arrow-poison")},
    {"arrow-standard", Resources.Load<Sprite>("Sprites/arrow-standard")}
  };

  public static Dictionary<string, string[]> itemProjectiles = new Dictionary<string, string[]> {
    {"bow", new string[]{"arrow-standard", "arrow-poison", "arrow-fire"}}
  };

  // initialize constructable PauseItem objects by: new PauseItem(thumbnail, image, name, description, type, effects)
  public static Dictionary<string, PauseItem> pauseItems = new Dictionary<string, PauseItem> {
    {"basic-longsword", new PauseItem(Sprites.itemThumbnails[0], Sprites.itemImages[0], "Basic Longsword", "Useful two-handed weapon.", "double", new Effects() {atk=40})},
    {"basic-sword", new PauseItem(Sprites.itemThumbnails[1], Sprites.itemImages[1], "Basic Sword", "Standard adventurer's sword.", "single", new Effects() {atk=20})},
    {"basic-shield", new PauseItem(Sprites.itemThumbnails[2], Sprites.itemImages[2], "Basic Shield", "Can also be used to start a campfire.", "defense", new Effects() {def=10})},
    {"chicken-drumstick", new PauseItem(Sprites.itemThumbnails[3], Sprites.itemImages[3], "Chicken Drumstick", "From range-free raised fowl.", "food", new Effects() {hp = 20})},
    {"love-necklace", new PauseItem(Sprites.itemThumbnails[4], Sprites.itemImages[4], "Necklace of Love", "A family heirloom that belonged to the princess.", "neck", new Effects() {def=50, luck=0.1f})},
    {"solomon-ring", new PauseItem(Sprites.itemThumbnails[5], Sprites.itemImages[5], "Ring of Solomon", "Ever sought after, wearer will rule the world!", "ring", new Effects() {atk=100, def=100, crit=0.3f, luck=0.3f})},
    {"ra-ring", new PauseItem(Sprites.itemThumbnails[6], Sprites.itemImages[6], "Ring of Ra", "Said to have been created by the people of Atlantis.", "ring", new Effects() {def=50, magicResistances=new MagicResistance[]{new MagicResistance(){name="Dark", type="add"}}})},
    {"skull-ring", new PauseItem(Sprites.itemThumbnails[7], Sprites.itemImages[7], "Skull Ring", "Most priced possession of the commander of dark armies.", "ring", new Effects() {def=50, crit=0.2f})},
    {"gold-bracelet", new PauseItem(Sprites.itemThumbnails[8], Sprites.itemImages[8], "Gold Bracelet", "Imbued with notable attack power.", "armwear", new Effects() {atk=25})},
    {"silver-bracelet", new PauseItem(Sprites.itemThumbnails[9], Sprites.itemImages[9], "Silver Bracelet", "Imbued with significant defense.", "armwear", new Effects() {def=25})},
    {"rabbit-paw", new PauseItem(Sprites.itemThumbnails[10], Sprites.itemImages[10], "Rabbit's Paw", "Carefully crafted from grade-A bunnies.", "neck", new Effects() {luck=0.2f})},
    {"moonlight-pendant", new PauseItem(Sprites.itemThumbnails[11], Sprites.itemImages[11], "Moonlight Pendant", "Protects from harsh environments and increases visibility.", "neck", new Effects() {def=10, crit=0.1f})},
    {"rainbow-bracer", new PauseItem(Sprites.itemThumbnails[12], Sprites.itemImages[12], "Rainbow Bracer", "Protects against most magic elements.", "armwear", new Effects() {magicResistances=new MagicResistance[]{new MagicResistance(){name="Earth", type="add"}, new MagicResistance(){name="Air", type="add"}, new MagicResistance(){name="Water", type="add"}, new MagicResistance(){name="Fire", type="add"}, new MagicResistance(){name="Lightning", type="add"}, new MagicResistance(){name="Ice", type="add"}, new MagicResistance(){name="Light", type="add"}}})},
    {"body-1", new PauseItem(Sprites.itemThumbnails[13], Sprites.itemImages[13], "Townsman Tunic", "Your basic countryman attire, custom dyed.", "body", new Effects() {def=10, crit=0.05f, luck=0.05f})},
    {"lance", new PauseItem(Sprites.itemThumbnails[14], Sprites.itemImages[14], "Lance", "Said to require twice the skill to handle compared to a sword.", "throwable-double", new Effects() {atk=60})},
    {"axe", new PauseItem(Sprites.itemThumbnails[15], Sprites.itemImages[15], "Axe", "Dual edged for maximum damage and reach.", "throwable-double", new Effects() {atk=40})},
    {"hatchet", new PauseItem(Sprites.itemThumbnails[16], Sprites.itemImages[16], "Hatchet", "Useful for chopping either wood or some vermin's head.", "throwable", new Effects() {atk=20})},
    {"shuriken-6", new PauseItem(Sprites.itemThumbnails[17], Sprites.itemImages[17], "Six point Shuriken", "Unexpected rolling attack.", "throwable", new Effects() {atk=10})},
    {"shuriken-4", new PauseItem(Sprites.itemThumbnails[18], Sprites.itemImages[18], "Four point Shuriken", "Fast throwing weapon, not too strong.", "throwable", new Effects() {atk=5})},
    {"knife", new PauseItem(Sprites.itemThumbnails[19], Sprites.itemImages[19], "Knife", "Could be used to spread butter on bread.", "throwable", new Effects() {atk=15})},
    {"kunai", new PauseItem(Sprites.itemThumbnails[20], Sprites.itemImages[20], "Kunai", "Used for masonry as well as for battle.", "throwable", new Effects() {atk=10})},
    {"bow", new PauseItem(Sprites.itemThumbnails[21], Sprites.itemImages[21], "Standard Bow", "Your basic archery bow.", "bow", new Effects() {atk=0})},
    {"arrow-standard", new PauseItem(Sprites.itemThumbnails[22], Sprites.itemImages[22], "Standard Arrow", "Basic arrows to deal projectile damage.", "arrow", new Effects() {atk=20})},
    {"arrow-poison", new PauseItem(Sprites.itemThumbnails[23], Sprites.itemImages[23], "Poisoned Arrow", "Arrows dipped with poison to damage beyond contact.", "arrow", new Effects() {atk=10})},
    {"arrow-fire", new PauseItem(Sprites.itemThumbnails[24], Sprites.itemImages[24], "Fire Arrow", "Incendiary arrow which burns a non-resistant enemy upon contact.", "arrow", new Effects() {atk=5})},
    {"bomb", new PauseItem(Sprites.itemThumbnails[25], Sprites.itemImages[25], "Bomb", "Paper-based weapon of Asian manufacture with high explosive power.", "throwable", new Effects() {atk=100})}

    // Example of having everything
    // {"chicken-drumstick", new PauseItem(Sprites.itemThumbnails[3], Sprites.itemImages[3], "Chicken Drumstick", "From range-free raised fowl.", "food", new Effects() {hp = 99, mp=99, statusHeal=new string[]{"Poison", "Curse", "Drain"}, atk=-99, def=99, crit=0.5f, luck=-0.2f, magicResistances=new MagicResistance[]{new MagicResistance(){name="Earth", type="add"}, new MagicResistance(){name="Air", type="add"}, new MagicResistance(){name="Water", type="add"}, new MagicResistance(){name="Fire", type="add"}, new MagicResistance(){name="Lightning", type="remove"}, new MagicResistance(){name="Ice", type="remove"}, new MagicResistance(){name="Light", type="remove"}, new MagicResistance(){name="Dark", type="remove"}}})}
  };

  public static Dictionary<string, MoneyItem> moneyItems = new Dictionary<string, MoneyItem> {
    {"money-50", new MoneyItem(){ image = Sprites.moneyImages[0], increment = 50, size = new Vector2(0.45f, 0.55f), text = "$50"}},
    {"money-100", new MoneyItem(){ image = Sprites.moneyImages[1], increment = 100, size = new Vector2(0.45f, 0.55f), text = "$100"}},
    {"money-200", new MoneyItem(){ image = Sprites.moneyImages[2], increment = 200, size = new Vector2(0.8f, 0.9f), text = "$200"}},
    {"money-500", new MoneyItem(){ image = Sprites.moneyImages[3], increment = 500, size = new Vector2(0.8f, 0.9f), text = "$500"}},
    {"money-1000", new MoneyItem(){ image = Sprites.moneyImages[4], increment = 1000, size = new Vector2(1.4f, 1.2f), text = "$1000"}},
    {"money-2000", new MoneyItem(){ image = Sprites.moneyImages[5], increment = 2000, size = new Vector2(1.4f, 1.2f), text = "$2000"}},
    {"money-5000", new MoneyItem(){ image = Sprites.moneyImages[6], increment = 5000, size = new Vector2(1.4f, 1.2f), text = "$5000"}}
  };

  public static Dictionary<string, CompositePauseImage> compositePauseImages = new Dictionary<string, CompositePauseImage> {
    {"bow-with-arrow-standard", new CompositePauseImage(){thumbnail = Sprites.itemCombinedThumbnails[0], name = "Bow with Std. Arrow"}},
    {"bow-with-arrow-poison", new CompositePauseImage(){thumbnail = Sprites.itemCombinedThumbnails[1], name = "Bow with Poison Arrow"}},
    {"bow-with-arrow-fire", new CompositePauseImage(){thumbnail = Sprites.itemCombinedThumbnails[2], name = "Bow with Fire Arrow"}}
  };

  public static Dictionary<string, RuntimeAnimatorController> animationControllers = new Dictionary<string, RuntimeAnimatorController> {
    {"breakable", Resources.Load("Animations/Breakables/Breakable") as RuntimeAnimatorController},
    {"hero-1", Resources.Load("Animations/Hero/Hero-1") as RuntimeAnimatorController},
    {"hero-1-movement", Resources.Load("Animations/Hero/Hero-1-Movement") as RuntimeAnimatorController},
    {"patroller", Resources.Load("Animations/Enemy/patroller") as RuntimeAnimatorController},
    {"skeleton-king", Resources.Load("Animations/Enemy/skeleton-king/skeleton-king") as RuntimeAnimatorController}
  };

  public static Dictionary<string, BreakableDimension> breakableSizes = new Dictionary<string, BreakableDimension> {
    {"barrel", new BreakableDimension() {offset = new Vector2(0, 0.52f), size = new Vector2(0.8f, 1.05f)}},
    {"box", new BreakableDimension() {offset = new Vector2(0, 0.55f), size = new Vector2(1, 1.1f)}},
    {"jar", new BreakableDimension() {offset = new Vector2(0, 0.35f), size = new Vector2(0.6f, 0.7f)}},
    {"vase", new BreakableDimension() {offset = new Vector2(0, 0.72f), size = new Vector2(0.95f, 1.45f)}}
  };

  public static Dictionary<string, float> decrementBarMultipliers = new Dictionary<string, float> {
    {"hp", Constants.hpMultiplier},
    {"mp", Constants.mpMultiplier}
  };

  public static Dictionary<string, EnemyStats> enemyStats = new Dictionary<string, EnemyStats> {
    {"goblin", new EnemyStats() {name = "Goblin", type = "patroller", hp = 80, atk = 40, def = 30, crit = 0.015f, exp = 60, speed= 3, reach = 0.5f}},
    {"skeleton", new EnemyStats() {name = "Skeleton", type = "patroller", hp = 100, atk = 20, def = 10, crit = 0.01f, exp = 30, speed= 3, reach = 0.5f}},
    {"skeleton-king", new EnemyStats() {name = "Skeleton King", type = "champion", hp = 300, atk = 60, def = 30, crit = 0.15f, exp = 100, speed= 2, reach = 1}}
  };

  public static Dictionary<string, Vector2> enemyDimensions = new Dictionary<string, Vector2> {
    {"skeleton", new Vector2(1.77f, 2.05f)},
    {"skeleton-king", new Vector2(1.82f, 3.21f)}
  };

  public static Dictionary<string, TMP_FontAsset> fonts = new Dictionary<string, TMP_FontAsset> {
    {"levi-rebrushed", Resources.Load("Fonts/LeviRebrushed SDF") as TMP_FontAsset}
  };
}
