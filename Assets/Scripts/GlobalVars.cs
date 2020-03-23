using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GlobalVars : MonoBehaviourPunCallbacks, IPunObservable {

	#region IPunObservable Implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        
        if(stream.IsWriting){
            stream.SendNext(level);
			stream.SendNext(p1Death);
			stream.SendNext(p2Death);
			stream.SendNext(player1);
			stream.SendNext(player2);
        }
        else{
			level = (int)stream.ReceiveNext();
			p1Death = (int)stream.ReceiveNext();
			p2Death = (int)stream.ReceiveNext();
			player1 = (GameObject)stream.ReceiveNext();
			player2 = (GameObject)stream.ReceiveNext();
			
        }
    }
    #endregion

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
			new Vector2(-12.26f, -29.59f), //1
			new Vector2(12.71f, -8.33f) //2
		};

		p1camPos = new List<Vector2>(){
			new Vector2(-5.65f, 12),
			new Vector2(19.5f, 12)
		};

		p2camPos = new List<Vector2>(){
			new Vector2(-5.65f, -28.4f),
			new Vector2(19.5f, -6.85f)
		};

	}

	private void Update() {

		//buttoncheck
		//PRESS K FOR DEBUG MODE ADVANCE
		if(Input.GetKeyDown(KeyCode.K) || ((player1 !=null && player2 !=null) && 
		  (player1.GetComponent<PlayerMovement>().onButton && player2.GetComponent<PlayerMovement>().onButton))){
			Debug.Log("Advancing Level...");
			level++;
			Debug.Log("Player 1: " + player1);
			Debug.Log("Player 2: " + player2);
			Debug.Log("Now starting level " + level);
			advanceLevel();
			photonView.RPC("advanceLevel", RpcTarget.All);
		}
	}


	public void setP1(GameObject p){
		player1 = p;
		Debug.Log("P1 Assigned!");
		if(GameObject.Find("P2") != null && player2 == null){
			Debug.Log("Assigning P2...");
			player2 = GameObject.Find("P2");
		}
		else{Debug.Log("P2 is null OR ALREADY ASSIGNED right now.");}
	}
	public void setP2(GameObject p){
		player2 = p;
		Debug.Log("P2 Assigned!");
		if(GameObject.Find("P1") != null && player1 == null){
			Debug.Log("Assigning P1...");
			player1 = GameObject.Find("P1");
		}
		else{Debug.Log("P1 is null OR ALREADY ASSIGNED right now.");}
	}

	[PunRPC]
	public void advanceLevel(){
        GameObject.Find("P1").transform.position = new Vector3 (p1ResetPos[level].x, p1ResetPos[level].y , 0f);
        GameObject.Find("P1_Camera").transform.position = new Vector3 (p1camPos[level].x, p1camPos[level].y , -10f);
        GameObject.Find("P2").transform.position = new Vector3 (p2ResetPos[level].x, p2ResetPos[level].y , 0f);
        GameObject.Find("P2_Camera").transform.position = new Vector3 (p2camPos[level].x, p2camPos[level].y , -10f);
    }


}
