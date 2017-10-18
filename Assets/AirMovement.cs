using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMovement : StateMachineBehaviour {

	//Tweak
	public float Gravity = 25;
	public float AirControl = 100;
	public float AirCap = 10;
	public int AirJumpNumber = 1;
	public int AirDashNumber = 1;

	//Mandatory
	Rigidbody _rgbd;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		_rgbd = animator.gameObject.GetComponent<Rigidbody> ();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		_rgbd.velocity += Vector3.down * Gravity * Time.deltaTime;

		float newAirSpeed = _rgbd.velocity.x + animator.GetFloat("Joystick X") * AirControl * Time.deltaTime;
		if ((newAirSpeed > -AirCap) && (newAirSpeed < AirCap)) {
			_rgbd.velocity = new Vector3(newAirSpeed, _rgbd.velocity.y, 0);
		}
		else {
			int dir = (_rgbd.velocity.x > 0) ? 1 : -1;
			_rgbd.velocity = new Vector3(dir * AirCap, _rgbd.velocity.y, 0);
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (animator.GetBool ("Landing")) {
			animator.SetInteger ("AirJumpNumber", AirJumpNumber);
			animator.SetInteger ("AirDashNumber", AirDashNumber);
		}
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
