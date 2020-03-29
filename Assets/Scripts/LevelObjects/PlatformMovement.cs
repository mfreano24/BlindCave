using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PlatformMovement : MonoBehaviourPunCallbacks, IPunObservable
{

    #region IPunObservable Implementation
    float lag;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        
        if(stream.IsWriting){
            
            if(enabled){
                stream.SendNext(transform.position);
            }
            
        }
        else{
            if(!enabled){
                transform.position = (Vector3)stream.ReceiveNext();
            }
            
            //may need some lag reduc
        }
    }
    #endregion
    //private
    private Vector3 startPosition;
    private float calculatedAmplitude;
    private Vector3 lastPosition;
    //public
    public float amplitude = .5f;
    public float frequency = 1f;

    public Vector3 velocity;// FIX: potentially need to make private, public to see what the value is
    bool movementLocal;
    float timeOffset;

    int direction; //1 or -1, depending on the direction.
    Vector3 maximum;
    Vector3 minimum;

    /*
    * DESIGN: Define two positions that are max and min, those will be "horizontal asymptotes" of the platform's movement path.
    *
    *
    */


    public enum movementType {
        Horizontal, Vertical, TLDiagonal, TRDiagonal, BLDiagonal, BRDiagonal, RoatateLeft, RotateRight
    };  
    //For diagonal movements the TL = top left, TR = top right BL = bottom left BR = bottom right
    //Platform starts in the "center" and moves towards the direction indicated
    public movementType moveType; 
    

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.PlayerList.Length == 1){
            enabled = true;
        }
        else{
            enabled = false;
        }
        //Purpose of enabled is so that the platform can just sync in the 2nd player's game instead of calculating its position on both ends.
        startPosition = transform.position;
        lastPosition = transform.position;
        calculatedAmplitude = 0.25f * (amplitude / (Mathf.PI));
    }

    // Update is called once per frame
    void Update()
    {
        if(enabled){
            switch (moveType)
            {
                case movementType.Horizontal:
                    HorizontalMovement();
                    break;
                case movementType.Vertical:
                    VerticalMovement();
                    break;
                case movementType.TLDiagonal:
                    TLDiagonalMovment();
                    break;
                case movementType.TRDiagonal:
                    TRDiagonalMovement();
                    break;
                case movementType.BLDiagonal:
                    BLDiagonalMovement();
                    break;
                case movementType.BRDiagonal:
                    BRDiagonalMovement();
                    break;
                case movementType.RoatateLeft:
                    RotateLeftMovement();
                    break;
                case movementType.RotateRight:
                    RotateRightMovement();
                    break;
            }

        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, (transform.position + maximum)*direction* lag, Time.fixedDeltaTime);
        }
        
    }

    private void HorizontalMovement()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        Debug.Log("Horizontal");
        startPosition = transform.position;
        startPosition.x += Mathf.Sin((float)PhotonNetwork.Time * frequency) * calculatedAmplitude;
        transform.position = startPosition;
    }

    private void VerticalMovement()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        Debug.Log("Vertical");
        startPosition = transform.position;
        startPosition.y += Mathf.Sin((float)PhotonNetwork.Time * frequency) * calculatedAmplitude;
        transform.position = startPosition;
    }

    private void TLDiagonalMovment()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        Debug.Log("TLDiagonal");
        //<-1,1>
        startPosition = transform.position;
        startPosition.x -= Mathf.Sin((float)PhotonNetwork.Time  * frequency) * calculatedAmplitude;
        startPosition.y += Mathf.Sin((float)PhotonNetwork.Time  * frequency) * calculatedAmplitude;
        transform.position = startPosition;
    }

    private void TRDiagonalMovement()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        Debug.Log("TRDiagonal");
        //<1,1>
        startPosition = transform.position;
        startPosition.x += Mathf.Sin((float)PhotonNetwork.Time  * frequency) * calculatedAmplitude;
        startPosition.y += Mathf.Sin((float)PhotonNetwork.Time  * frequency) * calculatedAmplitude;
        transform.position = startPosition;
    }

    private void BLDiagonalMovement()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        Debug.Log("BLDiagonal");
        //<-1,-1>
        startPosition = transform.position;
        startPosition.x -= Mathf.Sin((float)PhotonNetwork.Time  * frequency) * calculatedAmplitude;
        startPosition.y -= Mathf.Sin((float)PhotonNetwork.Time  * frequency) * calculatedAmplitude;
        transform.position = startPosition;
    }

    private void BRDiagonalMovement()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        Debug.Log("BRDiagonal");
        //<1,-1>
        startPosition = transform.position;
        startPosition.x += Mathf.Sin((float)PhotonNetwork.Time  * frequency) * calculatedAmplitude;
        startPosition.y -= Mathf.Sin((float)PhotonNetwork.Time  * frequency) * calculatedAmplitude;
        transform.position = startPosition;
    }

    private void RotateLeftMovement()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        Debug.Log("RotateLeft");
        transform.Rotate(Vector3.forward);

    }

    private void RotateRightMovement()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        Debug.Log("RotateRight");
        transform.Rotate(Vector3.back);
    }



    /*public void startMoving(float c){
        moving = true;
        timeOffset = c;
    }*/



    
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
