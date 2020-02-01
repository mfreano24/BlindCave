using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVars : MonoBehaviour {

	public List<Vector2> p1ResetPos;

	public List<Vector2> p2ResetPos;

	public int p1Death = 0;
	public int p2Death = 0;

	public int level = 0;

	private void Start() {
		p1ResetPos = new List<Vector2>() {
			new Vector2(-8, 11)
		};

		p2ResetPos = new List<Vector2>() {
			Vector2.zero
		};
	}

}
