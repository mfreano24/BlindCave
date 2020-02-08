using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;


public class LightBall : MonoBehaviour
{
    GameObject player;
    GameObject camera;
    bool onPlayer;
    public void isPlayerLight(bool b){
        onPlayer = b;
    }
    void Awake(){
        //set the player here so we can pull some bullshit in Start() oh yeah babey swag
        player = GameObject.Find(PlayerMovement.LocalPlayerInstance.gameObject.name); //??????
        camera = GameObject.Find("P2_Camera"); //PLACEHOLDER
        
    }
    void Start(){
        //manage the visible brightnesses and activity in GameManager, this should just SPAWN the lights.
        
    }

    void Update(){
        if(onPlayer){
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 2);
        }
        else{
            transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, 3);
        }
        

    }
}
