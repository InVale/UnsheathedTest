using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Control : MonoBehaviour {
			
	//MainStuff
	Player _player;
	Animator _animator;
	[HideInInspector]
	public Rigidbody _rgbd;
	public Transform GroundCheck;

	//Layer
	public LayerMask Ground;

	//State
	[HideInInspector]
	public bool _isGrounded = true;
	bool _canJump = true;
	int _canAirJump = 0;
	int _canDash;
	[HideInInspector]
	public bool _isDashing;
	float _dashDirection = 1;

	//WeirdStuff
	[HideInInspector]
	public float _bonusVelocity = 0;

	//Tweaking
	public float GroundSpeed = 5;
	public float Gravity = 9.81f;
	public float AirControl = 1;
	public float GroundedJumpMinForce = 6;
	public float GroundedJumpAddedForce = 12;
	public float GroundedJumpHoldTime = 1;
	public int AirJumpNumber = 1;
	public float AirJumpForce = 8;
	public float DashSpeed = 14;
	public int DashNumber = 1;
	public float DashCooldown = 0.5f;

	//SmallStuff
	float _jumpBuffer = 0;
	float _jumpTime = 0;
	float _basicAttackBuffer = 0;
	float _DashBuffer = 0;

	// Use this for initialization
	void Start () {
		_player = ReInput.players.GetPlayer (0);
		_animator = GetComponent<Animator> ();
		_rgbd = GetComponent<Rigidbody> ();

		_canAirJump = AirJumpNumber;
		_canDash = DashNumber;
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
				_canDash = DashNumber;
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
		if (!_isDashing) {
			if (_isGrounded) {
				_rgbd.velocity = new Vector3(_player.GetAxis ("Horizontal") * GroundSpeed + _bonusVelocity, _rgbd.velocity.y, 0);
			}
			else if (!_isGrounded) {
				float newAirSpeed = _rgbd.velocity.x + _player.GetAxis ("Horizontal") * AirControl * Time.deltaTime;
				if ((newAirSpeed > -GroundSpeed) && (newAirSpeed < GroundSpeed)) {
					_rgbd.velocity = new Vector3(newAirSpeed, _rgbd.velocity.y, 0);
				}
				else {
					int dir = (_rgbd.velocity.x > 0) ? 1 : -1;
					_rgbd.velocity = new Vector3(dir * GroundSpeed, _rgbd.velocity.y, 0);
				}
			}
		}
		else {
			_rgbd.velocity = Vector3.right * DashSpeed * _dashDirection;
		}

		//TURN AROUND
		if (!_animator.GetBool("Attacking")) {
			if ((_player.GetAxis ("Horizontal") <= -0.2f) || (_player.GetAxis ("Horizontal") >= 0.2f)) {
				if (_player.GetAxis ("Horizontal") * transform.right.x < 0) {
					if (transform.right.x > 0) {
						_animator.SetBool("GoLeft", true);
					}
					else {
						_animator.SetBool ("GoRight", true);
					}
				}
			}
		}
		else {
			_animator.SetBool("GoRight", false);
			_animator.SetBool ("GoLeft", false);
		}

		//JUMP
		if (((_player.GetButtonDown ("Jump")) || ((_jumpBuffer > 0) && _isGrounded)) && !_animator.GetBool ("Attacking")) {
			if (_canJump) {
				_jumpBuffer = 0;
				_rgbd.velocity = new Vector3 (_rgbd.velocity.x, GroundedJumpMinForce, 0);
				_canJump = false;
				_jumpTime = GroundedJumpHoldTime;
				_animator.SetTrigger("Jumping");
			}
			else if (!_canJump) {
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
		if (!_isDashing) {
			if (_player.GetButtonDown ("BasicAttack")) {
				_animator.SetBool ("Attacking", true);
				_animator.SetTrigger("BasicAttack");
			}
			else if (_player.GetButtonDown ("SpecialAttack")) {
				_animator.SetBool ("Attacking", true);
				_animator.SetTrigger ("SpecialAttack");
			}
		}

		//DASH
		if (_canDash > 0) {
			if (_player.GetButtonDown("Dash_Left")) {
				_canDash --;
				_DashBuffer = DashCooldown;
				_dashDirection = -1;
				_animator.SetTrigger ("Dash");
			}
			else if (_player.GetButtonDown("Dash_Right")) {
				_canDash --;
				_DashBuffer = DashCooldown;
				_dashDirection = 1;
				_animator.SetTrigger ("Dash");
			}
		}
		else {
			if (_DashBuffer > 0) {
				_DashBuffer -= Time.deltaTime;
			}
			else if (_isGrounded) {
				_canDash = DashNumber;
			}
		}
	}
}
