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
    {"enemy-spawner", Resources.Load("Prefabs/EnemySpawner") as GameObject},
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
    {"bomb", new PauseItem(Sprites.itemThumbnails[25], Sprites.itemImages[25], "Bomb", "Paper-based weapon of Asian manufacture with high explosive power.", "throwable", new Effects() {atk=100})},
    {"diamond", new PauseItem(Sprites.itemThumbnails[26], Sprites.itemImages[26], "Diamond", "Commercially non-rare precious stone.", "valuable")},
    {"emerald", new PauseItem(Sprites.itemThumbnails[27], Sprites.itemImages[27], "Emerald", "Precious stone of a vibrant green color.", "valuable")},
    {"sapphire", new PauseItem(Sprites.itemThumbnails[28], Sprites.itemImages[28], "Sapphire", "Blue gemstone that matches the deep seas.", "valuable")},
    {"citrine", new PauseItem(Sprites.itemThumbnails[29], Sprites.itemImages[29], "Citrine", "A soothing gemstone known for its yellow color.", "valuable")},
    {"ruby", new PauseItem(Sprites.itemThumbnails[30], Sprites.itemImages[30], "Ruby", "A very durable gemstone with a blood-like red appearance.", "valuable")},
    {"opal", new PauseItem(Sprites.itemThumbnails[31], Sprites.itemImages[31], "Opal", "Valuable gem appreciated by royalty.", "valuable")},
    {"amethyst", new PauseItem(Sprites.itemThumbnails[32], Sprites.itemImages[32], "Amethyst", "Gemstone that is believed to be a good luck charm for soldiers.", "valuable")},
    {"gold-bar", new PauseItem(Sprites.itemThumbnails[33], Sprites.itemImages[33], "Gold Bar", "Valuable bar made of high quality gold.", "valuable")},
    {"gold-ingot", new PauseItem(Sprites.itemThumbnails[34], Sprites.itemImages[34], "Gold Ingot", "Highly valuable ingot guaranteed to fetch a small fortune.", "valuable")},
    {"pearl", new PauseItem(Sprites.itemThumbnails[35], Sprites.itemImages[35], "Pearl", "Natural treasure of the seas.", "valuable")},
    {"potion", new PauseItem(Sprites.itemThumbnails[36], Sprites.itemImages[36], "Potion", "Basic healing potion.", "consumable", new Effects() {hpPercentage=0.25f})},
    {"mid-potion", new PauseItem(Sprites.itemThumbnails[37], Sprites.itemImages[37], "Mid Potion", "Potion with high quality healing properties.", "consumable", new Effects() {hpPercentage=0.5f})},
    {"high-potion", new PauseItem(Sprites.itemThumbnails[38], Sprites.itemImages[38], "High Potion", "Made from the highest quality materials for healing.", "consumable", new Effects() {hpPercentage=1f})},
    {"magic-ampoule", new PauseItem(Sprites.itemThumbnails[39], Sprites.itemImages[39], "Magic Ampoule", "Substance with magic properties.", "consumable", new Effects() {mpPercentage=0.25f})},
    {"magic-vial", new PauseItem(Sprites.itemThumbnails[40], Sprites.itemImages[40], "Magic Vial", "Potent magic restorer.", "consumable", new Effects() {mpPercentage=0.5f})},
    {"magic-bottle", new PauseItem(Sprites.itemThumbnails[41], Sprites.itemImages[41], "Magic Bottle", "Greatly replenishes magic expenditure.", "consumable", new Effects() {mpPercentage=1f})},
    {"strength-flask", new PauseItem(Sprites.itemThumbnails[42], Sprites.itemImages[42], "Strength Flask", "Temporarily increases strength.", "consumable", new Effects() {atk=20, duration=30})},
    {"stamina-flask", new PauseItem(Sprites.itemThumbnails[43], Sprites.itemImages[43], "Stamina Flask", "Temporarily increases stamina.", "consumable", new Effects() {def=20, duration=30})},
    {"critical-flask", new PauseItem(Sprites.itemThumbnails[44], Sprites.itemImages[44], "Critical Flask", "Temporarily increases critical rate.", "consumable", new Effects() {crit=0.1f, duration=30})},
    {"luck-flask", new PauseItem(Sprites.itemThumbnails[45], Sprites.itemImages[45], "Luck Flask", "Temporarily increases luck.", "consumable", new Effects() {luck=0.1f, duration=30})},
    // TODO: implement a randomized effect
    {"random-flask", new PauseItem(Sprites.itemThumbnails[46], Sprites.itemImages[46], "Random Flask", "Temporarily increases a random atttribute.", "consumable", new Effects() {luck=0.1f, duration=30})},
    {"theriac", new PauseItem(Sprites.itemThumbnails[47], Sprites.itemImages[47], "Theriac", "Fabled concoction said to heal 100 diseases.", "", new Effects() {duration=30, statusHeal=new string[]{"all"}, magicResistances=new MagicResistance[]{new MagicResistance(){name="Earth", type="add"}, new MagicResistance(){name="Air", type="add"}, new MagicResistance(){name="Water", type="add"}, new MagicResistance(){name="Fire", type="add"}, new MagicResistance(){name="Lightning", type="add"}, new MagicResistance(){name="Ice", type="add"}, new MagicResistance(){name="Light", type="add"}, new MagicResistance(){name="Dark", type="add"}}})},
    {"hashish", new PauseItem(Sprites.itemThumbnails[48], Sprites.itemImages[48], "Hashish", "Known to the Wattman Caliphate to cause cancer, birth defects, or other reproductive harm.", "", new Effects() {hpPercentage=-0.2f, mpPercentage=0.6f})},
    {"earth-med", new PauseItem(Sprites.itemThumbnails[49], Sprites.itemImages[49], "Earth Medicine", "Provides resistance against earth magic.", "consumable", new Effects() {duration=30, magicResistances=new MagicResistance[]{new MagicResistance(){name="Earth", type="add"}}})},
    {"air-med", new PauseItem(Sprites.itemThumbnails[50], Sprites.itemImages[50], "Air Medicine", "Provides resistance against air magic.", "consumable", new Effects() {duration=30, magicResistances=new MagicResistance[]{new MagicResistance(){name="Air", type="add"}}})},
    {"water-med", new PauseItem(Sprites.itemThumbnails[51], Sprites.itemImages[51], "Water Medicine", "Provides resistance against water magic.", "consumable", new Effects() {duration=30, magicResistances=new MagicResistance[]{new MagicResistance(){name="Water", type="add"}}})},
    {"fire-med", new PauseItem(Sprites.itemThumbnails[52], Sprites.itemImages[52], "Fire Medicine", "Provides resistance against fire magic.", "consumable", new Effects() {duration=30, magicResistances=new MagicResistance[]{new MagicResistance(){name="Fire", type="add"}}})},
    {"lightning-med", new PauseItem(Sprites.itemThumbnails[53], Sprites.itemImages[53], "Lightning Medicine", "Provides resistance against lightning magic.", "consumable", new Effects() {duration=30, magicResistances=new MagicResistance[]{new MagicResistance(){name="Lightning", type="add"}}})},
    {"ice-med", new PauseItem(Sprites.itemThumbnails[54], Sprites.itemImages[54], "Ice Medicine", "Provides resistance against ice magic.", "consumable", new Effects() {duration=30, magicResistances=new MagicResistance[]{new MagicResistance(){name="Ice", type="add"}}})},
    {"light-med", new PauseItem(Sprites.itemThumbnails[55], Sprites.itemImages[55], "Light Medicine", "Provides resistance against light magic.", "consumable", new Effects() {duration=30, magicResistances=new MagicResistance[]{new MagicResistance(){name="Light", type="add"}}})},
    {"dark-med", new PauseItem(Sprites.itemThumbnails[56], Sprites.itemImages[56], "Dark Medicine", "Provides resistance against dark magic.", "consumable", new Effects() {duration=30, magicResistances=new MagicResistance[]{new MagicResistance(){name="Dark", type="add"}}})},
    {"elixir", new PauseItem(Sprites.itemThumbnails[57], Sprites.itemImages[57], "Elixir", "Heals all health and replenishes all magic.", "", new Effects() {hpPercentage=1f, mpPercentage=1f})},
    {"apple", new PauseItem(Sprites.itemThumbnails[58], Sprites.itemImages[58], "Apple", "Tasty red fruit found pretty much everywhere.", "food", new Effects() {hp=20})},
    {"banana", new PauseItem(Sprites.itemThumbnails[59], Sprites.itemImages[59], "Banana", "Yellow, really tasty when ripe. Goes well with bread.", "food", new Effects() {hp=20})},
    {"orange", new PauseItem(Sprites.itemThumbnails[60], Sprites.itemImages[60], "Orange", "May be sweet but also sometimes a bit bitter.", "food", new Effects() {hp=20})},
    {"pear", new PauseItem(Sprites.itemThumbnails[61], Sprites.itemImages[61], "Pear", "Hard on the outside but soft on the inside.", "food", new Effects() {hp=20})},
    {"strawberry", new PauseItem(Sprites.itemThumbnails[62], Sprites.itemImages[62], "Strawberry", "Small but flavorful. Really tasty with cake.", "food", new Effects() {hp=10})},
    {"cherry", new PauseItem(Sprites.itemThumbnails[63], Sprites.itemImages[63], "Cherry", "Somewhat sour. Great as a pie filling.", "food", new Effects() {hp=10})},
    {"grapes", new PauseItem(Sprites.itemThumbnails[64], Sprites.itemImages[64], "Grapes", "Great as a snack. Beware of seeds.", "food", new Effects() {hp=10})},
    {"pineapple", new PauseItem(Sprites.itemThumbnails[65], Sprites.itemImages[65], "Pineapple", "Tropical fruit with a sweet but tangy flavor.", "food", new Effects() {hp=50})},
    {"mango", new PauseItem(Sprites.itemThumbnails[66], Sprites.itemImages[66], "Mango", "Delicious fruit with an edible peel.", "food", new Effects() {hp=20})},
    {"coconut", new PauseItem(Sprites.itemThumbnails[67], Sprites.itemImages[67], "Coconut", "Tropical fruit with a hard surface. Can be thrown.", "throwable-food", new Effects() {hp=50})},
    {"honeydew", new PauseItem(Sprites.itemThumbnails[68], Sprites.itemImages[68], "Honeydew", "Tasty melon with a green flesh. Can be thrown.", "throwable-food", new Effects() {hp=80})},
    {"watermelon", new PauseItem(Sprites.itemThumbnails[69], Sprites.itemImages[69], "Watermelon", "Refreshing melon with a red flesh. Can be thrown to get slices.", "throwable-food", new Effects() {hp=100})},
    {"watermelon-slice", new PauseItem(Sprites.itemThumbnails[70], Sprites.itemImages[70], "Watermelon Slice", "Refreshing watermelon slice ideal for snacking.", "food", new Effects() {hp=20})},
    {"silver-bar", new PauseItem(Sprites.itemThumbnails[71], Sprites.itemImages[71], "Silver Bar", "Silver molded into bars to use as currency.", "valuable")},
    {"silver-ingot", new PauseItem(Sprites.itemThumbnails[72], Sprites.itemImages[72], "Silver Ingot", "Silver ingot made from high quality silver.", "valuable")},
    {"bronze-bar", new PauseItem(Sprites.itemThumbnails[73], Sprites.itemImages[73], "Bronze Bar", "Bronze molded into bars for use as trade.", "valuable")},
    {"bronze-ingot", new PauseItem(Sprites.itemThumbnails[74], Sprites.itemImages[74], "Bronze Ingot", "Bronze molded into ingots for trade.", "valuable")},
    {"skull", new PauseItem(Sprites.itemThumbnails[75], Sprites.itemImages[75], "Skull", "A skeleton’s head, useful for museum display or to decorate an eccentric person’s house.", "valuable")},
    {"polished-skull", new PauseItem(Sprites.itemThumbnails[76], Sprites.itemImages[76], "Polished Skull", "A sturdier skull from a higher ranked skeleton.", "valuable")},
    {"silver-skull", new PauseItem(Sprites.itemThumbnails[77], Sprites.itemImages[77], "Silver Skull", "Skull from elite skeletons.", "valuable")},
    {"calcite", new PauseItem(Sprites.itemThumbnails[78], Sprites.itemImages[78], "Calcite", "The magical core of a skeleton which did not explode upon its death.", "valuable")},
    {"refined-calcite", new PauseItem(Sprites.itemThumbnails[79], Sprites.itemImages[79], "Refined Calcite", "Belonging to higher ranked skeletons, it requires a more complex composition.", "valuable")},
    {"precious-calcite", new PauseItem(Sprites.itemThumbnails[80], Sprites.itemImages[80], "Precious", "Elite skeleton calcite which harnessed much of its reanimation magic.", "valuable")},
    {"darkling-sword", new PauseItem(Sprites.itemThumbnails[81], Sprites.itemImages[81], "Darkling Sword", "Sword from low ranked soldiers of darkness.", "single", new Effects() {atk=15})},
    {"darklord-sword", new PauseItem(Sprites.itemThumbnails[82], Sprites.itemImages[82], "Darklord Sword", "Sword reserved for high ranked lords of chaos.", "double", new Effects() {atk=50})},
    {"spices", new PauseItem(Sprites.itemThumbnails[83], Sprites.itemImages[83], "Spices", "Important ingredients from trading merchants.", "valuable")},
    {"textiles", new PauseItem(Sprites.itemThumbnails[84], Sprites.itemImages[84], "Textiles", "Important cargo from trading merchants.", "valuable")},
    {"bandit-knife", new PauseItem(Sprites.itemThumbnails[85], Sprites.itemImages[85], "Bandit Knife", "Common weapon from lowly bandits.", "throwable", new Effects() {atk=10})},
    {"bandit-rapier", new PauseItem(Sprites.itemThumbnails[86], Sprites.itemImages[86], "Bandit Rapier", "Weapon from seasoned outlaws.", "single", new Effects() {atk=30})},
    {"laurel-crown", new PauseItem(Sprites.itemThumbnails[87], Sprites.itemImages[87], "Laurel Crown", "Simple leaf crown made by magical girls.", "valuable")},
    {"flower-crown", new PauseItem(Sprites.itemThumbnails[88], Sprites.itemImages[88], "Flower Crown", "Crown made out of large flowers.", "valuable")},
    {"flower-wreath", new PauseItem(Sprites.itemThumbnails[89], Sprites.itemImages[89], "Flower Wreath", "Prized by magical beings and humans alike, this is the pinnacle of decoration.", "valuable")},
    {"vine-staff", new PauseItem(Sprites.itemThumbnails[90], Sprites.itemImages[90], "Vine Staff", "Standard nymph weapon.", "single", new Effects() {atk=20})},
    {"vine-scepter", new PauseItem(Sprites.itemThumbnails[91], Sprites.itemImages[91], "Vine Scepter", "A respected nymph’s magical weapon.", "single", new Effects() {atk=50})},
    {"wine", new PauseItem(Sprites.itemThumbnails[92], Sprites.itemImages[92], "Wine", "Brewed to perfection for a long time.", "food", new Effects() {hp=100, mp=-20})},
    {"reinforced-axe", new PauseItem(Sprites.itemThumbnails[93], Sprites.itemImages[93], "Reinforced Axe", "A strong dwarf’s weapon of choice.", "throwable-double", new Effects() {atk=80})},
    {"dwarf-hammer", new PauseItem(Sprites.itemThumbnails[95], Sprites.itemImages[94], "Dwarf Hammer", "Simple hammer that can be wielded with one hand.", "single", new Effects() {atk=40})},
    {"war-maul", new PauseItem(Sprites.itemThumbnails[95], Sprites.itemImages[95], "War Maul", "Reinforced hammer that needs to be wielded with two hands.", "double", new Effects() {atk=80})},
    {"bronze-pixie-belt", new PauseItem(Sprites.itemThumbnails[96], Sprites.itemImages[96], "Bronze Pixie Belt", "A pixie belt that can be used as a bracelet.", "armwear", new Effects() {atk=10, def=10})},
    {"silver-pixie-belt", new PauseItem(Sprites.itemThumbnails[97], Sprites.itemImages[97], "Silver Pixie Belt", "Pixie belt from nobility.", "armwear", new Effects() {atk=20, def=30, luck=0.05f})},
    {"gold-pixie-belt", new PauseItem(Sprites.itemThumbnails[98], Sprites.itemImages[98], "Gold Pixie Belt", "Prized possession of the wealthiest of pixies.", "armwear", new Effects() {atk=30, def=50, luck=0.05f, crit=0.05f})},
    {"draco-shield", new PauseItem(Sprites.itemThumbnails[99], Sprites.itemImages[99], "Draco Shield", "Bronze shield said to be indestructible.", "defense", new Effects() {def=100})},
    {"king-skull", new PauseItem(Sprites.itemThumbnails[100], Sprites.itemImages[100], "King Skull", "The head from a skeleton king, far more intimidating than any skeleton's", "valuable")},
    {"polished-king-skull", new PauseItem(Sprites.itemThumbnails[101], Sprites.itemImages[101], "Polished King Skull", "A more refined head from a skeleton king", "valuable")},
    {"silver-king-skull", new PauseItem(Sprites.itemThumbnails[102], Sprites.itemImages[102], "Silver King Skull", "Silver skeleton king skull with a gold laurel crown", "valuable")},
    {"royal-pelt", new PauseItem(Sprites.itemThumbnails[103], Sprites.itemImages[103], "Royal Pelt", "Comfortable pelt worn by skeleton kings", "valuable")},
    {"giant-bone", new PauseItem(Sprites.itemThumbnails[104], Sprites.itemImages[104], "Giant Bone", "Thrown by the skeleton king. Quite deadly when it hits", "throwable-double", new Effects() {atk=80})},

    // Example of having everything
    // {"chicken-drumstick", new PauseItem(Sprites.itemThumbnails[3], Sprites.itemImages[3], "Chicken Drumstick", "From range-free raised fowl.", "food", new Effects() {hp = 99, mp=99, statusHeal=new string[]{"Poison", "Curse", "Drain"}, atk=-99, def=99, crit=0.5f, luck=-0.2f, magicResistances=new MagicResistance[]{new MagicResistance(){name="Earth", type="add"}, new MagicResistance(){name="Air", type="add"}, new MagicResistance(){name="Water", type="add"}, new MagicResistance(){name="Fire", type="add"}, new MagicResistance(){name="Lightning", type="remove"}, new MagicResistance(){name="Ice", type="remove"}, new MagicResistance(){name="Light", type="remove"}, new MagicResistance(){name="Dark", type="remove"}}})}
  };

  // To get item probabilities, order them with the highest probability item first, then descending. the next item's probability has to be its inteded probability PLUS the previous ones
  // Example: if 4 items (A, B, C, D) can be dropped, and A drops 80% of the time, B does so 10%, C does so 6% and D does so 4%, then A's probability is 0.8, B is 0.9, C is 0.96, and D is 1
  public static Dictionary<string, Dictionary<string, ProbabilityItem[]>> enemyDroppables = new Dictionary<string, Dictionary<string, ProbabilityItem[]>> {
    {"skeleton", new Dictionary<string, ProbabilityItem[]> {
      {"low", new ProbabilityItem[] {
        new ProbabilityItem(){key="low-potions", probability=0.8f},
        new ProbabilityItem(){key="low-money", probability=0.9f},
        new ProbabilityItem(){key="skull", probability=0.96f},
        new ProbabilityItem(){key="calcite", probability=1},
      }},
      {"mid", new ProbabilityItem[] {
        new ProbabilityItem(){key="mid-potions", probability=0.8f},
        new ProbabilityItem(){key="darkling-sword", probability=0.9f},
        new ProbabilityItem(){key="polished-skull", probability=0.96f},
        new ProbabilityItem(){key="refined-calcite", probability=1},
      }},
      {"high", new ProbabilityItem[] {
        new ProbabilityItem(){key="silver-bracelet", probability=0.8f},
        new ProbabilityItem(){key="darklord-sword", probability=0.9f},
        new ProbabilityItem(){key="silver-skull", probability=0.96f},
        new ProbabilityItem(){key="precious-calcite", probability=1},
      }}
    }},
    {"goblin", new Dictionary<string, ProbabilityItem[]> {
      {"low", new ProbabilityItem[] {
        new ProbabilityItem(){key="low-food", probability=0.5f},
        new ProbabilityItem(){key="low-money", probability=0.8f},
        new ProbabilityItem(){key="spices", probability=0.9f},
        new ProbabilityItem(){key="bronze-bar", probability=1},
      }},
      {"mid", new ProbabilityItem[] {
        new ProbabilityItem(){key="mid-food", probability=0.5f},
        new ProbabilityItem(){key="goblin-knives", probability=0.8f},
        new ProbabilityItem(){key="textiles", probability=0.9f},
        new ProbabilityItem(){key="goblin-mid-item4", probability=1},
      }},
      {"high", new ProbabilityItem[] {
        new ProbabilityItem(){key="mid-food", probability=0.5f},
        new ProbabilityItem(){key="bandit-rapier", probability=0.8f},
        new ProbabilityItem(){key="mid-money", probability=0.9f},
        new ProbabilityItem(){key="goblin-high-item4", probability=1},
      }}
    }},
    {"nymph", new Dictionary<string, ProbabilityItem[]> {
      {"low", new ProbabilityItem[] {
        new ProbabilityItem(){key="low-food", probability=0.6f},
        new ProbabilityItem(){key="low-potions", probability=0.8f},
        new ProbabilityItem(){key="low-bracelets", probability=0.95f},
        new ProbabilityItem(){key="laurel-crown", probability=1},
      }},
      {"mid", new ProbabilityItem[] {
        new ProbabilityItem(){key="mid-food", probability=0.6f},
        new ProbabilityItem(){key="vine-staff", probability=0.8f},
        new ProbabilityItem(){key="low-pendants", probability=0.95f},
        new ProbabilityItem(){key="flower-crown", probability=1},
      }},
      {"high", new ProbabilityItem[] {
        new ProbabilityItem(){key="high-food", probability=0.6f},
        new ProbabilityItem(){key="vine-scepter", probability=0.8f},
        new ProbabilityItem(){key="mid-potions", probability=0.95f},
        new ProbabilityItem(){key="flower-wreath", probability=1},
      }}
    }},
    {"dwarf", new Dictionary<string, ProbabilityItem[]> {
      {"low", new ProbabilityItem[] {
        new ProbabilityItem(){key="low-food", probability=0.7f},
        new ProbabilityItem(){key="low-money", probability=0.85f},
        new ProbabilityItem(){key="silver-bar", probability=0.95f},
        new ProbabilityItem(){key="wine", probability=1},
      }},
      {"mid", new ProbabilityItem[] {
        new ProbabilityItem(){key="mid-food", probability=0.7f},
        new ProbabilityItem(){key="axe", probability=0.85f},
        new ProbabilityItem(){key="silver-ingot", probability=0.95f},
        new ProbabilityItem(){key="dwarf-hammer", probability=1},
      }},
      {"high", new ProbabilityItem[] {
        new ProbabilityItem(){key="mid-food", probability=0.7f},
        new ProbabilityItem(){key="reinforced-axe", probability=0.85f},
        new ProbabilityItem(){key="diamond", probability=0.95f},
        new ProbabilityItem(){key="war-maul", probability=1},
      }}
    }},
    {"pixie", new Dictionary<string, ProbabilityItem[]> {
      {"low", new ProbabilityItem[] {
        new ProbabilityItem(){key="low-food", probability=0.5f},
        new ProbabilityItem(){key="low-potions", probability=0.8f},
        new ProbabilityItem(){key="low-money", probability=0.92f},
        new ProbabilityItem(){key="bronze-pixie-belt", probability=1},
      }},
      {"mid", new ProbabilityItem[] {
        new ProbabilityItem(){key="mid-food", probability=0.5f},
        new ProbabilityItem(){key="gold-bar", probability=0.8f},
        new ProbabilityItem(){key="mid-potions", probability=0.92f},
        new ProbabilityItem(){key="silver-pixie-belt", probability=1},
      }},
      {"high", new ProbabilityItem[] {
        new ProbabilityItem(){key="high-food", probability=0.5f},
        new ProbabilityItem(){key="gold-ingot", probability=0.8f},
        new ProbabilityItem(){key="mid-potions", probability=0.92f},
        new ProbabilityItem(){key="gold-pixie-belt", probability=1},
      }}
    }},
    {"skeleton-king", new Dictionary<string, ProbabilityItem[]> {
      {"low", new ProbabilityItem[] {
        new ProbabilityItem(){key="giant-bone", probability=0.7f},
        new ProbabilityItem(){key="mid-potions", probability=0.9f},
        new ProbabilityItem(){key="royal-pelt", probability=0.97f},
        new ProbabilityItem(){key="king-skull", probability=1},
      }},
      {"mid", new ProbabilityItem[] {
        new ProbabilityItem(){key="giant-bone", probability=0.7f},
        new ProbabilityItem(){key="high-potions", probability=0.9f},
        new ProbabilityItem(){key="gold-ingot", probability=0.97f},
        new ProbabilityItem(){key="polished-king-skull", probability=1},
      }},
      {"high", new ProbabilityItem[] {
        new ProbabilityItem(){key="giant-bone", probability=0.7f},
        new ProbabilityItem(){key="elixir", probability=0.9f},
        new ProbabilityItem(){key="citrine", probability=0.97f},
        new ProbabilityItem(){key="silver-king-skull", probability=1},
      }}
    }}
  };

  public static Dictionary<string, string[]> itemGroups = new Dictionary<string, string[]> {
    {"goblin-high-item4", Constants.goblinHighItem4},
    {"goblin-knives", Constants.goblinKnives},
    {"goblin-mid-item4", Constants.goblinMidItem4},
    {"high-food", Constants.highLevelFood},
    {"high-potions", Constants.highLevelPotions},
    {"low-bracelets", Constants.lowLevelBracelets},
    {"low-food", Constants.lowLevelFood},
    {"low-money", Constants.lowLevelMoney},
    {"low-pendants", Constants.lowLevelPendants},
    {"low-potions", Constants.lowLevelPotions},
    {"mid-food", Constants.midLevelFood},
    {"mid-money", Constants.midLevelMoney},
    {"mid-potions", Constants.midLevelPotions}
  };

  public static Dictionary<string, MoneyItem> moneyItems = new Dictionary<string, MoneyItem> {
    {"money-50", new MoneyItem(){ image = Sprites.moneyImages[0], increment = 50, text = "$50"}},
    {"money-100", new MoneyItem(){ image = Sprites.moneyImages[1], increment = 100, text = "$100"}},
    {"money-200", new MoneyItem(){ image = Sprites.moneyImages[2], increment = 200, text = "$200"}},
    {"money-500", new MoneyItem(){ image = Sprites.moneyImages[3], increment = 500, text = "$500"}},
    {"money-1000", new MoneyItem(){ image = Sprites.moneyImages[4], increment = 1000, text = "$1000"}},
    {"money-2000", new MoneyItem(){ image = Sprites.moneyImages[5], increment = 2000, text = "$2000"}},
    {"money-5000", new MoneyItem(){ image = Sprites.moneyImages[6], increment = 5000, text = "$5000"}}
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
    {"dwarf", new EnemyStats() {name = "Dwarf", type = "patroller", hp = 50, atk = 20, def = 80, crit = 0.05f, exp = 30, speed= 5, reach = 0.5f, groundCastLength = 1, arrowBurnPosition = 1}},
    {"goblin", new EnemyStats() {name = "Goblin", type = "patroller", hp = 80, atk = 40, def = 30, crit = 0.015f, exp = 60, speed= 3, reach = 0.5f, groundCastLength = 1, arrowBurnPosition = 1}},
    {"nymph", new EnemyStats() {name = "Nymph", type = "patroller", hp = 50, atk = 10, def = 40, crit = 0.03f, exp = 80, speed= 4, reach = 0.5f, groundCastLength = 1, arrowBurnPosition = 1}},
    {"pixie", new EnemyStats() {name = "Pixie", type = "patroller", hp = 100, atk = 10, def = 60, crit = 0.01f, exp = 100, speed= 3, reach = 0.25f, groundCastLength = 1.5f, arrowBurnPosition = 0.5f}},
    {"skeleton", new EnemyStats() {name = "Skeleton", type = "patroller", hp = 100, atk = 20, def = 10, crit = 0.01f, exp = 30, speed= 3, reach = 0.5f, groundCastLength = 1, arrowBurnPosition = 1}},
    {"skeleton-king", new EnemyStats() {name = "Skeleton King", type = "champion", hp = 300, atk = 60, def = 30, crit = 0.15f, exp = 100, speed= 2, reach = 1, groundCastLength = 1, arrowBurnPosition = 1.5f}}
  };

  public static Dictionary<string, Vector2> enemyDimensions = new Dictionary<string, Vector2> {
    {"dwarf", new Vector2(0.76f, 1.44f)},
    {"goblin", new Vector2(1.12f, 2.06f)},
    {"nymph", new Vector2(0.72f, 2.34f)},
    {"pixie", new Vector2(0.7f, 0.68f)},
    {"skeleton", new Vector2(1.77f, 2.05f)},
    {"skeleton-king", new Vector2(1.82f, 3.21f)}
  };

  public static Dictionary<string, TMP_FontAsset> fonts = new Dictionary<string, TMP_FontAsset> {
    {"levi-rebrushed", Resources.Load("Fonts/LeviRebrushed SDF") as TMP_FontAsset}
  };

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
      Resources.Load<AudioClip>("SFX/breakables/box/box-1"),
      Resources.Load<AudioClip>("SFX/breakables/box/box-2"),
      Resources.Load<AudioClip>("SFX/breakables/box/box-3"),
    }}
  };
}
