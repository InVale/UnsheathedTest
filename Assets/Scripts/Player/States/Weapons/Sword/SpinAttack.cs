using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : StateMachineBehaviour {

	Control	_control;

	public float AddedSpeed = 5;
	public float Jump = 3;

	bool _wasSpeedAdded = false;
	float _bufferSpeed;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		_control = animator.gameObject.GetComponent<Control> ();

		if (_control._isGrounded) {
			_wasSpeedAdded = true;
			_control._bonusVelocity = AddedSpeed * animator.transform.right.x;
			_bufferSpeed = _control.GroundSpeed;
			_control.GroundSpeed = 0;
		}
		else {
			if (true) {
				_control._rgbd.velocity = new Vector3 (_control._rgbd.velocity.x, Jump, 0);
				animator.SetTrigger ("Jumping");
			}
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (_wasSpeedAdded) {
			_wasSpeedAdded = false;
			_control._bonusVelocity = 0;
			_control.GroundSpeed = _bufferSpeed;
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
