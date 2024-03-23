using UnityEngine;

public class ArrowExplosion : MonoBehaviour {
  private InGame inGame;
  void Start() {
    inGame = GameObject.Find("InGame").gameObject.GetComponent<InGame>();
  }
  void Update() {}

  public void DestroyExplosion() {
    Destroy(gameObject);
  }

  public void PlayExplosionSound() {
    inGame.PlaySound(Sounds.explosionSounds["arrow"], transform.position);
  }
}
