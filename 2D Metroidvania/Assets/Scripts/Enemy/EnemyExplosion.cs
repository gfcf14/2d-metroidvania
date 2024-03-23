using UnityEngine;

public class EnemyExplosion : MonoBehaviour {
  private InGame inGame;

  void Start() {
    inGame = GameObject.Find("InGame").gameObject.GetComponent<InGame>();
  }
  void Update() {}

  public void PlayExplosion() {
    inGame.PlaySound(Sounds.explosionSounds["enemy"], transform.position);
  }

  public void DestroyExplosion() {
    Destroy(gameObject);
  }
}
