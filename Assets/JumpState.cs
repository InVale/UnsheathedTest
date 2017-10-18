using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : StateMachineBehaviour {

	//Tweak
	public GameObject JumpFX;
	public float GroundedJumpForce = 15;
	public float AirJumpForce = 15;

	//Mandatory
	Rigidbody _rgbd;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		_rgbd = animator.gameObject.GetComponent<Rigidbody> ();

		Instantiate (JumpFX, animator.transform.position + Vector3.down * 0.5f, Quaternion.identity);

		animator.SetBool ("JumpInput", false);
		if (animator.GetBool("Grounded")) {
			_rgbd.velocity = new Vector3(_rgbd.velocity.x, GroundedJumpForce, 0);
		}
		else {
			_rgbd.velocity = new Vector3(_rgbd.velocity.x, AirJumpForce, 0);
			animator.SetInteger ("AirJumpNumber", animator.GetInteger ("AirJumpNumber") - 1);
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

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
