using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVars : MonoBehaviour {

	public List<Vector2> p1ResetPos;

	public List<Vector2> p2ResetPos;

	public List<Vector2> p1camPos;
	public List<Vector2> p2camPos;

	public int p1Death = 0;
	public int p2Death = 0;

	public int level = 0;

	public GameObject player1;
	public GameObject player2;

	private void Start() {
		p1ResetPos = new List<Vector2>(){
			new Vector2(-11.2f, 11), //1
			new Vector2(12.71f, 7.74f) //2
		};
		
		p2ResetPos = new List<Vector2>() {
			new Vector2(-12.26f, -8.33f), //1
			new Vector2(12.71f, -8.33f) //2
		};

		p1camPos = new List<Vector2>(){
			new Vector2(-5.65f, 12),
			new Vector2(19.5f, 12)
		};

		p2camPos = new List<Vector2>(){
			new Vector2(-5.65f, -6.85f),
			new Vector2(19.5f, -6.85f)
		};

	}

	private void Update() {
		if(player1 == null){
			player1 = GameObject.Find("P1");
			Debug.Log("P1 Assigned!");
		}
		if(player2 == null){
			player2 = GameObject.Find("P2");
			Debug.Log("P2 Assigned!");
		}

		//buttoncheck
		//PRESS K FOR DEBUG MODE ADVANCE
		if(/*(player1.GetComponent<PlayerMovement>().onButton && player2.GetComponent<PlayerMovement>().onButton) ||*/ Input.GetKeyDown(KeyCode.K)){
			level++;
			Debug.Log("Level " + level);
			player1.GetComponent<PlayerMovement>().advanceLevel();
			player2.GetComponent<PlayerMovement>().advanceLevel();
		}
	}

}
