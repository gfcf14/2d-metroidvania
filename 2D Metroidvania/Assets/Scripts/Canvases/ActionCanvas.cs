using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCanvas : MonoBehaviour {
  [SerializeField] GameObject buttonImage;
  [SerializeField] public string text = "";
  [SerializeField] public string icon = "";
  private string currentPreferredInput = "";
  void Start() {
    SetIcon();
  }

  void Update() {
    if (currentPreferredInput != Constants.preferredInput ||
      (currentPreferredInput == Constants.preferredInput && (
          (currentPreferredInput == "keyboard" && icon != Controls.currentKeyboardAction) ||
          (currentPreferredInput == "gamepad" && icon != Controls.currentGamepadAction)
        )
      )) {
        SetIcon();
    }
  }

  public void SetContent(string action) {
    text = action.ToUpper();
  }

  public void SetIcon() {
    currentPreferredInput = Constants.preferredInput;
    icon = currentPreferredInput == "keyboard" ? Controls.currentKeyboardAction : Controls.currentGamepadAction;

    buttonImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[icon];
  }
}
