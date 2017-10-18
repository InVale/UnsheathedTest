using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : StateMachineBehaviour {

	//Tweak
	public float DashSpeed = 14;
	public float DashCooldown = 0.25f;

	//Mandatory
	Rigidbody _rgbd;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		_rgbd = animator.gameObject.GetComponent<Rigidbody> ();

		animator.SetBool ("DashInput", false);

		animator.SetBool ("CanMove", false);
		animator.SetBool ("CanTurn", false);
		if (animator.GetBool ("Cancel")) {
			animator.SetBool ("Cancel", false);
		}
		else {
			animator.SetBool ("Cancel", true);
			animator.SetFloat ("DashCooldown", DashCooldown);
			if (!animator.GetBool("Grounded")) {
				animator.SetInteger ("AirDashNumber", animator.GetInteger ("AirDashNumber") - 1);
				animator.SetInteger ("AirJumpNumber", animator.GetInteger ("AirJumpNumber") + 1);
			}
		}

		int dir = (animator.transform.right.x * animator.GetFloat("Joystick X") >= 0) ? 1 : -1;
		_rgbd.velocity = animator.transform.right * DashSpeed * dir;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		animator.SetBool ("CanMove", true);
		animator.SetBool ("CanTurn", true);
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
