using UnityEngine;

public class SkeletonKingStateBehavior : StateMachineBehaviour {
  public override void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex) {
    if (stateInfo.IsName("defense")) {
      anim.ResetTrigger("isDefending");
    }
  }
}
