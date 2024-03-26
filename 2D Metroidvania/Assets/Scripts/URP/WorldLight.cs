using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


namespace WorldTime {
  [RequireComponent(typeof(Light2D))]
  public class WorldLight : MonoBehaviour {
    [SerializeField] Gradient gradient;

    // TODO: duration might not even be needed. Try to plug in Constants.maxDayLength in its place
    public float duration;
    private Light2D light;
    private float startTime;

    void Awake() {
      light = GetComponent<Light2D>();
      startTime = Time.time;
    }

    void Update() {
      // TODO: perform only if not paused

      // gets elapsed time since start
      // TODO: add time from loaded time once implemented
      float elapsedTime = Time.time - startTime;

      // gets the percentage based on the sine of the time elapsed
      float percentage = Mathf.Sin(elapsedTime / duration * Mathf.PI * 2) * 0.5f + 0.5f;

      // clamps the percentage to be between 0 and 1
      percentage = Mathf.Clamp01(percentage);

      light.color = gradient.Evaluate(percentage);
    }
  }
}

