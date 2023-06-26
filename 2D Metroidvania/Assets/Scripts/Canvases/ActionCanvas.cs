using UnityEngine;
using UnityEngine.UI;

public class ActionCanvas : MonoBehaviour {
  [SerializeField] GameObject buttonImage;
  [SerializeField] GameObject actionText;
  [SerializeField] GameObject actionTextContainer;
  [SerializeField] public string text = "";
  [SerializeField] public string icon = "";
  private string currentPreferredInput = "";

  private int textWidth = 0;
  private int textContainerWidth = 0;

  private int maxTextWidth = 0;
  private int maxTextContainerWidth = 0;
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

    if (textWidth < maxTextWidth) {
      textWidth += 2;
      if (textWidth > maxTextWidth) {
        textWidth = maxTextWidth;
      }

      SetObjectWidth(actionText, textWidth, Constants.actionTextHeight);
      SetText(text, textWidth);
    }

    if (textContainerWidth < maxTextContainerWidth) {
      textContainerWidth += 2;
      if (textContainerWidth > maxTextContainerWidth) {
        textContainerWidth = maxTextContainerWidth;
      }

      SetObjectWidth(actionTextContainer, textContainerWidth, Constants.actionTextContainerHeight);
    }
  }

  public void SetSpecs(string action) {
    text = action.ToUpper();
    textWidth = 0;
    textContainerWidth = 0;
    maxTextWidth = text.Length * Constants.alphaCharacterWidth;
    maxTextContainerWidth = maxTextWidth + Constants.defaultActionTextContainerWidth;
  }

  public void SetIcon() {
    currentPreferredInput = Constants.preferredInput;
    icon = currentPreferredInput == "keyboard" ? Controls.currentKeyboardAction : Controls.currentGamepadAction;

    buttonImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[icon];
  }

  public void SetObjectWidth(GameObject UIElement, int width, int height) {
    UIElement.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
  }

  // sets only as much text as the available width allows
  public void SetText(string text, int width) {
    int accumulatedWidth = 0;
    int truncateIndex = text.Length;

    for (int i = 0; i < text.Length; i++) {
      accumulatedWidth += Constants.alphaCharacterWidth;
      if (accumulatedWidth > width) {
        truncateIndex = i;
        break;
      }
    }

    actionText.GetComponent<Text>().text = text.Substring(0, truncateIndex);
  }
}
