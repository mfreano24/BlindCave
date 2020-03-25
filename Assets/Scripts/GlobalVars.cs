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

	public List<Vector2> p1buttonPos;
	public List<Vector2> p2buttonPos;

	public int p1Death = 0;
	public int p2Death = 0;

	public int level = 0;

	public GameObject player1;
	public GameObject player2;

	public GameObject button1;
	public GameObject button2;

	bool cooldown;

	private void Start() {
		button1 = GameObject.Find("P1L1");
		button2 = GameObject.Find("P2L1");
		player1 = null;
		player2 = null;
		cooldown = false;

		p1ResetPos = new List<Vector2>(){
			new Vector2(-11.2f, 11), //1
			new Vector2(12.71f, 7.74f), //2
			new Vector2(44.69f, 5.6f)
		};
		
		p2ResetPos = new List<Vector2>() {
			new Vector2(-12.26f, -29.59f), //1
			new Vector2(12.71f, -29.35f), //2
			new Vector2(45.1f, -33f)
		};

		p1camPos = new List<Vector2>(){
			new Vector2(-5.65f, 12),
			new Vector2(19.5f, 12),
			new Vector2(51.5f, 12.3f)
		};

		p2camPos = new List<Vector2>(){
			new Vector2(-5.65f, -28.4f),
			new Vector2(19.5f, -29.3f),
			new Vector2(51.5f, -28.7f)
		};

		p1buttonPos = new List<Vector2>(){
			new Vector2(7.80346f, 7.35f),
			new Vector2(25.5f, 9.91f),
			new Vector2(58.26f, 4.92f)

		};

		p2buttonPos = new List<Vector2>(){
			new Vector2(7.80346f, -32.55f),
			new Vector2(25.87f, -30.03f),
			new Vector2(56.01f, -27.06f)

		};



	}

	private void Update() {

		//buttoncheck
		//PRESS K FOR DEBUG MODE ADVANCE
		if(Input.GetKeyDown(KeyCode.K) || ((player1 !=null && player2 !=null) && 
		  (player1.GetComponent<PlayerMovement>().onButton && player2.GetComponent<PlayerMovement>().onButton)) && !cooldown){
			cooldown = true;
			StartCoroutine(resetCooldown());
			Debug.Log("Advancing Level...");
			level++;
			Debug.Log("Player 1: " + player1);
			Debug.Log("Player 2: " + player2);
			Debug.Log("Now starting level " + level);
			advanceLevel();
			photonView.RPC("advanceLevel", RpcTarget.All);
		}
	}

	IEnumerator resetCooldown(){
		yield return new WaitForSeconds(1.0f);
		cooldown = false;
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
		GameObject.Find("P1L1").transform.position = new Vector3 (p1buttonPos[level].x, p1buttonPos[level].y , 0f);
        GameObject.Find("P2").transform.position = new Vector3 (p2ResetPos[level].x, p2ResetPos[level].y , 0f);
        GameObject.Find("P2_Camera").transform.position = new Vector3 (p2camPos[level].x, p2camPos[level].y , -10f);
		GameObject.Find("P2L1").transform.position = new Vector3 (p2buttonPos[level].x, p2buttonPos[level].y , 0f);
    }


}
