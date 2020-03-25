using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
{
    

    #region IPunObservable Implementation
    float lag;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        
        if(stream.IsWriting){
            stream.SendNext(isJumping);
            stream.SendNext(rb.position);
            stream.SendNext(rb.velocity);
        }
        else{
            this.isJumping = (bool)stream.ReceiveNext();
            rb.position = (Vector3)stream.ReceiveNext();
            rb.velocity = (Vector3)stream.ReceiveNext();

            lag = Mathf.Abs((float) (PhotonNetwork.Time - info.timestamp));
            rb.position += rb.velocity * lag;

        }
    }
    #endregion

    //COMPONENTS
    public static GameObject LocalPlayerInstance;
    public GameObject playerlightPrefab;
    public GameObject cavelightPrefab;
    public GameObject pauseScreen;
    GameObject light_inst;
    GameObject backlight_inst;
    GameObject p1_backing;
    GameObject p2_backing;
    GameObject cam;
    
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

    public bool onButton;




    //Photon Configs go in Awake() so that Start() can be called successfully.
    void Awake(){
        if(photonView.IsMine){
            PlayerMovement.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);

    }
    void Start() {
        //we are gonna do this the hard way i guess
        if(this.gameObject == LocalPlayerInstance && PhotonNetwork.PlayerList.Length == 1){
            Debug.Log("Name scheme #1");
            this.gameObject.name = "P1";
        }
        else if(this.gameObject == LocalPlayerInstance && PhotonNetwork.PlayerList.Length == 2){
            Debug.Log("Name scheme #2");
            this.gameObject.name = "P2";
        }
        else if(this.gameObject != LocalPlayerInstance && GameObject.Find("P1") == null){
            Debug.Log("Name scheme #3");
            this.gameObject.name = "P1";
        }
        else if(GameObject.Find("P1") != null && this.gameObject != LocalPlayerInstance){
            Debug.Log("Name scheme #4");
            this.gameObject.name = "P2";
        }

        
        

        
        rb = GetComponent<Rigidbody2D>();
		gv = GameObject.Find("EventSystem").GetComponent<GlobalVars>();

        
        if(this.gameObject.name == "P1"){
            Debug.Log("Set Player 1!");
            gv.setP1(this.gameObject);
            this.transform.position = new Vector3(gv.p1ResetPos[gv.level].x, gv.p1ResetPos[gv.level].y , 0f);

        }

        else if(this.gameObject.name == "P2"){
            Debug.Log("Set Player 2!");
            gv.setP2(this.gameObject);
            this.transform.position = new Vector3(gv.p2ResetPos[gv.level].x, gv.p2ResetPos[gv.level].y , 0f);
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
            lb.range = 4.5f;
            lb.intensity = 1;

            backlight_inst = Instantiate(cavelightPrefab);
            backlight_inst.gameObject.name = "P2_backlight";
            GameObject cam_tmp = GameObject.Find("P2_Camera");
            backlight_inst.transform.position = new Vector3(cam_tmp.transform.position.x, cam_tmp.transform.position.y, backlight_inst.transform.position.z);
            backlight_inst.transform.parent = cam_tmp.transform;
            backlight_inst.GetComponent<Light>().range /= 2;



            
            


            //p2 camera backing
            p2_backing.SetActive(true);
            p1_backing.SetActive(true);

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

            backlight_inst = Instantiate(cavelightPrefab);
            backlight_inst.gameObject.name = "P1_backlight";
            GameObject cam_tmp = GameObject.Find("P1_Camera");
            backlight_inst.transform.position = new Vector3(cam_tmp.transform.position.x, cam_tmp.transform.position.y, backlight_inst.transform.position.z);
            backlight_inst.transform.parent = cam_tmp.transform;
            backlight_inst.GetComponent<Light>().range /= 2;

            //p1 backing
            p1_backing.SetActive(true);
            p2_backing.SetActive(true);

        }
        //some problems here, will get fixed surely :)
        flipped = 1;
        onButton = false;
        cam = GameObject.Find(this.gameObject.name+"_Camera");
        DontDestroyOnLoad(cam);


        pauseScreen = GameObject.FindGameObjectWithTag("Pause");
        pauseScreen.SetActive(false);
    }

    void FixedUpdate() {
        if (!photonView.IsMine)
        {
            rb.position = Vector3.MoveTowards(rb.position, rb.velocity * lag, Time.fixedDeltaTime);
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
        if(rb.velocity.y < 0){
            //falling condition for smoother jumping
            rb.velocity += Vector2.up * Physics2D.gravity.y * (2.5f-1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump")){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (2-1) * Time.deltaTime;

        }
        
    }


    private void Update() {
        if(Input.GetKeyDown(KeyCode.L)){
            //debug purposes
            Debug.Log("On button: " + onButton);
        }

        if(Input.GetButtonDown("Pause") && pauseScreen.active){
            pauseScreen.SetActive(false);
        }
        else if(Input.GetButtonDown("Pause") && !pauseScreen.active){
            pauseScreen.SetActive(true);
        }
    }

	// TODO: Wall jumping is possible and we gotta remove it?

	private void DeathToPlayer() {
        death();
    }


    public void MaintainMomentum()
    {   //check to see if parent is not null
        if((transform.parent != null) && (transform.parent.GetComponent<PlatformMovement>() != null))
        {
            PlatformMovement pVelocity = this.GetComponentInParent<PlatformMovement>();
            Vector3 parentVelocity = pVelocity.velocity;
            //rb.velocity = new Vector3(rb.velocity.x + pVelocity.x, jumpHeight, 0);
            Debug.Log("Platform Velocity: " + pVelocity);
        }
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

    public void death(){ //should be an IEnumerator when i fix the SHIT
        /*Vector3 defScale = transform.localScale;
        float defSpeed = playerSpeed;
        playerSpeed = 0;
        for(int i = 0; i < 20 ; i++){
            transform.localScale = 0.9f * transform.localScale;
            yield return new WaitForSeconds(0.1f);
        }*/
        if (name == "P1") {
			transform.position = gv.p1ResetPos[gv.level];
			gv.p1Death++;
		} else {
			transform.position = gv.p2ResetPos[gv.level];
			gv.p2Death++;
		}
        //transform.localScale = defScale;
        //playerSpeed = defSpeed;
    }

    public void Quit(){
        PhotonNetwork.Disconnect();
    }


    



}
