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

  IEnumerator ChangeTextOverTime(Text textComponent, string fullText, float duration) {
    float elapsedTime = 0f;
    int textLength = fullText.Length;
    string displayedText = "";

    while (elapsedTime < duration && displayedText.Length < textLength) {
      float progress = elapsedTime / duration;
      int charactersToShow = Mathf.FloorToInt(progress * textLength);
      displayedText = GetPartialText(fullText, charactersToShow, actionTextRect.sizeDelta.x);

      textComponent.text = displayedText;
      elapsedTime += Time.deltaTime;

      yield return null;
    }

    textComponent.text = fullText; // Ensure the full text is displayed at the end
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

  string GetPartialText(string fullText, int charactersToShow, float maxWidth) {
    int accumulatedWidth = 0;
    int truncateIndex = Mathf.Min(charactersToShow, fullText.Length);

    for (int i = 0; i < truncateIndex; i++) {
      accumulatedWidth += Helpers.GetCharacterDisplayWidth(fullText[i]);
      if (accumulatedWidth > maxWidth) {
        truncateIndex = i;
        break;
      }
    }

    return fullText.Substring(0, truncateIndex);
  }
}
