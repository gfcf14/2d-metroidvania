using UnityEngine;

public class Defense : MonoBehaviour {
  [System.NonSerialized] public bool isFacingLeft;

  private InGame inGame;

  void Start() {
    transform.localScale = new Vector3(isFacingLeft ? -1 : 1, 1, 1);
    inGame = GameObject.Find("InGame").gameObject.GetComponent<InGame>();
  }
  void Update() {}

  public void DestroyDefense() {
    Destroy(gameObject);
  }

  public void PlayBlockSound() {
    inGame.PlaySound(Sounds.blockSounds["basic"], transform.position);
  }
}
