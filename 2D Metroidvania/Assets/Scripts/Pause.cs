using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Pause : MonoBehaviour {
  // Canvases
  [Header("Canvases")]
  [SerializeField] GameObject mainCanvas;
  [SerializeField] GameObject itemsCanvas;
  [SerializeField] GameObject equipmentCanvas;
  [SerializeField] GameObject optionsCanvas;
  [SerializeField] GameObject controlsCanvas;
  [SerializeField] GameObject preferredInputCanvas;
  [SerializeField] GameObject quitCanvas;
  [Space(10)]

  // Objects to select first upon reaching a canvas
  [Header("First Selections")]
  [SerializeField] GameObject pauseFirstSelected;
  [SerializeField] GameObject itemsButton;
  [SerializeField] GameObject equipmentButton;
  [SerializeField] GameObject equipmentFirstSelected;
  [SerializeField] GameObject optionsButton;
  [SerializeField] GameObject optionsFirstSelected;
  [SerializeField] GameObject controlsButton;
  [SerializeField] GameObject controlsFirstSelected;
  [SerializeField] GameObject preferredInputButton;
  [SerializeField] GameObject preferredInputFirstSelected;
  [SerializeField] GameObject quitButton;
  [SerializeField] GameObject quitFirstSelected;
  [Space(10)]

  // Effects Objects
  [Header("Effects")]
  [SerializeField] GameObject effectsCurrentHP;
  [SerializeField] GameObject effectsTotalHP;
  [SerializeField] GameObject effectsCurrentMP;
  [SerializeField] GameObject effectsTotalMP;
  [SerializeField] GameObject effectsATK1;
  [SerializeField] GameObject effectsATK2;
  [SerializeField] GameObject effectsDEF1;
  [SerializeField] GameObject effectsDEF2;
  [SerializeField] GameObject effectsCritical;
  [SerializeField] GameObject effectsLuck;

  // Item Objects
  [Header("Item Objects")]
  [SerializeField] GameObject itemsContainer;
  [SerializeField] GameObject itemImage;
  [SerializeField] GameObject itemName;
  [SerializeField] GameObject itemDescription;
  [SerializeField] GameObject itemEffectsObject;
  [SerializeField] GameObject itemUseRectangle;
  [SerializeField] GameObject itemUseYes;
  [Space(10)]

  // Equipment Objects
  [Header("Equipment Objects")]
  [SerializeField] GameObject equipmentContainer;
  [SerializeField] GameObject bodyButton;
  [SerializeField] GameObject arm1Button;
  [SerializeField] GameObject arm2Button;
  [SerializeField] GameObject neckButton;
  [SerializeField] GameObject armwear1Button;
  [SerializeField] GameObject armwear2Button;
  [SerializeField] GameObject ring1Button;
  [SerializeField] GameObject ring2Button;
  [SerializeField] GameObject EquippedSTRLabel;
  [SerializeField] GameObject EquippedSTALabel;
  [SerializeField] GameObject EquippedATK1Label;
  [SerializeField] GameObject EquippedATK2Label;
  [SerializeField] GameObject EquippedDEF1Label;
  [SerializeField] GameObject EquippedDEF2Label;
  [SerializeField] GameObject EquippedCRITLabel;
  [SerializeField] GameObject EquippedLUCKLabel;

  [Space(10)]

  // Control buttons
  [Header("Jump Controls")]
  [SerializeField] GameObject jumpButton;
  [SerializeField] GameObject jumpKeyboardImage;
  [SerializeField] GameObject jumpGamepadImage;
  [Space(10)]

  [Header("ATK1 Controls")]
  [SerializeField] GameObject atk1Button;
  [SerializeField] GameObject atk1KeyboardImage;
  [SerializeField] GameObject atk1GamepadImage;
  [Space(10)]

  [Header("ATK2 Controls")]
  [SerializeField] GameObject atk2Button;
  [SerializeField] GameObject atk2KeyboardImage;
  [SerializeField] GameObject atk2GamepadImage;
  [Space(10)]

  // Awaiting Input Objects
  [Header("Awaiting Input")]
  [SerializeField] GameObject jumpAwaitLabel;
  [SerializeField] GameObject atk1AwaitLabel;
  [SerializeField] GameObject atk2AwaitLabel;
  [SerializeField] GameObject resetButton;
  [SerializeField] GameObject resetRectangle;
  [SerializeField] GameObject resetYes;
  [Space(10)]

  // main footer legends
  [Header("Footer Legends")]
  [SerializeField] GameObject mainButtonPanel;
  [SerializeField] GameObject mainKeysPanel;
  [Space(10)]

  // dynamic hero objects
  [Header("Hero Objects")]
  [SerializeField] GameObject hero;
  [SerializeField] GameObject playerAvatar;
  [SerializeField] GameObject level;
  [SerializeField] GameObject currentHPObject;
  [SerializeField] GameObject totalHP;
  [SerializeField] GameObject currentMPObject;
  [SerializeField] GameObject totalMP;
  [SerializeField] GameObject statusObject;
  [SerializeField] GameObject expObject;
  [SerializeField] GameObject nextObject;
  [SerializeField] GameObject goldObject;
  [SerializeField] GameObject strObject;
  [SerializeField] GameObject staObject;
  [SerializeField] GameObject atk1Object;
  [SerializeField] GameObject atk2Object;
  [SerializeField] GameObject def1Object;
  [SerializeField] GameObject def2Object;
  [SerializeField] GameObject luck;
  [SerializeField] GameObject critical;
  [SerializeField] GameObject locationObject;
  [Space(10)]

  // magic resistances
  [Header("Magic Resistances")]
  [SerializeField] GameObject resistance1Object;
  [SerializeField] GameObject resistance2Object;
  [SerializeField] GameObject resistance3Object;
  [SerializeField] GameObject resistance4Object;
  [SerializeField] GameObject resistance5Object;
  [SerializeField] GameObject resistance6Object;
  [SerializeField] GameObject resistance7Object;
  [SerializeField] GameObject resistance8Object;
  [SerializeField] GameObject magicEmptyObject;
  [Space(10)]

  // miscellaneous
  [Header("Miscellaneous")]
  [SerializeField] GameObject preferredInputObject;
  [SerializeField] public static EventSystem eventSystem;
  [Space(10)]

  [System.NonSerialized] bool hasGamepad = false;

  // variables to keep track of stats
  [System.NonSerialized] Hero heroScript;
  [System.NonSerialized] string bodyEquipment = "";
  [System.NonSerialized] int playerLevel = -1;
  [System.NonSerialized] int currentHP = -1;
  [System.NonSerialized] int maxHP = -1;
  [System.NonSerialized] int currentMP = -1;
  [System.NonSerialized] int maxMP = -1;
  [System.NonSerialized] string status = "";
  [System.NonSerialized] int exp = -1;
  [System.NonSerialized] int next = -1;
  [System.NonSerialized] int gold = -1;
  [System.NonSerialized] int strength = -1;
  [System.NonSerialized] int stamina = -1;
  [System.NonSerialized] int atk1 = -1;
  [System.NonSerialized] int atk2 = -1;
  [System.NonSerialized] int def1 = -1;
  [System.NonSerialized] int def2 = -1;
  [System.NonSerialized] float luckPercentage = -0.1f;
  [System.NonSerialized] float criticalPercentage = -0.1f;
  [System.NonSerialized] string location = "";
  [System.NonSerialized] string magicResistances = " ";

  // variables to keep track of preferences
  [System.NonSerialized] string preferredInputString = "";
  [System.NonSerialized] string jumpKeyboardString = "";
  [System.NonSerialized] string jumpGamepadString = "";
  [System.NonSerialized] string atk1KeyboardString = "";
  [System.NonSerialized] string atk1GamepadString = "";
  [System.NonSerialized] string atk2KeyboardString = "";
  [System.NonSerialized] string atk2GamepadString = "";

  // variables to keep track of equipment
  [System.NonSerialized] string bodyEquipmentKey = "";
  [System.NonSerialized] string arm1EquipmentKey = "";
  [System.NonSerialized] string arm2EquipmentKey = "";
  [System.NonSerialized] string neckEquipmentKey = "";
  [System.NonSerialized] string armwear1EquipmentKey = "";
  [System.NonSerialized] string armwear2EquipmentKey = "";
  [System.NonSerialized] string ring1EquipmentKey = "";
  [System.NonSerialized] string ring2EquipmentKey = "";

  // current mapping button
  [System.NonSerialized] public static string currentlyMapping = "";
  // current pause status (to match with canvas display)
  [System.NonSerialized] public static string canvasStatus = "main";
  // items list for added item buttons
  [System.NonSerialized] public List<GameObject> itemButtons = new List<GameObject>();
  // current index to display info
  [System.NonSerialized] public int currentItemButtonIndex = -1;
  // tracks the previously selected game button to return to after equipping or cancelling
  [System.NonSerialized] GameObject previouslySelectedEquipmentButton;
  // tracks which item list is currently visible
  [System.NonSerialized] List<Item> currentEquipmentItems;
  // tracks which equipment should be compared to a new equipment
  [System.NonSerialized] int currentlyEquippedIndex;

  void Start() {
    heroScript = hero.GetComponent<Hero>();
    eventSystem = EventSystem.current;
  }

  void Update() {
    CheckIfGamepad();
    UpdateControls();
    UpdatePreferredInput();
    UpdatePlayerStats();
    UpdateMagicResistances();
    UpdateItemView();
    UpdateEquipmentPromptButtons();
  }

  void FadeOut() {
    currentEquipmentItems.Clear();
    canvasStatus = "";
    itemsCanvas.SetActive(false);
    itemsContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    equipmentCanvas.SetActive(false);
    equipmentContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    optionsCanvas.SetActive(false);
    controlsCanvas.SetActive(false);
    preferredInputCanvas.SetActive(false);
    quitCanvas.SetActive(false);
    mainCanvas.SetActive(true);
    gameObject.SetActive(false);
    Helpers.FocusUIElement(null);
    Time.timeScale = 1;
  }

  void SelectItemsButton() {
    canvasStatus = "main";
    Helpers.FocusUIElement(pauseFirstSelected);
  }

  public void ShowItemsCanvas() {
    canvasStatus = "items";
    mainCanvas.SetActive(false);
    itemsCanvas.SetActive(true);

    ClearItems(itemsContainer);
    PopulateItemsContainer(heroScript.items, true, itemsContainer);
    Helpers.FocusUIElement(itemButtons.ElementAt(0));
    SetItemInfo(0);
  }

  // destroys all current children of the items container to avoid duplicating
  void ClearItems(GameObject container) {
    foreach (Transform child in container.transform) {
      GameObject.Destroy(child.gameObject);
    }
    itemButtons.Clear();
  }

  // adds all items in the hero item list or equipment list
  void PopulateItemsContainer(List<Item> itemsList, bool isItemList, GameObject parentContainer) {
    int i = 0;
    foreach (Item currentItem in itemsList) {
      string currentKey = currentItem.key;
      int currentAmount = currentItem.amount;
      PauseItem currentPauseItem = Objects.pauseItems[currentKey];

      // TODO: do not add button (for equipment) if its amount is 2 (or less) and it's already equipped twice)
      // if ((canvasStatus == "equipment" && ((Helpers.IsValueInArraySeveralTimes(heroScript.equipmentArray, currentKey, 2) && currentAmount > 2) || !Helpers.IsValueInArraySeveralTimes(heroScript.equipmentArray, currentKey, 2))) || canvasStatus == "items") {
        GameObject currentItemButton = Instantiate(Objects.prefabs["item-button"], Vector2.zero, Quaternion.identity);

        currentItemButton.transform.SetParent(parentContainer.transform);
        currentItemButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Constants.startItemY + (i * Constants.itemIncrementY * -1));
        currentItemButton.transform.localScale = Vector3.one;
        currentItemButton.transform.Find("Image").gameObject.GetComponent<Image>().sprite = currentPauseItem.thumbnail;
        currentItemButton.transform.Find("Text").gameObject.GetComponent<Text>().text = currentPauseItem.name;

        if (isItemList) {
          currentItemButton.transform.Find("Amount").gameObject.GetComponent<Text>().text = currentAmount.ToString();
        }

        itemButtons.Add(currentItemButton);

        if (i > 1 && i < itemsList.Count()) {
          Navigation buttonNavigation = new Navigation();
          buttonNavigation.mode = Navigation.Mode.Explicit;
          buttonNavigation.selectOnDown = currentItemButton.GetComponent<Button>();
          buttonNavigation.selectOnUp = itemButtons.ElementAt(i - 2).GetComponent<Button>();

          itemButtons.ElementAt(i - 1).GetComponent<Button>().navigation = buttonNavigation;
        }

        if (i == itemsList.Count() - 1 && itemsList.Count() > 1) {
          Navigation lastButtonNavigation = new Navigation();
          lastButtonNavigation.mode = Navigation.Mode.Explicit;
          lastButtonNavigation.selectOnDown = itemButtons.ElementAt(0).GetComponent<Button>();
          lastButtonNavigation.selectOnUp = itemButtons.ElementAt(i - 1).GetComponent<Button>();

          itemButtons.ElementAt(i).GetComponent<Button>().navigation = lastButtonNavigation;

          Navigation firstButtonNavigation = new Navigation();
          firstButtonNavigation.mode = Navigation.Mode.Explicit;
          firstButtonNavigation.selectOnDown = itemButtons.ElementAt(1).GetComponent<Button>();
          firstButtonNavigation.selectOnUp = itemButtons.ElementAt(i).GetComponent<Button>();

          itemButtons.ElementAt(0).GetComponent<Button>().navigation = firstButtonNavigation;
        }

        if (canvasStatus == "items" && Helpers.IsUsableItem(currentPauseItem.type)) {
          currentItemButton.GetComponent<Button>().onClick.AddListener(ProceedToUse);
        }

        if (canvasStatus == "equipment") {
          currentItemButton.GetComponent<Button>().onClick.AddListener(Equip);
        }
      // }

      i++;
    }
  }

  public void ProceedToUse() {
    canvasStatus = "items_use";
    Helpers.FocusUIElement(itemUseYes);
  }

  public void Equip() {
    string newItemKey = currentEquipmentItems.ElementAt(currentItemButtonIndex).key;
    heroScript.EquipItem(newItemKey, currentlyEquippedIndex);
    CancelEquipmentSelection();
  }

  public void UseItem() {
    Item heroItem = heroScript.items.ElementAt(currentItemButtonIndex);
    PauseItem currentPauseItem = Objects.pauseItems[heroItem.key];

    if (currentPauseItem.effects != null) {
      Effects itemEffects = currentPauseItem.effects;

      if (itemEffects.hp != null) {
        effectsCurrentHP.transform.Find("Text").gameObject.GetComponent<Text>().text = (itemEffects.hp >= 0 ? "+" : "") + itemEffects.hp;
        effectsCurrentHP.SetActive(true);

        // TODO: ensure that this can be either temporary (for potions of limited time use) or permanent (for single use items)
        heroScript.UpdateStats("hp", itemEffects.hp);
      }

      // TODO: build the others as more items are created!

      // determines what to do with the item and its amount
      if (heroItem.amount > 1) {
        heroScript.ConsumeItem(heroItem.key);
        itemButtons.ElementAt(0).transform.Find("Amount").gameObject.GetComponent<Text>().text = (heroItem.amount).ToString();
        Helpers.FocusUIElement(itemButtons.ElementAt(currentItemButtonIndex));
      } else {
        heroScript.RemoveItem(currentItemButtonIndex);
        ClearItems(itemsContainer);
        PopulateItemsContainer(heroScript.items, true, itemsContainer);
        Helpers.FocusUIElement(itemButtons.ElementAt(0));
        SetItemInfo(0);
      }
    }

    canvasStatus = "items";
  }

  public void CancelItemUse() {
    canvasStatus = "items";
    Helpers.FocusUIElement(itemButtons.ElementAt(currentItemButtonIndex));
  }

  public void CancelEquipmentSelection() {
    HideEquipmentLabels();
    canvasStatus = "equipment";
    Helpers.FocusUIElement(previouslySelectedEquipmentButton);
    previouslySelectedEquipmentButton = null;
  }

  void UpdateItemView() {
    if (Helpers.IsOnItemContainerState(Constants.itemContainerStates, canvasStatus) && itemButtons.Count > 0) {
      int i = 0;
      GameObject currentContainer = canvasStatus == "items" ? itemsContainer : canvasStatus == "equipment_select" ? equipmentContainer : null;

      foreach (GameObject currentItemButton in itemButtons) {
        if (eventSystem.currentSelectedGameObject == currentItemButton) {
          // do not change item view info if indices match; this means the item is already displaying
          if (currentItemButtonIndex != i) {
            int indexDifference = i - currentItemButtonIndex;
            // going down
            if (indexDifference == 1) {
              if (i > Constants.maxItemContainerHeight - 1) {
                int movingItemLocation = (int)(Constants.startItemY - (Constants.itemIncrementY * (Constants.maxItemContainerHeight - 1)));
                int selectedItemLocation = (int)(itemButtons.ElementAt(i - 1).GetComponent<RectTransform>().anchoredPosition.y + currentContainer.GetComponent<RectTransform>().anchoredPosition.y);

                // to avoid moving the container up if the selected button is not at the bottom
                if (selectedItemLocation == movingItemLocation) {
                  currentContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentContainer.GetComponent<RectTransform>().anchoredPosition.x, currentContainer.GetComponent<RectTransform>().anchoredPosition.y + Constants.itemIncrementY);
                }
              }
            } else if (indexDifference == -1) { //going up
              if (i < Constants.maxItemContainerHeight + 2) {
                int movingItemLocation = (int)(Constants.startItemY - (Constants.itemIncrementY * (itemButtons.Count - Constants.maxItemContainerHeight)));
                int selectedItemLocation = (int)(itemButtons.ElementAt(i).GetComponent<RectTransform>().anchoredPosition.y + (currentContainer.GetComponent<RectTransform>().anchoredPosition.y - ((itemButtons.Count + 1 - Constants.maxItemContainerHeight) * Constants.itemIncrementY)));

                // to avoid moving the container down if the selected button is not at the top
                if (selectedItemLocation == movingItemLocation) {
                  currentContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentContainer.GetComponent<RectTransform>().anchoredPosition.x, currentContainer.GetComponent<RectTransform>().anchoredPosition.y - Constants.itemIncrementY);
                }
              }
            } else if (indexDifference == itemButtons.Count - 1 && itemButtons.Count > Constants.maxItemContainerHeight) { // from first item to last
              currentContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentContainer.GetComponent<RectTransform>().anchoredPosition.x, (Constants.itemIncrementY * (itemButtons.Count - Constants.maxItemContainerHeight)));
            } else if (indexDifference == -(itemButtons.Count - 1)) { // from last item to first
              currentContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            }

            if (canvasStatus == "items") {
              SetItemInfo(i);
            } else if (canvasStatus == "equipment_select") {
              SetEquipmentProspect(i);
            }
          }
          break;
        }

        i++;
      }
    }
  }

  void HideEquipmentLabels() {
    EquippedSTRLabel.SetActive(false);
    EquippedSTALabel.SetActive(false);
    EquippedATK1Label.SetActive(false);
    EquippedATK2Label.SetActive(false);
    EquippedDEF1Label.SetActive(false);
    EquippedDEF2Label.SetActive(false);
    EquippedCRITLabel.SetActive(false);
    EquippedLUCKLabel.SetActive(false);
  }

  void SetEquipmentProspect(int index) {
    HideEquipmentLabels();

    currentItemButtonIndex = index;

    // grab current selected based on index
    PauseItem selectedEquipment = Objects.pauseItems[currentEquipmentItems.ElementAt(currentItemButtonIndex).key];
    // grab current equipped
    PauseItem currentEquipment = heroScript.equipmentArray[currentlyEquippedIndex] != "" ? Objects.pauseItems[heroScript.equipmentArray[currentlyEquippedIndex]] : null;

    // check if the selected equipment is a double handed item
    bool isEquippingDouble = selectedEquipment.type == "double";

    // check if the current equipment is a double handed item
    bool equippedIsDouble = currentEquipment != null ? currentEquipment.type == "double" : false;

    // Check ATK1
    if (currentlyEquippedIndex == 1 || (currentlyEquippedIndex == 2 && (isEquippingDouble || equippedIsDouble))) {
      PauseItem otherArmEquipment = heroScript.equipmentArray[1] != "" ? Objects.pauseItems[heroScript.equipmentArray[1]] : null;
      PauseItem equippedSelected = (currentlyEquippedIndex == 2 && (isEquippingDouble || equippedIsDouble)) ? otherArmEquipment : currentEquipment;

      int newEquippedATK1 = (selectedEquipment.effects.atk ?? 0) + (heroScript.equippedATK1 - (equippedSelected != null ? equippedSelected.effects.atk ?? 0 : 0));
      if (newEquippedATK1 != heroScript.equippedATK1) {
        Color equippedLabelColor = newEquippedATK1 > heroScript.equippedATK1 ? Colors.pauseStatsColors["higher"] : Colors.pauseStatsColors["lower"];

        Text equipmentText = EquippedATK1Label.transform.Find("Text").gameObject.GetComponent<Text>();
        equipmentText.text = newEquippedATK1 > 0 ? newEquippedATK1.ToString() : "0";
        equipmentText.color = equippedLabelColor;

        EquippedATK1Label.SetActive(true);
      }
    }

    // Check ATK2
    if (currentlyEquippedIndex == 2 || (currentlyEquippedIndex == 1 && (isEquippingDouble || equippedIsDouble))) {
      PauseItem otherArmEquipment = heroScript.equipmentArray[2] != "" ? Objects.pauseItems[heroScript.equipmentArray[2]] : null;
      PauseItem equippedSelected = (currentlyEquippedIndex == 1 && (isEquippingDouble || equippedIsDouble)) ? otherArmEquipment : currentEquipment;

      int newEquippedATK2 = (selectedEquipment.effects.atk ?? 0) + (heroScript.equippedATK2 - (equippedSelected != null ? equippedSelected.effects.atk ?? 0 : 0));
      if (newEquippedATK2 != heroScript.equippedATK2) {
        Color equippedLabelColor = newEquippedATK2 > heroScript.equippedATK2 ? Colors.pauseStatsColors["higher"] : Colors.pauseStatsColors["lower"];

        Text equipmentText = EquippedATK2Label.transform.Find("Text").gameObject.GetComponent<Text>();
        equipmentText.text = newEquippedATK2 > 0 ? newEquippedATK2.ToString() : "0";
        equipmentText.color = equippedLabelColor;

        EquippedATK2Label.SetActive(true);
      }
    }

    // Check DEF1
    if (currentlyEquippedIndex == 1 || (currentlyEquippedIndex == 2 && (isEquippingDouble))) {
      PauseItem otherArmEquipment = heroScript.equipmentArray[1] != "" ? Objects.pauseItems[heroScript.equipmentArray[1]] : null;
      PauseItem equippedSelected = (currentlyEquippedIndex == 2 && (isEquippingDouble || equippedIsDouble)) ? otherArmEquipment : currentEquipment;

      int newEquippedDEF1 = (selectedEquipment.effects.def ?? 0) + (heroScript.equippedDEF1 - (equippedSelected != null ? equippedSelected.effects.def ?? 0 : 0));
      if (newEquippedDEF1 != heroScript.equippedDEF1) {
        Color equippedLabelColor = newEquippedDEF1 > heroScript.equippedDEF1 ? Colors.pauseStatsColors["higher"] : Colors.pauseStatsColors["lower"];

        Text equipmentText = EquippedDEF1Label.transform.Find("Text").gameObject.GetComponent<Text>();
        equipmentText.text = newEquippedDEF1 > 0 ? newEquippedDEF1.ToString() : "0";
        equipmentText.color = equippedLabelColor;

        EquippedDEF1Label.SetActive(true);
      }
    }

    // Check DEF2
    if (currentlyEquippedIndex == 2 || (currentlyEquippedIndex == 1 && isEquippingDouble)) {
      PauseItem otherArmEquipment = heroScript.equipmentArray[2] != "" ? Objects.pauseItems[heroScript.equipmentArray[2]] : null;
      PauseItem equippedSelected = (currentlyEquippedIndex == 1 && (isEquippingDouble || equippedIsDouble)) ? otherArmEquipment : currentEquipment;

      int newEquippedDEF2 = (selectedEquipment.effects.def ?? 0) + (heroScript.equippedDEF2 - (equippedSelected != null ? equippedSelected.effects.def ?? 0 : 0));
      if (newEquippedDEF2 != heroScript.equippedDEF2) {
        Color equippedLabelColor = newEquippedDEF2 > heroScript.equippedDEF2 ? Colors.pauseStatsColors["higher"] : Colors.pauseStatsColors["lower"];

        Text equipmentText = EquippedDEF2Label.transform.Find("Text").gameObject.GetComponent<Text>();
        equipmentText.text = newEquippedDEF2 > 0 ? newEquippedDEF2.ToString() : "0";
        equipmentText.color = equippedLabelColor;

        EquippedDEF2Label.SetActive(true);
      }
    }

    // Check STR
    if (currentlyEquippedIndex > 2) {
      int totalEquippedSTR = heroScript.strength + (int)heroScript.equippedSTR;
      float newEquippedSTR = (selectedEquipment.effects.atk ?? 0) + (totalEquippedSTR - (currentEquipment != null ? currentEquipment.effects.atk ?? 0 : 0));
      if (newEquippedSTR != totalEquippedSTR) {
        Color equippedLabelColor = newEquippedSTR > totalEquippedSTR ? Colors.pauseStatsColors["higher"] : Colors.pauseStatsColors["lower"];

        Text equipmentText = EquippedSTRLabel.transform.Find("Text").gameObject.GetComponent<Text>();
        equipmentText.text = newEquippedSTR > 0 ? newEquippedSTR.ToString() : "0";
        equipmentText.color = equippedLabelColor;

        EquippedSTRLabel.SetActive(true);
      }
    }

    // Check STA
    if (currentlyEquippedIndex > 2) {
      int totalEquippedSTA = heroScript.stamina + (int)heroScript.equippedSTA;
      float newEquippedSTA = (selectedEquipment.effects.def ?? 0) + (totalEquippedSTA - (currentEquipment != null ? currentEquipment.effects.def ?? 0 : 0));
      if (newEquippedSTA != totalEquippedSTA) {
        Color equippedLabelColor = newEquippedSTA > totalEquippedSTA ? Colors.pauseStatsColors["higher"] : Colors.pauseStatsColors["lower"];

        Text equipmentText = EquippedSTALabel.transform.Find("Text").gameObject.GetComponent<Text>();
        equipmentText.text = newEquippedSTA > 0 ? newEquippedSTA.ToString() : "0";
        equipmentText.color = equippedLabelColor;

        EquippedSTALabel.SetActive(true);
      }
    }

    // Check CRIT
    float totalEquippedCRIT = heroScript.criticalPercentage + heroScript.equippedCRIT;
    float newEquippedCRIT = (selectedEquipment.effects.crit ?? 0) + (totalEquippedCRIT - (currentEquipment != null ? currentEquipment.effects.crit ?? 0 : 0));
    if (newEquippedCRIT != totalEquippedCRIT) {
      Color equippedLabelColor = newEquippedCRIT > totalEquippedCRIT ? Colors.pauseStatsColors["higher"] : Colors.pauseStatsColors["lower"];

      Text equipmentText = EquippedCRITLabel.transform.Find("Text").gameObject.GetComponent<Text>();
      equipmentText.text = newEquippedCRIT > 0 ? ((int)(newEquippedCRIT * 100)).ToString() + " %" : "0 %";
      equipmentText.color = equippedLabelColor;

      EquippedCRITLabel.SetActive(true);
    }

    // Check LUCK
    float totalEquippedLUCK = heroScript.luckPercentage + heroScript.equippedLUCK;
    float newEquippedLUCK = (selectedEquipment.effects.luck ?? 0) + (totalEquippedLUCK - (currentEquipment != null ? currentEquipment.effects.luck ?? 0 : 0));
    if (newEquippedLUCK != totalEquippedLUCK) {
      Color equippedLabelColor = newEquippedLUCK > totalEquippedLUCK ? Colors.pauseStatsColors["higher"] : Colors.pauseStatsColors["lower"];

      Text equipmentText = EquippedLUCKLabel.transform.Find("Text").gameObject.GetComponent<Text>();
      equipmentText.text = newEquippedLUCK > 0 ? ((int)(newEquippedLUCK * 100)).ToString() + " %" : "0 %";
      equipmentText.color = equippedLabelColor;

      EquippedLUCKLabel.SetActive(true);
    }

    // TODO: make a check for adding/removing magic resistances
  }

  void SetItemInfo(int index) {
    currentItemButtonIndex = index;

    PauseItem currentPauseItem = Objects.pauseItems[heroScript.items.ElementAt(currentItemButtonIndex).key];
    itemName.GetComponent<Text>().text = currentPauseItem.name.ToUpper();
    itemImage.GetComponent<Image>().sprite = currentPauseItem.image;
    itemDescription.GetComponent<Text>().text = currentPauseItem.description;
    itemUseRectangle.SetActive(Helpers.IsUsableItem(currentPauseItem.type));

    string effectsText = "None";

    if (currentPauseItem.effects != null) {
      Effects itemEffects = currentPauseItem.effects;
      effectsText = "";

      if (itemEffects.duration != null) {
        effectsText += itemEffects.duration + " " + (itemEffects.duration == 1 ? "sec" : "secs") + "\n";
      }

      if (itemEffects.hp != null) {
        effectsText += "HP" + (itemEffects.hp >= 0 ? "+" : "") + itemEffects.hp + "\n";
      }

      if (itemEffects.mp != null) {
        effectsText += "MP" + (itemEffects.mp >= 0 ? "+" : "") + itemEffects.mp + "\n";
      }

      if (itemEffects.statusHeal != null) {
        effectsText += "Heals ";

        int i = 0;
        foreach (string currStatusHeal in itemEffects.statusHeal) {
          effectsText += currStatusHeal + (i < itemEffects.statusHeal.Length - 1 ? ", " : "\n");
          i++;
        }
      }

      if (itemEffects.atk != null) {
        effectsText += "ATK" + (itemEffects.atk >= 0 ? "+" : "") + itemEffects.atk + "\n";
      }

      if (itemEffects.def != null) {
        effectsText += "DEF" + (itemEffects.def >= 0 ? "+" : "") + itemEffects.def + "\n";
      }

      if (itemEffects.crit != null) {
        effectsText += "CRIT" + (itemEffects.crit >= 0 ? "+" : "") + (int)(itemEffects.crit * 100) + "%\n";
      }

      if (itemEffects.luck != null) {
        effectsText += "LUCK" + (itemEffects.luck >= 0 ? "+" : "") + (int)(itemEffects.luck * 100) + "%\n";
      }

      if (itemEffects.magicResistances != null) {
        string addingResistances = "";
        string removingResistances = "";

        int i = 0;
        foreach (MagicResistance currMagicResistance in itemEffects.magicResistances) {
          if (currMagicResistance.type == "add") {
            if (addingResistances == "") {
              addingResistances += "Adds: ";
            }
            addingResistances += currMagicResistance.name + ", ";
          } else if (currMagicResistance.type == "remove") {
            if (removingResistances == "") {
              removingResistances += "Removes: ";
            }
            removingResistances += currMagicResistance.name + ", ";
          }
          i++;
        }

        addingResistances.TrimEnd(new Char[]{ ',', ' '});
        removingResistances.TrimEnd(new Char[]{ ',', ' '});

        effectsText += addingResistances + "\n" + removingResistances;
      }
    }

    itemEffectsObject.GetComponent<Text>().text = effectsText;
  }

  public void ShowEquipmentCanvas() {
    canvasStatus = "equipment";
    mainCanvas.SetActive(false);
    equipmentCanvas.SetActive(true);

    Helpers.FocusUIElement(equipmentFirstSelected);
  }

  public void ShowOptionsCanvas() {
    canvasStatus = "options";
    mainCanvas.SetActive(false);
    optionsCanvas.SetActive(true);

    Helpers.FocusUIElement(optionsFirstSelected);
  }

  public void ShowControlsCanvas() {
    canvasStatus = "options_controls";
    optionsCanvas.SetActive(false);
    controlsCanvas.SetActive(true);

    Helpers.FocusUIElement(controlsFirstSelected);
  }

  public void ShowPreferredInputCanvas() {
    canvasStatus = "options_preferred-input";
    optionsCanvas.SetActive(false);
    preferredInputCanvas.SetActive(true);

    Helpers.FocusUIElement(preferredInputFirstSelected);
  }

  public void ShowQuitCanvas() {
    canvasStatus = "quit";
    mainCanvas.SetActive(false);
    quitCanvas.SetActive(true);

    Helpers.FocusUIElement(quitFirstSelected);
  }

  public void GoBackToMainFromItems() {
    canvasStatus = "main";
    itemUseRectangle.SetActive(false);
    itemsCanvas.SetActive(false);
    mainCanvas.SetActive(true);

    Helpers.FocusUIElement(itemsButton);
  }

  public void GoBackToMainFromEquipment() {
    currentEquipmentItems.Clear();
    canvasStatus = "main";
    equipmentCanvas.SetActive(false);
    mainCanvas.SetActive(true);

    Helpers.FocusUIElement(equipmentButton);
  }

  public void GoBackToMainFromOptions() {
    canvasStatus = "main";
    optionsCanvas.SetActive(false);
    mainCanvas.SetActive(true);

    Helpers.FocusUIElement(optionsButton);
  }

  public void GoBackToOptionsFromControls() {
    canvasStatus = "options";
    controlsCanvas.SetActive(false);
    optionsCanvas.SetActive(true);

    Helpers.FocusUIElement(controlsButton);
  }

  public void GoBackToOptionsFromPreferredInput() {
    canvasStatus = "options";
    preferredInputCanvas.SetActive(false);
    optionsCanvas.SetActive(true);

    Helpers.FocusUIElement(preferredInputButton);
  }

  public void GoBackToMainFromQuit() {
    canvasStatus = "main";
    quitCanvas.SetActive(false);
    mainCanvas.SetActive(true);

    Helpers.FocusUIElement(quitButton);
  }

  public void PerformBack() {
    switch (canvasStatus) {
      case "items":
        GoBackToMainFromItems();
      break;
      case "items_use":
        CancelItemUse();
      break;
      case "equipment":
        GoBackToMainFromEquipment();
      break;
      case "equipment_select":
        CancelEquipmentSelection();
      break;
      case "options":
        GoBackToMainFromOptions();
      break;
      case "options_controls":
        GoBackToOptionsFromControls();
      break;
      case "options_controls_reset":
        CancelReset();
      break;
      case "options_preferred-input":
        GoBackToOptionsFromPreferredInput();
      break;
      case "quit":
        GoBackToMainFromQuit();
      break;
      default:
        Debug.Log("unknown canvas status: " + canvasStatus);
      break;
    }
  }

  public void QuitGame() {
    Debug.Log("should return to title screen");
  }

  void CheckIfGamepad() {
    List<string> validGamepads = new List<String>();
    foreach (string s in Input.GetJoystickNames()) {
      if (s != "") {
        validGamepads.Add(s);
      }
    }
    hasGamepad = validGamepads.Count > 0;

    if (hasGamepad && Constants.preferredInput == "gamepad" && mainButtonPanel.activeInHierarchy == false ) {
      ShowGamePadOptions();
    } else if ((!hasGamepad || Constants.preferredInput == "keyboard") && mainKeysPanel.activeInHierarchy == false) {
      ShowKeyboardOptions();
    }
  }

  void UpdateControls() {
    if (jumpKeyboardString != Controls.currentKeyboardJump) {
      jumpKeyboardString = Controls.currentKeyboardJump;
      jumpKeyboardImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[jumpKeyboardString];
    }

    if (jumpGamepadString != Controls.currentGamepadJump) {
      jumpGamepadString = Controls.currentGamepadJump;
      jumpGamepadImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[jumpGamepadString];
    }

    if (atk1KeyboardString != Controls.currentKeyboardAttack1) {
      atk1KeyboardString = Controls.currentKeyboardAttack1;
      atk1KeyboardImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[atk1KeyboardString];
    }

    if (atk1GamepadString != Controls.currentGamepadAttack1) {
      atk1GamepadString = Controls.currentGamepadAttack1;
      atk1GamepadImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[atk1GamepadString];
    }

    if (atk2KeyboardString != Controls.currentKeyboardAttack2) {
      atk2KeyboardString = Controls.currentKeyboardAttack2;
      atk2KeyboardImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[atk2KeyboardString];
    }

    if (atk2GamepadString != Controls.currentGamepadAttack2) {
      atk2GamepadString = Controls.currentGamepadAttack2;
      atk2GamepadImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[atk2GamepadString];
    }
  }

  void UpdatePreferredInput() {
    if (preferredInputString != Constants.preferredInput) {
      preferredInputString = Constants.preferredInput;
      preferredInputObject.GetComponent<Text>().text = preferredInputString.ToUpper();
    }
  }

  void UpdatePlayerStats() {
    if (bodyEquipment != Hero.bodyEquipment) {
      bodyEquipment = Hero.bodyEquipment;
      playerAvatar.GetComponent<Image>().sprite = Sprites.pauseAvatars[bodyEquipment];
    }

    if (playerLevel != heroScript.playerLevel) {
      playerLevel = heroScript.playerLevel;
      level.GetComponent<Text>().text = (playerLevel).ToString();
    }

    if (currentHP != heroScript.currentHP) {
      currentHP = heroScript.currentHP;
      currentHPObject.GetComponent<Text>().text = (currentHP).ToString();
    }

    if (maxHP != heroScript.maxHP) {
      maxHP = heroScript.maxHP;
      totalHP.GetComponent<Text>().text = (maxHP).ToString();
    }

    if (currentMP != heroScript.currentMP) {
      currentMP = heroScript.currentMP;
      currentMPObject.GetComponent<Text>().text = (currentMP).ToString();
    }

    if (maxMP != heroScript.maxMP) {
      maxMP = heroScript.maxMP;
      totalMP.GetComponent<Text>().text = (maxMP).ToString();
    }

    if (status != heroScript.status) {
      status = heroScript.status;
      statusObject.GetComponent<Text>().text = char.ToUpper(status[0]) + status.Substring(1);
    }

    if (exp != heroScript.exp) {
      exp = heroScript.exp;
      expObject.GetComponent<Text>().text = (exp).ToString();
    }

    if (next != heroScript.next) {
      next = heroScript.next;
      nextObject.GetComponent<Text>().text = (next).ToString();
    }

    if (gold != heroScript.gold) {
      gold = heroScript.gold;
      goldObject.GetComponent<Text>().text = (gold).ToString();
    }

    if (strength != (heroScript.strength + (int)heroScript.equippedSTR)) {
      strength = heroScript.strength + (int)heroScript.equippedSTR;
      strObject.GetComponent<Text>().text = (strength).ToString();
    }

    if (stamina != (heroScript.stamina + (int)heroScript.equippedSTA)) {
      stamina = heroScript.stamina + (int)heroScript.equippedSTA;
      staObject.GetComponent<Text>().text = (stamina).ToString();
    }

    if (atk1 != heroScript.equippedATK1) {
      atk1 = heroScript.equippedATK1;
      atk1Object.GetComponent<Text>().text = (atk1).ToString();
    }

    if (atk2 != heroScript.equippedATK2) {
      atk2 = heroScript.equippedATK2;
      atk2Object.GetComponent<Text>().text = (atk2).ToString();
    }

    if (def1 != heroScript.equippedDEF1) {
      def1 = heroScript.equippedDEF1;
      def1Object.GetComponent<Text>().text = (def1).ToString();
    }

    if (def2 != heroScript.equippedDEF2) {
      def2 = heroScript.equippedDEF2;
      def2Object.GetComponent<Text>().text = (def2).ToString();
    }

    if (criticalPercentage != heroScript.criticalPercentage) {
      criticalPercentage = heroScript.criticalPercentage + heroScript.equippedCRIT;
      critical.GetComponent<Text>().text = ((int)(criticalPercentage * 100)).ToString() + " %";
    }

    if (luckPercentage != heroScript.luckPercentage) {
      luckPercentage = heroScript.luckPercentage + heroScript.equippedLUCK;
      luck.GetComponent<Text>().text = ((int)(luckPercentage * 100)).ToString() + " %";
    }

    if (location != heroScript.location) {
      location = heroScript.location;
      locationObject.GetComponent<Image>().sprite = Sprites.locationImages[location];
    }
  }

  void UpdateEquipmentPromptButtons() {
    if (bodyEquipmentKey != Hero.bodyEquipment) {
      bodyEquipmentKey = Hero.bodyEquipment;

      if (bodyEquipmentKey == "") {
        bodyButton.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Sprites.equipmentIcons["body"];
        bodyButton.transform.Find("Text").gameObject.GetComponent<Text>().text = "None";
      } else {
        bodyButton.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Objects.pauseItems[bodyEquipmentKey].thumbnail;
        bodyButton.transform.Find("Text").gameObject.GetComponent<Text>().text = Objects.pauseItems[bodyEquipmentKey].name;
      }
    }

    if (arm1EquipmentKey != Hero.arm1Equipment) {
      arm1EquipmentKey = Hero.arm1Equipment;

      if (arm1EquipmentKey == "") {
        arm1Button.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Sprites.equipmentIcons["arm1"];
        arm1Button.transform.Find("Text").gameObject.GetComponent<Text>().text = "None";
      } else {
        arm1Button.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Objects.pauseItems[arm1EquipmentKey].thumbnail;
        arm1Button.transform.Find("Text").gameObject.GetComponent<Text>().text = Objects.pauseItems[arm1EquipmentKey].name;
      }
    }

    if (arm2EquipmentKey != Hero.arm2Equipment) {
      arm2EquipmentKey = Hero.arm2Equipment;

      if (arm2EquipmentKey == "") {
        arm2Button.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Sprites.equipmentIcons["arm2"];
        arm2Button.transform.Find("Text").gameObject.GetComponent<Text>().text = "None";
      } else {
        arm2Button.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Objects.pauseItems[arm2EquipmentKey].thumbnail;
        arm2Button.transform.Find("Text").gameObject.GetComponent<Text>().text = Objects.pauseItems[arm2EquipmentKey].name;
      }
    }

    if (neckEquipmentKey != Hero.neckEquipment) {
      neckEquipmentKey = Hero.neckEquipment;

      if (neckEquipmentKey == "") {
        neckButton.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Sprites.equipmentIcons["neck"];
        neckButton.transform.Find("Text").gameObject.GetComponent<Text>().text = "None";
      } else {
        neckButton.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Objects.pauseItems[neckEquipmentKey].thumbnail;
        neckButton.transform.Find("Text").gameObject.GetComponent<Text>().text = Objects.pauseItems[neckEquipmentKey].name;
      }
    }

    if (armwear1EquipmentKey != Hero.armwear1Equipment) {
      armwear1EquipmentKey = Hero.armwear1Equipment;

      if (armwear1EquipmentKey == "") {
        armwear1Button.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Sprites.equipmentIcons["armwear1"];
        armwear1Button.transform.Find("Text").gameObject.GetComponent<Text>().text = "None";
      } else {
        armwear1Button.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Objects.pauseItems[armwear1EquipmentKey].thumbnail;
        armwear1Button.transform.Find("Text").gameObject.GetComponent<Text>().text = Objects.pauseItems[armwear1EquipmentKey].name;
      }
    }

    if (armwear2EquipmentKey != Hero.armwear2Equipment) {
      armwear2EquipmentKey = Hero.armwear2Equipment;

      if (armwear2EquipmentKey == "") {
        armwear2Button.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Sprites.equipmentIcons["armwear2"];
        armwear2Button.transform.Find("Text").gameObject.GetComponent<Text>().text = "None";
      } else {
        armwear2Button.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Objects.pauseItems[armwear2EquipmentKey].thumbnail;
        armwear2Button.transform.Find("Text").gameObject.GetComponent<Text>().text = Objects.pauseItems[armwear2EquipmentKey].name;
      }
    }

    if (ring1EquipmentKey != Hero.ring1Equipment) {
      ring1EquipmentKey = Hero.ring1Equipment;

      if (ring1EquipmentKey == "") {
        ring1Button.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Sprites.equipmentIcons["ring1"];
        ring1Button.transform.Find("Text").gameObject.GetComponent<Text>().text = "None";
      } else {
        ring1Button.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Objects.pauseItems[ring1EquipmentKey].thumbnail;
        ring1Button.transform.Find("Text").gameObject.GetComponent<Text>().text = Objects.pauseItems[ring1EquipmentKey].name;
      }
    }

    if (ring2EquipmentKey != Hero.ring2Equipment) {
      ring2EquipmentKey = Hero.ring2Equipment;

      if (ring2EquipmentKey == "") {
        ring2Button.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Sprites.equipmentIcons["ring2"];
        ring2Button.transform.Find("Text").gameObject.GetComponent<Text>().text = "None";
      } else {
        ring2Button.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Objects.pauseItems[ring2EquipmentKey].thumbnail;
        ring2Button.transform.Find("Text").gameObject.GetComponent<Text>().text = Objects.pauseItems[ring2EquipmentKey].name;
      }
    }
  }

  void UpdateMagicResistances() {
    string currentMagicResistances = String.Join(",", heroScript.magicResistances);

    if (magicResistances != currentMagicResistances) {
      GameObject[] magicResistanceObjects = new GameObject[] {resistance1Object, resistance2Object, resistance3Object, resistance4Object, resistance5Object, resistance6Object, resistance7Object, resistance8Object};
      string[] heroMagicResistances = heroScript.magicResistances;

      if (heroMagicResistances.Length > 1) {
        magicEmptyObject.SetActive(false);
      } else {
        magicEmptyObject.SetActive(true);
      }

      int i = 0;
      foreach (GameObject currMagicResistanceObject in magicResistanceObjects) {
        if (i >= heroMagicResistances.Length) {
          currMagicResistanceObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        } else {
          currMagicResistanceObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
          currMagicResistanceObject.GetComponent<Image>().sprite = Sprites.magicResistances[heroMagicResistances[i]];
        }
        i++;
      }

      magicResistances = currentMagicResistances;
    }
  }

  void ShowGamePadOptions() {
    mainKeysPanel.SetActive(false);
    mainButtonPanel.SetActive(true);
  }

  void ShowKeyboardOptions() {
    mainButtonPanel.SetActive(false);
    mainKeysPanel.SetActive(true);
  }

  public void SetKeyboardAsPreferredInput() {
    Constants.preferredInput = "keyboard";
  }

  public void SetGamepadAsPreferredInput() {
    Constants.preferredInput = "gamepad";
  }

  public void AwaitInput(String key) {
    canvasStatus = "mapping";
    Helpers.FocusUIElement(null);

    switch(key) {
      case "jump":
        jumpAwaitLabel.SetActive(true);
        break;
      case "atk1":
        atk1AwaitLabel.SetActive(true);
        break;
      case "atk2":
        atk2AwaitLabel.SetActive(true);
        break;
      default:
        Debug.Log("Unknown await case: " + key);
        break;
    }

    currentlyMapping = key;
  }

  public void FinishMapping(String keyCode) {
    switch(currentlyMapping) {
      case "jump":
        if (Helpers.IsGamepadKey(keyCode)) {
          if (keyCode == Controls.currentGamepadAttack1) {
            Controls.currentGamepadAttack1 = Controls.currentGamepadJump;
          } else if (keyCode == Controls.currentGamepadAttack2) {
            Controls.currentGamepadAttack2 = Controls.currentGamepadJump;
          }

          Controls.currentGamepadJump = keyCode;
        } else {
          if (keyCode == Controls.currentKeyboardAttack1) {
            Controls.currentKeyboardAttack1 = Controls.currentKeyboardJump;
          } else if (keyCode == Controls.currentKeyboardAttack2) {
            Controls.currentKeyboardAttack2 = Controls.currentKeyboardJump;
          }

          Controls.currentKeyboardJump = keyCode;
        }

        jumpAwaitLabel.SetActive(false);
        Helpers.FocusUIElement(jumpButton);
        break;
      case "atk1":
        if (Helpers.IsGamepadKey(keyCode)) {
          if (keyCode == Controls.currentGamepadJump) {
            Controls.currentGamepadJump = Controls.currentGamepadAttack1;
          } else if (keyCode == Controls.currentGamepadAttack2) {
            Controls.currentGamepadAttack2 = Controls.currentGamepadAttack1;
          }

          Controls.currentGamepadAttack1 = keyCode;
        } else {
          if (keyCode == Controls.currentKeyboardJump) {
            Controls.currentKeyboardJump = Controls.currentKeyboardAttack1;
          } else if (keyCode == Controls.currentKeyboardAttack2) {
            Controls.currentKeyboardAttack2 = Controls.currentKeyboardAttack1;
          }

          Controls.currentKeyboardAttack1 = keyCode;
        }

        atk1AwaitLabel.SetActive(false);
        Helpers.FocusUIElement(atk1Button);
        break;
      case "atk2":
        if (Helpers.IsGamepadKey(keyCode)) {
          if (keyCode == Controls.currentGamepadJump) {
            Controls.currentGamepadJump = Controls.currentGamepadAttack2;
          } else if (keyCode == Controls.currentGamepadAttack1) {
            Controls.currentGamepadAttack1 = Controls.currentGamepadAttack2;
          }

          Controls.currentGamepadAttack2 = keyCode;
        } else {
          if (keyCode == Controls.currentKeyboardJump) {
            Controls.currentKeyboardJump = Controls.currentKeyboardAttack2;
          } else if (keyCode == Controls.currentKeyboardAttack1) {
            Controls.currentKeyboardAttack1 = Controls.currentKeyboardAttack2;
          }

          Controls.currentKeyboardAttack2 = keyCode;
        }

        atk2AwaitLabel.SetActive(false);
        Helpers.FocusUIElement(atk2Button);
        break;
      default:
        Debug.Log("unknown map case: " + currentlyMapping);
        break;
    }

    currentlyMapping = "";
    canvasStatus = "options_controls";
  }

  public void ProceedToReset() {
    canvasStatus = "options_controls_reset";
    resetRectangle.SetActive(true);
    Helpers.FocusUIElement(resetYes);
  }

  public void ResetMapping() {
    Controls.currentKeyboardJump = Controls.DEFAULT_KEYBOARD_JUMP;
    Controls.currentKeyboardAttack1 = Controls.DEFAULT_KEYBOARD_ATTACK_1;
    Controls.currentKeyboardAttack2 = Controls.DEFAULT_KEYBOARD_ATTACK_2;

    Controls.currentGamepadJump = Controls.DEFAULT_GAMEPAD_JUMP;
    Controls.currentGamepadAttack1 = Controls.DEFAULT_GAMEPAD_ATTACK_1;
    Controls.currentGamepadAttack2 = Controls.DEFAULT_GAMEPAD_ATTACK_2;

    canvasStatus = "options_controls";
    resetRectangle.SetActive(false);
    Helpers.FocusUIElement(resetButton);
  }

  public void CancelReset() {
    canvasStatus = "options_controls";
    resetRectangle.SetActive(false);
    Helpers.FocusUIElement(resetButton);
  }

  public void PopulateEquipmentContainer(string equipmentType) {
    ClearItems(equipmentContainer);

    switch(equipmentType) {
      case "body":
        currentEquipmentItems = Helpers.GetSpecificItemList(Constants.bodyEquipmentTypes, heroScript.items);
        PopulateItemsContainer(currentEquipmentItems, false, equipmentContainer);
      break;
      case "arms":
        currentEquipmentItems = Helpers.GetSpecificItemList(Constants.armEquipmentTypes, heroScript.items);
        PopulateItemsContainer(currentEquipmentItems, false, equipmentContainer);
      break;
      case "neck":
        currentEquipmentItems = Helpers.GetSpecificItemList(Constants.neckEquipmentTypes, heroScript.items);
        PopulateItemsContainer(currentEquipmentItems, false, equipmentContainer);
      break;
      case "armwear":
        currentEquipmentItems = Helpers.GetSpecificItemList(Constants.armwearEquipmentTypes, heroScript.items);
        PopulateItemsContainer(currentEquipmentItems, false, equipmentContainer);
      break;
      case "rings":
        currentEquipmentItems = Helpers.GetSpecificItemList(Constants.ringEquipmentTypes, heroScript.items);
        PopulateItemsContainer(currentEquipmentItems, false, equipmentContainer);
      break;
      default:
        Debug.Log("Unknown equipmentType: " + equipmentType);
      break;
    }
  }

  public void SelectEquipment(int selectedIndex) {
    canvasStatus = "equipment_select";
    currentlyEquippedIndex = selectedIndex;
    previouslySelectedEquipmentButton = eventSystem.currentSelectedGameObject;
    Helpers.FocusUIElement(itemButtons.ElementAt(0));
    SetEquipmentProspect(0);
  }
}
