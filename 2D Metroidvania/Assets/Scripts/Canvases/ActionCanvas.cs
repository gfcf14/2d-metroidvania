using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActionCanvas : MonoBehaviour {
  [SerializeField] GameObject buttonImage;
  [SerializeField] GameObject actionText;
  [SerializeField] GameObject actionTextContainer;
  [SerializeField] public string text = "";
  [SerializeField] public string icon = "";

  private RectTransform actionTextRect;
  private RectTransform actionTextContainerRect;
  private Text textComponent;

  private string currentPreferredInput = "";
  void Start() {
    SetComponents();
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

  IEnumerator ChangeWidthOverTime(RectTransform rectTransform, int targetWidth, float duration) {
    float elapsedTime = 0f;
    float initialWidth = rectTransform.sizeDelta.x;

    while (elapsedTime < duration) {
      elapsedTime += Time.deltaTime;
      float t = Mathf.Clamp01(elapsedTime / duration);
      float newWidth = Mathf.Lerp(initialWidth, targetWidth, t);
      rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
      yield return null;
    }

    // Ensure final width is set precisely
    rectTransform.sizeDelta = new Vector2(targetWidth, rectTransform.sizeDelta.y);
  }

  public void ChangeTextContainerWidthOverTime(RectTransform rectTransform, int targetWidth, float duration) {
    StartCoroutine(ChangeWidthOverTime(rectTransform, targetWidth, duration));
  }

  IEnumerator ChangeTextOverTime(Text textComponent, string text, float duration) {
    float elapsedTime = 0f;

    while (elapsedTime < duration) {
      elapsedTime += Time.deltaTime;

      float currentTextWidth = actionTextRect.sizeDelta.x;
      SetText(textComponent, text, (int)currentTextWidth);
      yield return null;
    }

    textComponent.text = text;
  }

  public void SetActionText(Text textComponent, string text, float duration) {
    StartCoroutine(ChangeTextOverTime(textComponent, text, duration));
  }

  public void SetSpecs(string action) {
    if (!actionTextRect || !actionTextContainerRect || !textComponent) {
      SetComponents();
    }

    text = action.ToUpper();
    int textWidth = Helpers.GetTextDisplayWidth(text);
    int textContainerWidth = textWidth + Constants.defaultActionTextContainerWidth;

    ChangeTextContainerWidthOverTime(actionTextRect, textWidth, 0.3f);
    SetActionText(textComponent, text, 0.3f);
    ChangeTextContainerWidthOverTime(actionTextContainerRect, textContainerWidth, 0.3f);
  }

  public void ClearSpecs() {
    text = "";
    actionTextRect.sizeDelta = new Vector2(0, actionTextRect.sizeDelta.y);
    actionTextContainerRect.sizeDelta = new Vector2(0, actionTextContainerRect.sizeDelta.y);
    textComponent.text = "";
  }

  public void SetComponents() {
    actionTextRect = actionText.GetComponent<RectTransform>();
    actionTextContainerRect = actionTextContainer.GetComponent<RectTransform>();
    textComponent = actionText.GetComponent<Text>();
  }

  public void SetIcon() {
    currentPreferredInput = Constants.preferredInput;
    icon = currentPreferredInput == "keyboard" ? Controls.currentKeyboardAction : Controls.currentGamepadAction;

    buttonImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[icon];
  }


  // sets only as much text as the available width allows
  public void SetText(Text textComponent, string text, int width) {
    int accumulatedWidth = 0;
    int truncateIndex = text.Length;

    for (int i = 0; i < text.Length; i++) {
      accumulatedWidth += Helpers.GetCharacterDisplayWidth(text[i]);
      if (accumulatedWidth > width) {
        truncateIndex = i;
        break;
      }
    }

    textComponent.text = text.Substring(0, truncateIndex);
  }
}
