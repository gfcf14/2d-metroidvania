using UnityEngine;

public class ChampionStateBehavior : StateMachineBehaviour {
  public override void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex) {
    if (stateInfo.IsName("defense")) {
      anim.ResetTrigger("isDefending");
    }
  }
}
