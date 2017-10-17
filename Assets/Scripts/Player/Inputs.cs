using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public enum BufferInput {
	None,
	Jump,
	Dash,
	BasicAttack,
	SpecialAttack
}

public class Inputs : MonoBehaviour {

	public float BufferWindow;

	Animator _animator;
	Player _player;

	BufferInput _buffer = BufferInput.None;
	float _bufferTime;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
		_player = ReInput.players.GetPlayer (0);
		//Debug.Log (_buffer.ToString ());
	}
	
	// Update is called once per frame
	void Update () {

		if (_player.GetButtonDown("Jump")) {
			
		}

	}
}
