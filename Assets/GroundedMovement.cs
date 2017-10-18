using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedMovement : StateMachineBehaviour {

	//Tweak
	public float GroundSpeed = 10;

	//Mandatory
	Rigidbody _rgbd;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		_rgbd = animator.gameObject.GetComponent<Rigidbody> ();
		animator.SetBool ("CanSpecialAttack", true);
		animator.SetBool ("Landing", false);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		_rgbd.velocity = new Vector3(animator.GetFloat("Joystick X") * GroundSpeed, _rgbd.velocity.y, 0);
		animator.SetFloat ("GroundSpeed", Mathf.Abs (animator.GetFloat ("Joystick X") * GroundSpeed));
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
