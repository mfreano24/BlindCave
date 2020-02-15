using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
{

    #region IPunObservable Implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if(stream.IsWriting){
            stream.SendNext(isJumping);
        }
        else{
            this.isJumping = (bool)stream.ReceiveNext();
        }
    }
    #endregion

    //COMPONENTS
    public static GameObject LocalPlayerInstance;
    public GameObject playerlightPrefab;
    public GameObject cavelightPrefab;
    GameObject light_inst;
    GameObject p1_backing;
    GameObject p2_backing;
    
    Rigidbody2D rb;
	GlobalVars gv;
    //VARS
    Vector2 moveDirection;
    public float playerSpeed;
    public float jumpHeight;
	bool isPlaying = true;
    public float hSpeed = 5f;
    public float jumpPower = 15f;
    private int directionFacing = 1;
    private bool isJumping = false;
    int flipped;

    //Photon Configs go in Awake() so that Start() can be called successfully.
    void Awake(){
        if(photonView.IsMine){
            PlayerMovement.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);

    }
    void Start() {
        this.gameObject.name = "P" + PhotonNetwork.PlayerList.Length.ToString();
        rb = GetComponent<Rigidbody2D>();
		gv = GameObject.Find("EventSystem").GetComponent<GlobalVars>();

        /*
        on network, we want the local player's light to be small and to follow them and the nonlocal player's light to be big and light up the whole room.
        */
        if(this.gameObject.name == "P1"){
            this.transform.position = new Vector3(-8.3f, 11f, -4f);

        }

        else if(this.gameObject.name == "P2"){
            this.transform.position = new Vector3(-8.3f, -8f, 0f);
        }

        p1_backing = GameObject.Find("P1_BackingMask");
        p2_backing = GameObject.Find("P2_BackingMask");

        if(PlayerMovement.LocalPlayerInstance.gameObject.name == "P1"){
            //p1 light
            light_inst = Instantiate(playerlightPrefab);
            light_inst.gameObject.name = "P1_light";
            light_inst.GetComponent<LightBall>().isPlayerLight(true);
            light_inst.transform.parent = GameObject.Find("P1").transform;
            Light lb =  light_inst.GetComponent<Light>();
            lb.color = new Color(242f/255f, 216f/255f, 114f/255f);
            lb.range = 3;
            lb.intensity = 1;
            
            //p2 area light
            /*light_inst = Instantiate(cavelightPrefab);
            light_inst.gameObject.name = "P2_light";
            light_inst.GetComponent<LightBall>().isPlayerLight(false);
            light_inst.transform.parent = GameObject.Find("P2_Camera").transform;
            lb =  light_inst.GetComponent<Light>();
            lb.color = new Color(190f/255f, 161f/255f, 108f/255f);
            lb.range = 40;
            lb.intensity = 5;*/


            //p2 camera backing
            p2_backing.SetActive(false);
            //p1_backing.SetActive(true);

        }

        else if(PlayerMovement.LocalPlayerInstance.gameObject.name == "P2"){
            //p1 light
            light_inst = Instantiate(playerlightPrefab);
            light_inst.gameObject.name = "P2_light";
            light_inst.GetComponent<LightBall>().isPlayerLight(true);
            light_inst.transform.parent = GameObject.Find("P2").transform;
            Light lb =  light_inst.GetComponent<Light>();
            lb.color = new Color(242f/255f, 216f/255f, 114f/255f);
            lb.range = 3;
            lb.intensity = 1;

            /*p2 area light
            light_inst = Instantiate(cavelightPrefab);
            light_inst.gameObject.name = "P1_light";
            light_inst.GetComponent<LightBall>().isPlayerLight(false);
            light_inst.transform.parent = GameObject.Find("P1_Camera").transform;
            lb =  light_inst.GetComponent<Light>();
            lb.color = new Color(190f/255f, 161f/255f, 108f/255f);
            lb.range = 40;
            lb.intensity = 5;*/

            //p1 backing
            p1_backing.SetActive(false);
            p2_backing.SetActive(true);

        }
        //some problems here, will get fixed surely :)
        flipped = 1;

        
    }

    void Update() {
        if(!photonView.IsMine && PhotonNetwork.IsConnected){
            return;
        }

		if (!isPlaying) {
			return;
		}

		float xInput = (flipped) * Input.GetAxis("Horizontal");
        rb.velocity = new Vector3((xInput * hSpeed), rb.velocity.y, 0);

        //if grounded
        if(rb.velocity.y != 0) {
            isJumping = true;
        }
        else if (!isJumping) {  
            //Jump
            if (Input.GetButton("Jump")) {
                isJumping = true;
                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, 0);
            }
        }
        if(rb.velocity.y == 0){
            isJumping = false;
        }
        
    }

	// TODO: Wall jumping is possible and we need to remove it.

	private void DeathToPlayer() {
		if (name == "P1") {
			transform.position = gv.p1ResetPos[gv.level];
			gv.p1Death++;
		} else {
			transform.position = gv.p2ResetPos[gv.level];
			gv.p2Death++;
		}

		// TODO: Update UI

	}

	// Collision with Spikes
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.name == "Hazard") {
			DeathToPlayer();
		}
	}


    public void flipControls(){
        flipped = -1;
        //TODO: add condition to unfuck the controls
    }


}
