using UnityEngine;

public class ArrowBurn : MonoBehaviour {
  private Animator anim;
  private SpriteRenderer spriteRenderer;

  [System.NonSerialized] public float startTime;
  [System.NonSerialized] float maxBurnDuration = 3000f;
  [System.NonSerialized] float arrowBurnX = 0;
  [System.NonSerialized] float arrowBurnY = 0;
  [System.NonSerialized] public bool isFinished;
  [SerializeField] public Vector2 burnDimensions;

  private AudioSource audioSource;

  void Start() {
    spriteRenderer = GetComponent<SpriteRenderer>();
    arrowBurnX = Mathf.Max(burnDimensions.x, burnDimensions.y) * 1.2f;
    arrowBurnY = arrowBurnX * 1.05f;
    audioSource = GetComponent<AudioSource>();
    audioSource.loop = true;
  }

  void Update() {
    anim = GetComponent<Animator>();

    if (Helpers.ExceedsTime(startTime, maxBurnDuration)) {
      isFinished = true;
    }

    anim.SetBool("isFinished", isFinished);
  }

  void LateUpdate() {
    spriteRenderer.size = new Vector2(arrowBurnX, arrowBurnY);
  }

  public void DestroySmoke() {
    Destroy(gameObject);
  }

  public void PlayBurnSound() {
    if (!audioSource.isPlaying) {
      audioSource.clip = Sounds.loops["arrow-burn"];
      audioSource.Play();
    }
  }
}
