using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Control : MonoBehaviour {

	public enum Direction {
		Left = -1,
		Right = 1
	}

	Player _player;

	Direction _direction = Direction.Right;

	// Use this for initialization
	void Start () {
		_player = ReInput.players.GetPlayer (0);
	}
	
	// Update is called once per frame
	void Update () {

		if ((_player.GetAxis ("Horizontal") <= -0.2f) || (_player.GetAxis ("Horizontal") >= 0.2f)) {
			if (_player.GetAxis ("Horizontal") * (int)_direction > 0) {
				
			}
		}

	}
}
