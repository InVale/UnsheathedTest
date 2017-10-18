using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : StateMachineBehaviour {

	GameObject _main;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		_main = animator.gameObject;

		_main.transform.localRotation = Quaternion.Euler (_main.transform.localRotation.eulerAngles.x, 
			_main.transform.localRotation.eulerAngles.y + 180, _main.transform.localRotation.eulerAngles.z);
		animator.SetBool ("FacingRight", !animator.GetBool ("FacingRight"));
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (animator.GetBool("CanTurn")) {
			if ((!animator.GetBool("FacingRight")) && (animator.GetFloat("Joystick X") > 0.2)) {
				animator.Play ("Right Turn Around", 0, (1 - stateInfo.normalizedTime));
			}
			else if ((animator.GetBool("FacingRight")) && (animator.GetFloat("Joystick X") < -0.2)) {
				animator.Play ("Left Turn Around", 0, (1 - stateInfo.normalizedTime));
			}
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
