using TMPro;
using UnityEngine;

public class DamageContainer : MonoBehaviour {
  [System.NonSerialized] public int damage;
  void Start() {
    GetComponent<Animator>().enabled = true;
    transform.Find("DamageText").gameObject.GetComponent<TextMeshPro>().text = damage.ToString();
  }

  void Update() {}

  public void DestroyDamageContainer() {
    Destroy(gameObject);
  }
}
