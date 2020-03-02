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
		player1 = null;
		player2 = null;
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

		//buttoncheck
		//PRESS K FOR DEBUG MODE ADVANCE
		if(Input.GetKeyDown(KeyCode.K) || ((player1 !=null && player2 !=null) && 
		  (player1.GetComponent<PlayerMovement>().onButton && player2.GetComponent<PlayerMovement>().onButton))){
			level++;
			Debug.Log("Level " + level);
			player1.GetComponent<PlayerMovement>().advanceLevel();
			player2.GetComponent<PlayerMovement>().advanceLevel();
		}
	}


	public void setP1(GameObject p){
		player1 = p;
		Debug.Log("P1 Assigned!");
	}
	public void setP2(GameObject p){
		player2 = p;
		Debug.Log("P2 Assigned!");
	}


}
