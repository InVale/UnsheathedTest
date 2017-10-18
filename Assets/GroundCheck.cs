using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : StateMachineBehaviour {

	public LayerMask Ground;
	Transform _myCheck;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		_myCheck = animator.transform.GetChild(0).GetChild(0).GetChild(0);
	}

	// OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (!animator.GetBool ("Grounded")) {
			animator.SetBool ("Grounded", Physics.CheckBox (_myCheck.position, _myCheck.lossyScale, Quaternion.identity, Ground));
			if (animator.GetBool ("Grounded")) {
				animator.SetBool ("Landing", true);
			}
		}
		else {
			animator.SetBool ("Grounded", Physics.CheckBox (_myCheck.position, _myCheck.lossyScale, Quaternion.identity, Ground));
			if (!animator.GetBool ("Grounded")) {
				animator.SetBool ("Landing", false);
			}
		}


		if (animator.GetFloat("DashCooldown") >= 0) {
			animator.SetFloat ("DashCooldown", animator.GetFloat ("DashCooldown") - Time.deltaTime);
		}
	}

	// OnStateExit is called before OnStateExit is called on any state inside this state machine
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called before OnStateMove is called on any state inside this state machine
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called before OnStateIK is called on any state inside this state machine
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMachineEnter is called when entering a statemachine via its Entry Node
	//override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
	//
	//}

	// OnStateMachineExit is called when exiting a statemachine via its Exit Node
	//override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
	//
	//}
}
