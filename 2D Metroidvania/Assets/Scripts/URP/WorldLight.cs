using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


namespace WorldTime {
  [RequireComponent(typeof(Light2D))]
  public class WorldLight : MonoBehaviour {
    [SerializeField] Gradient gradient;

    // TODO: remove these test variables once no longer used
    [SerializeField] string currentTime;
    [SerializeField] float currentGradientPercentage;

    [System.NonSerialized] public Hero hero;
    private Light2D light;
    // private float startTime;

    void Awake() {
      hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
      light = GetComponent<Light2D>();
      startTime = Time.time;
    }

    void Update() {
      if (!hero.isPaused) {
        // gets elapsed time since start
        // TODO: add time from loaded time once implemented
        float elapsedTime = Time.time + GameData.initialGameTime;
        // float elapsedTime = Time.time - startTime;
        currentTime = Helpers.GetGameTime((int)(elapsedTime % Constants.maxDayLength));

        // gets the percentage based on the sine of the time elapsed
        float percentage = (elapsedTime % Constants.maxDayLength) / Constants.maxDayLength;
        // float percentage = Mathf.Sin(elapsedTime / 30 * Mathf.PI * 2) * 0.5f + 0.5f;

        // clamps the percentage to be between 0 and 1
        percentage = Mathf.Clamp01(percentage);
        currentGradientPercentage = percentage;

        light.color = gradient.Evaluate(percentage);
      }
    }
  }
}

