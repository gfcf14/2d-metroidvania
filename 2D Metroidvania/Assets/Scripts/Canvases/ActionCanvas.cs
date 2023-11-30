using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActionCanvas : MonoBehaviour {
  [SerializeField] GameObject buttonImage;
  [SerializeField] GameObject actionText;
  [SerializeField] GameObject actionTextContainer;
  [SerializeField] public string text = "";
  [SerializeField] public string icon = "";

  private Animator anim;
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

  public void SetAction(string action) {
    if (!actionTextRect || !actionTextContainerRect || !textComponent) {
      SetComponents();
    }

    textComponent.text = action.ToUpper();
    anim.Play(action);
  }

  public void ClearAction() {
    textComponent.text = "";
    anim.Play("idle");
  }

  public void SetComponents() {
    anim = GetComponent<Animator>();
    actionTextRect = actionText.GetComponent<RectTransform>();
    actionTextContainerRect = actionTextContainer.GetComponent<RectTransform>();
    textComponent = actionText.GetComponent<Text>();
  }

  public void SetIcon() {
    currentPreferredInput = Constants.preferredInput;
    icon = currentPreferredInput == "keyboard" ? Controls.currentKeyboardAction : Controls.currentGamepadAction;

    buttonImage.GetComponent<Image>().sprite = Sprites.keycodeSprites[icon];
  }
}
