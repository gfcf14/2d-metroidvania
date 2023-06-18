using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprites {
  public static Sprite[] elements = Resources.LoadAll<Sprite>("Pause/elements");
  public static Sprite[] keyset = Resources.LoadAll<Sprite>("Spritesheets/keyset");
  public static Sprite[] itemThumbnails = Resources.LoadAll<Sprite>("Pause/Items/thumbnails");
  public static Sprite[] itemCombinedThumbnails = Resources.LoadAll<Sprite>("Pause/Items/combined-thumbnails");
  public static Sprite[] itemImages = Resources.LoadAll<Sprite>("Pause/Items/images");
  public static Sprite[] equipmentButtonIcons = Resources.LoadAll<Sprite>("Pause/equipment-icons");
  public static Sprite[] statsIcons = Resources.LoadAll<Sprite>("Pause/stats-icons");
  public static Sprite[] droppableImages = Resources.LoadAll<Sprite>("Spritesheets/droppables");
  public static Sprite[] moneyImages = Resources.LoadAll<Sprite>("Spritesheets/money");

  public static Sprite[] breakableBoxes = Resources.LoadAll<Sprite>("Spritesheets/box");
  public static Sprite[] breakableBarrels = Resources.LoadAll<Sprite>("Spritesheets/barrel");
  public static Sprite[] breakableJars = Resources.LoadAll<Sprite>("Spritesheets/jar");
  public static Sprite[] breakableVases = Resources.LoadAll<Sprite>("Spritesheets/vase");
  public static Sprite[] skeletonKingSprites = Resources.LoadAll<Sprite>("Spritesheets/skeleton-king");

  public static Sprite[] peasantGirlSprites = Resources.LoadAll<Sprite>("Spritesheets/npcs/peasant-girl");

  public static Dictionary<string, Sprite> equipmentIcons = new Dictionary<string, Sprite> {
    {"body", equipmentButtonIcons[0]},
    {"arm1", equipmentButtonIcons[1]},
    {"arm2", equipmentButtonIcons[5]},
    {"neck", equipmentButtonIcons[4]},
    {"armwear1", equipmentButtonIcons[2]},
    {"armwear2", equipmentButtonIcons[6]},
    {"ring1", equipmentButtonIcons[3]},
    {"ring2", equipmentButtonIcons[7]},
  };

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

  public static Dictionary<string, Sprite> keycodeSprites = new Dictionary<string, Sprite> {
    {"None", keyset[0]},
    {"Backspace", keyset[123]},
    {"Delete", keyset[1]},
    {"Tab", keyset[124]},
    {"Clear", keyset[2]},
    {"Return", keyset[125]},
    {"Pause", keyset[3]},
    {"Escape", keyset[4]},
    {"Space", keyset[121]},
    {"Keypad0", keyset[5]},
    {"Keypad1", keyset[6]},
    {"Keypad2", keyset[7]},
    {"Keypad3", keyset[8]},
    {"Keypad4", keyset[9]},
    {"Keypad5", keyset[10]},
    {"Keypad6", keyset[11]},
    {"Keypad7", keyset[12]},
    {"Keypad8", keyset[13]},
    {"Keypad9", keyset[14]},
    {"KeypadPeriod", keyset[15]},
    {"KeypadDivide", keyset[16]},
    {"KeypadMultiply", keyset[17]},
    {"KeypadMinus", keyset[18]},
    {"KeypadPlus", keyset[19]},
    {"KeypadEnter", keyset[20]},
    {"KeypadEquals", keyset[21]},
    {"UpArrow", keyset[22]},
    {"DownArrow", keyset[23]},
    {"RightArrow", keyset[24]},
    {"LeftArrow", keyset[25]},
    {"Insert", keyset[26]},
    {"Home", keyset[27]},
    {"End", keyset[28]},
    {"PageUp", keyset[29]},
    {"PageDown", keyset[30]},
    {"F1", keyset[31]},
    {"F2", keyset[32]},
    {"F3", keyset[33]},
    {"F4", keyset[34]},
    {"F5", keyset[35]},
    {"F6", keyset[36]},
    {"F7", keyset[37]},
    {"F8", keyset[38]},
    {"F9", keyset[39]},
    {"F10", keyset[40]},
    {"F11", keyset[41]},
    {"F12", keyset[42]},
    {"F13", keyset[43]},
    {"F14", keyset[44]},
    {"F15", keyset[45]},
    {"Alpha0", keyset[46]},
    {"Alpha1", keyset[47]},
    {"Alpha2", keyset[48]},
    {"Alpha3", keyset[49]},
    {"Alpha4", keyset[50]},
    {"Alpha5", keyset[51]},
    {"Alpha6", keyset[52]},
    {"Alpha7", keyset[53]},
    {"Alpha8", keyset[54]},
    {"Alpha9", keyset[55]},
    {"Exclaim", keyset[56]},
    {"DoubleQuote", keyset[57]},
    {"Hash", keyset[58]},
    {"Dollar", keyset[59]},
    {"Percent", keyset[60]},
    {"Ampersand", keyset[61]},
    {"Quote", keyset[62]},
    {"LeftParen", keyset[63]},
    {"RightParen", keyset[64]},
    {"Asterisk", keyset[65]},
    {"Plus", keyset[66]},
    {"Comma", keyset[67]},
    {"Minus", keyset[68]},
    {"Period", keyset[69]},
    {"Slash", keyset[70]},
    {"Colon", keyset[71]},
    {"Semicolon", keyset[72]},
    {"Less", keyset[73]},
    {"Equals", keyset[74]},
    {"Greater", keyset[75]},
    {"Question", keyset[76]},
    {"At", keyset[77]},
    {"LeftBracket", keyset[78]},
    {"Backslash", keyset[79]},
    {"RightBracket", keyset[80]},
    {"Caret", keyset[81]},
    {"Underscore", keyset[82]},
    {"BackQuote", keyset[83]},
    {"A", keyset[84]},
    {"B", keyset[85]},
    {"C", keyset[86]},
    {"D", keyset[87]},
    {"E", keyset[88]},
    {"F", keyset[89]},
    {"G", keyset[90]},
    {"H", keyset[91]},
    {"I", keyset[92]},
    {"J", keyset[93]},
    {"K", keyset[94]},
    {"L", keyset[95]},
    {"M", keyset[96]},
    {"N", keyset[97]},
    {"O", keyset[98]},
    {"P", keyset[99]},
    {"Q", keyset[100]},
    {"R", keyset[101]},
    {"S", keyset[102]},
    {"T", keyset[103]},
    {"U", keyset[104]},
    {"V", keyset[105]},
    {"W", keyset[106]},
    {"X", keyset[107]},
    {"Y", keyset[108]},
    {"Z", keyset[109]},
    {"LeftCurlyBracket", keyset[110]},
    {"Pipe", keyset[111]},
    {"RightCurlyBracket", keyset[112]},
    {"Tilde", keyset[113]},
    {"Numlock", keyset[114]},
    {"CapsLock", keyset[126]},
    {"ScrollLock", keyset[115]},
    {"RightShift", keyset[122]},
    {"LeftShift", keyset[131]},
    {"RightControl", keyset[127]},
    {"LeftControl", keyset[128]},
    {"RightAlt", keyset[129]},
    {"LeftAlt", keyset[130]},
    {"LeftMeta", keyset[132]},
    {"LeftCommand", keyset[133]},
    {"LeftApple", keyset[132]},
    {"LeftWindows", keyset[134]},
    {"RightMeta", keyset[135]},
    {"RightCommand", keyset[136]},
    {"RightApple", keyset[135]},
    {"RightWindows", keyset[137]},
    {"AltGr", keyset[138]},
    {"Help", keyset[116]},
    {"Print", keyset[117]},
    {"SysReq", keyset[118]},
    {"Break", keyset[119]},
    {"Menu", keyset[120]},
    {"JoystickButton0", keyset[155]},
    {"JoystickButton1", keyset[169]},
    {"JoystickButton2", keyset[170]},
    {"JoystickButton3", keyset[154]},
    {"JoystickButton4", keyset[153]},
    {"JoystickButton5", keyset[168]},
    {"JoystickButton6", keyset[139]},
    {"JoystickButton7", keyset[156]},
    {"JoystickButton8", keyset[140]},
    {"JoystickButton9", keyset[141]},
    {"Joystick1Button0", keyset[155]},
    {"Joystick1Button1", keyset[169]},
    {"Joystick1Button2", keyset[170]},
    {"Joystick1Button3", keyset[154]},
    {"Joystick1Button4", keyset[153]},
    {"Joystick1Button5", keyset[168]},
    {"Joystick1Button6", keyset[139]},
    {"Joystick1Button7", keyset[156]},
    {"Joystick1Button8", keyset[140]},
    {"Joystick1Button9", keyset[141]},
    {"Joystick2Button0", keyset[155]},
    {"Joystick2Button1", keyset[169]},
    {"Joystick2Button2", keyset[170]},
    {"Joystick2Button3", keyset[154]},
    {"Joystick2Button4", keyset[153]},
    {"Joystick2Button5", keyset[168]},
    {"Joystick2Button6", keyset[139]},
    {"Joystick2Button7", keyset[156]},
    {"Joystick2Button8", keyset[140]},
    {"Joystick2Button9", keyset[141]},
    {"Joystick3Button0", keyset[155]},
    {"Joystick3Button1", keyset[169]},
    {"Joystick3Button2", keyset[170]},
    {"Joystick3Button3", keyset[154]},
    {"Joystick3Button4", keyset[153]},
    {"Joystick3Button5", keyset[168]},
    {"Joystick3Button6", keyset[139]},
    {"Joystick3Button7", keyset[156]},
    {"Joystick3Button8", keyset[140]},
    {"Joystick3Button9", keyset[141]},
    {"Joystick4Button0", keyset[155]},
    {"Joystick4Button1", keyset[169]},
    {"Joystick4Button2", keyset[170]},
    {"Joystick4Button3", keyset[154]},
    {"Joystick4Button4", keyset[153]},
    {"Joystick4Button5", keyset[168]},
    {"Joystick4Button6", keyset[139]},
    {"Joystick4Button7", keyset[156]},
    {"Joystick4Button8", keyset[140]},
    {"Joystick4Button9", keyset[141]},
    {"Joystick5Button0", keyset[155]},
    {"Joystick5Button1", keyset[169]},
    {"Joystick5Button2", keyset[170]},
    {"Joystick5Button3", keyset[154]},
    {"Joystick5Button4", keyset[153]},
    {"Joystick5Button5", keyset[168]},
    {"Joystick5Button6", keyset[139]},
    {"Joystick5Button7", keyset[156]},
    {"Joystick5Button8", keyset[140]},
    {"Joystick5Button9", keyset[141]},
    {"Joystick6Button0", keyset[155]},
    {"Joystick6Button1", keyset[169]},
    {"Joystick6Button2", keyset[170]},
    {"Joystick6Button3", keyset[154]},
    {"Joystick6Button4", keyset[153]},
    {"Joystick6Button5", keyset[168]},
    {"Joystick6Button6", keyset[139]},
    {"Joystick6Button7", keyset[156]},
    {"Joystick6Button8", keyset[140]},
    {"Joystick6Button9", keyset[141]},
    {"Joystick7Button0", keyset[155]},
    {"Joystick7Button1", keyset[169]},
    {"Joystick7Button2", keyset[170]},
    {"Joystick7Button3", keyset[154]},
    {"Joystick7Button4", keyset[153]},
    {"Joystick7Button5", keyset[168]},
    {"Joystick7Button6", keyset[139]},
    {"Joystick7Button7", keyset[156]},
    {"Joystick7Button8", keyset[140]},
    {"Joystick7Button9", keyset[141]},
    {"Joystick8Button0", keyset[155]},
    {"Joystick8Button1", keyset[169]},
    {"Joystick8Button2", keyset[170]},
    {"Joystick8Button3", keyset[154]},
    {"Joystick8Button4", keyset[153]},
    {"Joystick8Button5", keyset[168]},
    {"Joystick8Button6", keyset[139]},
    {"Joystick8Button7", keyset[156]},
    {"Joystick8Button8", keyset[140]},
    {"Joystick8Button9", keyset[141]},
  };

  public static Dictionary<string, Sprite> locationImages = new Dictionary<string, Sprite> {
    {"meadows", Resources.Load<Sprite>("Pause/Locations/meadows")}
  };

  public static Dictionary<string, Sprite> pauseAvatars = new Dictionary<string, Sprite> {
    {"body-1", Resources.Load<Sprite>("Pause/PlayerAvatars/body-1")}
  };

  public static Dictionary<string, Sprite> throwableSprites = new Dictionary<string, Sprite> {
    {"lance", Resources.Load<Sprite>("Sprites/lance")},
    {"knife", Resources.Load<Sprite>("Sprites/knife")},
    {"kunai", Resources.Load<Sprite>("Sprites/kunai")},
    {"shuriken-4", Resources.Load<Sprite>("Sprites/shuriken-4")},
    {"shuriken-6", Resources.Load<Sprite>("Sprites/shuriken-6")},
    {"hatchet", Resources.Load<Sprite>("Sprites/hatchet")},
    {"axe", Resources.Load<Sprite>("Sprites/axe")},
    {"king-bone", skeletonKingSprites[20]}
  };

  public static Dictionary<string, Sprite> droppableSprites = new Dictionary<string, Sprite> {
    {"basic-longsword", droppableImages[0]},
    {"basic-sword", droppableImages[1]},
    {"basic-shield", droppableImages[2]},
    {"chicken-drumstick", droppableImages[3]},
    {"love-necklace", droppableImages[4]},
    {"solomon-ring", droppableImages[5]},
    {"ra-ring", droppableImages[6]},
    {"skull-ring", droppableImages[7]},
    {"gold-bracelet", droppableImages[8]},
    {"silver-bracelet", droppableImages[9]},
    {"rabbit-paw", droppableImages[10]},
    {"moonlight-pendant", droppableImages[11]},
    {"rainbow-bracer", droppableImages[12]},
    {"body-1", droppableImages[13]},
    {"lance", droppableImages[14]},
    {"axe", droppableImages[15]},
    {"hatchet", droppableImages[16]},
    {"shuriken-6", droppableImages[17]},
    {"shuriken-4", droppableImages[18]},
    {"knife", droppableImages[19]},
    {"kunai", droppableImages[20]},
    {"bow", droppableImages[21]},
    {"arrow-standard", droppableImages[22]},
    {"arrow-poison", droppableImages[23]},
    {"arrow-fire", droppableImages[24]},
    {"bomb", droppableImages[25]},
    {"diamond", droppableImages[26]},
    {"emerald", droppableImages[27]},
    {"sapphire", droppableImages[28]},
    {"citrine", droppableImages[29]},
    {"ruby", droppableImages[30]},
    {"opal", droppableImages[31]},
    {"amethyst", droppableImages[32]},
    {"gold-bar", droppableImages[33]},
    {"gold-ingot", droppableImages[34]},
    {"pearl", droppableImages[35]},
    {"potion", droppableImages[36]},
    {"mid-potion", droppableImages[37]},
    {"high-potion", droppableImages[38]},
    {"magic-ampoule", droppableImages[39]},
    {"magic-vial", droppableImages[40]},
    {"magic-bottle", droppableImages[41]},
    {"strength-flask", droppableImages[42]},
    {"stamina-flask", droppableImages[43]},
    {"critical-flask", droppableImages[44]},
    {"luck-flask", droppableImages[45]},
    {"random-flask", droppableImages[46]},
    {"theriac", droppableImages[47]},
    {"hashish", droppableImages[48]},
    {"earth-med", droppableImages[49]},
    {"air-med", droppableImages[50]},
    {"water-med", droppableImages[51]},
    {"fire-med", droppableImages[52]},
    {"lightning-med", droppableImages[53]},
    {"ice-med", droppableImages[54]},
    {"light-med", droppableImages[55]},
    {"dark-med", droppableImages[56]},
    {"elixir", droppableImages[57]},
    {"apple", droppableImages[58]},
    {"banana", droppableImages[59]},
    {"orange", droppableImages[60]},
    {"pear", droppableImages[61]},
    {"strawberry", droppableImages[62]},
    {"cherry", droppableImages[63]},
    {"grapes", droppableImages[64]},
    {"pineapple", droppableImages[65]},
    {"mango", droppableImages[66]},
    {"coconut", droppableImages[67]},
    {"honeydew", droppableImages[68]},
    {"watermelon", droppableImages[69]},
    {"watermelon-slice", droppableImages[70]},
    {"silver-bar", droppableImages[71]},
    {"silver-ingot", droppableImages[72]},
    {"bronze-bar", droppableImages[73]},
    {"bronze-ingot", droppableImages[74]},
    {"skull", droppableImages[75]},
    {"polished-skull", droppableImages[76]},
    {"silver-skull", droppableImages[77]},
    {"calcite", droppableImages[78]},
    {"refined-calcite", droppableImages[79]},
    {"precious-calcite", droppableImages[80]},
    {"darkling-sword", droppableImages[81]},
    {"darklord-sword", droppableImages[82]},
    {"spices", droppableImages[83]},
    {"textiles", droppableImages[84]},
    {"bandit-knife", droppableImages[85]},
    {"bandit-rapier", droppableImages[86]},
    {"laurel-crown", droppableImages[87]},
    {"flower-crown", droppableImages[88]},
    {"flower-wreath", droppableImages[89]},
    {"vine-staff", droppableImages[90]},
    {"vine-scepter", droppableImages[91]},
    {"wine", droppableImages[92]},
    {"reinforced-axe", droppableImages[93]},
    {"dwarf-hammer", droppableImages[95]},
    {"war-maul", droppableImages[95]},
    {"bronze-pixie-belt", droppableImages[96]},
    {"silver-pixie-belt", droppableImages[97]},
    {"gold-pixie-belt", droppableImages[98]},
    {"draco-shield", droppableImages[99]},
    {"king-skull", droppableImages[100]},
    {"polished-king-skull", droppableImages[101]},
    {"silver-king-skull", droppableImages[102]},
    {"royal-pelt", droppableImages[103]},
    {"giant-bone", droppableImages[104]}
  };

  public static Dictionary<string, Sprite> breakableSprites = new Dictionary<string, Sprite> {
    {"barrel", breakableBarrels[0]},
    {"box", breakableBoxes[0]},
    {"jar", breakableJars[0]},
    {"vase", breakableVases[0]}
  };

  public static Dictionary<string, Sprite> uiElements = new Dictionary<string, Sprite> {
    {"checkbox-checked", Resources.Load<Sprite>("Sprites/UI/Pause/checkbox-checked")},
    {"checkbox-unchecked", Resources.Load<Sprite>("Sprites/UI/Pause/checkbox-unchecked")},
    {"radio-checked", Resources.Load<Sprite>("Sprites/UI/Pause/radio-checked")},
    {"radio-unchecked", Resources.Load<Sprite>("Sprites/UI/Pause/radio-unchecked")}
  };

  public static Dictionary<string, Sprite> firstBossSprites = new Dictionary<string, Sprite> {
    {"skeleton-king", skeletonKingSprites[0]}
  };

  public static Dictionary<string, Dictionary<string, Sprite>> emotions = new Dictionary<string, Dictionary<string, Sprite>> {
    {"peasant-girl", new Dictionary<string, Sprite> {
      {"affective", peasantGirlSprites[10]},
      {"afraid", peasantGirlSprites[19]},
      {"angry", peasantGirlSprites[14]},
      {"ashamed", peasantGirlSprites[16]},
      {"confused", peasantGirlSprites[18]},
      {"happy", peasantGirlSprites[11]},
      {"jealous", peasantGirlSprites[17]},
      {"lonely", peasantGirlSprites[13]},
      {"sad", peasantGirlSprites[12]},
      {"surprised", peasantGirlSprites[15]}
    }}
  };
}
