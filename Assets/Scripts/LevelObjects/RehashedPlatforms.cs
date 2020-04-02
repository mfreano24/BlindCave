using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RehashedPlatforms : MonoBehaviourPunCallbacks, IPunObservable
{

    #region IPunObservable Implementation
    float lag;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        
        if(stream.IsWriting){
            stream.SendNext(transform.position);    
        }
        else{
            transform.position = (Vector3)stream.ReceiveNext();
        }
    }

    #endregion
    [SerializeField]
    Vector3 max;
    [SerializeField]
    Vector3 min;
    [SerializeField]
    int divbase;
    [SerializeField]
    float bound;
    Vector3 middle;
    int direction;


    public bool buttonPressed = false;

    void Start()
    {
        middle = (max + min)/2;
        transform.position = middle;
        direction = 1; //start positive?
        /*
        */
    }

    void Update()
    {
        if(PhotonNetwork.PlayerList.Length > 1 || buttonPressed == true){
            //Input.GetKey(KeyCode.P) /*remove this extra cond later pls*/
            if(direction > 0){
                Vector3 dist = max - transform.position;
                dist.z = 0; //dont want to be moving on that axis.
                if(Mathf.Abs(dist.magnitude) <= bound){
                    direction = -1;
                    Debug.Log("direction now negative");
                }
                else{
                    transform.position += dist/divbase;
                }
            }


            else if(direction < 0){
                Vector3 dist = min - transform.position;
                dist.z = 0; //dont want to be moving on that axis.
                if(Mathf.Abs(dist.magnitude) <= bound){
                    direction = 1;
                    Debug.Log("direction now positive");
                }
                else{
                    transform.position += dist/divbase;
                }
            }    
        }    
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.gameObject.transform.SetParent(this.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.MaintainMomentum(); //need to either have the parent not set to null, or maybe pass the velocity to the other function
            other.gameObject.transform.SetParent(null); //currently, momentum is halted completely upon exiting the platform's collider
        }
    }
}
