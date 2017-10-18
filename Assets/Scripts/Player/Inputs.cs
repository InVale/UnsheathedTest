using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public enum BufferInput {
	None,
	JumpInput,
	DashInput,
	BasicAttackInput,
	SpecialAttackInput
}

public class Inputs : MonoBehaviour {

	//Tweak
	public float BufferWindow = 0.05f;

	//Mandatory
	Animator _animator;
	Player _player;

	//Stuff
	BufferInput _buffer = BufferInput.None;
	float _bufferTime;

	void Start () {
		_animator = GetComponent<Animator> ();
		_player = ReInput.players.GetPlayer (0);
	}

	void Update () {
		BufferTick ();
		InputRead ();
		StickRead ();
	}

	void BufferTick() {
		if (_bufferTime > 0) {
			_bufferTime -= Time.deltaTime;
			if (_bufferTime <= 0){
				_animator.SetBool (_buffer.ToString (), false);
				_buffer = BufferInput.None;
			}
		}
	}

	void InputRead() {
		if (_player.GetButtonDown("Jump")) {
			_buffer = BufferInput.JumpInput;
			_bufferTime = BufferWindow;
			_animator.SetBool (_buffer.ToString (), true);
		}
		else if (_player.GetButtonDown("Dash")) {
			_buffer = BufferInput.DashInput;
			_bufferTime = BufferWindow;
			_animator.SetBool (_buffer.ToString (), true);
		}
		else if (_player.GetButtonDown("BasicAttack")) {
			_buffer = BufferInput.BasicAttackInput;
			_bufferTime = BufferWindow;
			_animator.SetBool (_buffer.ToString (), true);
		}
		else if (_player.GetButtonDown("SpecialAttack")) {
			_buffer = BufferInput.SpecialAttackInput;
			_bufferTime = BufferWindow;
			_animator.SetBool (_buffer.ToString (), true);
		}
	}

	void StickRead() {
		_animator.SetFloat("Joystick X", _player.GetAxis("Horizontal"));
	}
}
