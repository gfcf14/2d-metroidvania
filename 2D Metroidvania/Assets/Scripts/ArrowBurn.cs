using UnityEngine;

public class ArrowBurn : MonoBehaviour {
  private Animator anim;
  private SpriteRenderer spriteRenderer;

  [System.NonSerialized] public float startTime;
  [System.NonSerialized] float maxBurnDuration = 3000f;
  [System.NonSerialized] public bool isFinished;
  [SerializeField] public Vector2 burnDimensions;

  void Start() {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void Update() {
    anim = GetComponent<Animator>();

    if (Helpers.ExceedsTime(startTime, maxBurnDuration)) {
      isFinished = true;
    }

    anim.SetBool("isFinished", isFinished);
  }

  void LateUpdate() {
    spriteRenderer.size = new Vector2(burnDimensions.x * 1.5f, burnDimensions.y * 1.25f);
  }

  public void DestroySmoke() {
    Destroy(gameObject);
  }
}
