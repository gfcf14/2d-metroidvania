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
    private new Light2D light;

    void Awake() {
      hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
      light = GetComponent<Light2D>();
    }

    void Update() {
      if (!hero.isPaused) {
        // gets elapsed time since start (to test hours, add values in multiples of 60 after GameData.initialGameTime)
        // TODO: add time from loaded time once implemented
        float elapsedTime = Time.time + GameData.initialGameTime;
        currentTime = Helpers.GetGameTime((int)(elapsedTime % Constants.maxDayLength));

        // gets the percentage based on the sine of the time elapsed
        float percentage = (elapsedTime % Constants.maxDayLength) / Constants.maxDayLength;

        // clamps the percentage to be between 0 and 1
        percentage = Mathf.Clamp01(percentage);
        currentGradientPercentage = percentage;

        light.color = gradient.Evaluate(percentage);
      }
    }
  }
}

