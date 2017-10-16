using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Control : MonoBehaviour {

	public enum Direction {
		Left = -1,
		Right = 1
	}
			
	//MainStuff
	Player _player;
	Animator _animator;
	Rigidbody _rgbd;
	public Transform GroundCheck;

	//Layer
	public LayerMask Ground;

	//State
	Direction _direction = Direction.Right;
	bool _isGrounded = true;
	bool _canJump = true;
	int _canAirJump = 0;

	//WeirdStuff
	//[HideInInspector]

	//Tweaking
	public float GroundSpeed = 5;
	public float Gravity = 9.81f;
	public float AirControl = 1;
	public float GroundedJumpMinForce = 6;
	public float GroundedJumpAddedForce = 12;
	public float GroundedJumpHoldTime = 1;
	public int AirJumpNumber = 1;
	public float AirJumpForce = 8;

	//SmallStuff
	float _jumpBuffer = 0;
	float _jumpTime = 0;
	float _basicAttackBuffer;

	// Use this for initialization
	void Start () {
		_player = ReInput.players.GetPlayer ("SYSTEM");
		_animator = GetComponent<Animator> ();
		_rgbd = GetComponent<Rigidbody> ();

		_canAirJump = AirJumpNumber;
	}

	// Update is called once per frame
	void Update () {

		//GROUND STATE
		if (!_isGrounded) {
			_isGrounded = Physics.CheckSphere (GroundCheck.position, 0.1f, Ground);
			_canJump = false;

			if (_isGrounded) {
				_canJump = true;
				_canAirJump = AirJumpNumber;
				_animator.SetTrigger ("Landing");
				_rgbd.velocity = new Vector3 (_rgbd.velocity.x, 0, 0);
			}
			else {
				_rgbd.velocity += Vector3.down * Gravity * Time.deltaTime;
			}
		}
		else {
			_isGrounded = Physics.CheckSphere (GroundCheck.position, 0.1f, Ground);
		}

		//MOVEMENT
		if (_isGrounded) {
			_rgbd.velocity = new Vector3(_player.GetAxis ("Horizontal") * GroundSpeed, _rgbd.velocity.y, 0);
		}
		else if (!_isGrounded) {
			float newAirSpeed = _rgbd.velocity.x + _player.GetAxis ("Horizontal") * AirControl * Time.deltaTime;
			if ((newAirSpeed > -GroundSpeed) && (newAirSpeed < GroundSpeed)) {
				_rgbd.velocity = new Vector3(newAirSpeed, _rgbd.velocity.y, 0);
			}
		}

		//TURN AROUND
		if ((_player.GetAxis ("Horizontal") <= -0.2f) || (_player.GetAxis ("Horizontal") >= 0.2f)) {

			if ((_player.GetAxis ("Horizontal") * (int)_direction) < 0) {
				if (_direction == Direction.Left) {
					_direction = Direction.Right;
					_animator.SetBool("GoRight", true);
				}
				else {
					_direction = Direction.Left;
					_animator.SetBool ("GoLeft", true);
				}
			}
		}

		//JUMP
		if ((_player.GetButtonDown ("Jump")) || ((_jumpBuffer > 0) && _isGrounded)) {
			if (_canJump) {
				_jumpBuffer = 0;
				_rgbd.velocity = new Vector3 (_rgbd.velocity.x, GroundedJumpMinForce, 0);
				_canJump = false;
				_jumpTime = GroundedJumpHoldTime;
				_animator.SetTrigger("Jumping");
			}
			else {
				if (_canAirJump > 0) {
					_rgbd.velocity = new Vector3 (_player.GetAxis ("Horizontal") * GroundSpeed, AirJumpForce, 0);
					_canAirJump--;
					_animator.SetTrigger ("Jumping");
				}
				else {
					_jumpBuffer = 0.1f;
				}
			}
		}
		else if (_jumpBuffer > 0) {
			_jumpBuffer -= Time.deltaTime;
		}
		else if ((_player.GetButton("Jump")) && (_jumpTime > 0)) {
			if (_jumpTime <= Time.deltaTime) {
				_rgbd.velocity += Vector3.up * GroundedJumpAddedForce * _jumpTime;
				_jumpTime = 0;
			}
			else {
				_rgbd.velocity += Vector3.up * GroundedJumpAddedForce * Time.deltaTime;
				_jumpTime -= Time.deltaTime;
			}
		}

		//ATTACK
		if (_player.GetButtonDown ("BasicAttack")) {
			_animator.SetTrigger("BasicAttack");
		}
		else if (_player.GetButtonDown ("SpecialAttack")) {
			_animator.SetTrigger ("SpecialAttack");
		}
	}
}
