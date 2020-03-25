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
            stream.SendNext(transform.position);
        }
        else{
            transform.position = (Vector3)stream.ReceiveNext();
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

    public enum movementType {
        Horizontal, Vertical, TLDiagonal, TRDiagonal, BLDiagonal, BRDiagonal, RoatateLeft, RotateRight
    };  
    //For diagonal movements the TL = top left, TR = top right BL = bottom left BR = bottom right
    //Platform starts in the "center" and moves towards the direction indicated
    public movementType moveType; 
    

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        lastPosition = transform.position;
        calculatedAmplitude = 0.25f * (amplitude / (Mathf.PI));
    }

    // Update is called once per frame
    void Update()
    {
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

    private void HorizontalMovement()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        Debug.Log("Horizontal");
        startPosition = transform.position;
        startPosition.x += Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
        transform.position = startPosition;
    }

    private void VerticalMovement()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        Debug.Log("Vertical");
        startPosition = transform.position;
        startPosition.y += Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
        transform.position = startPosition;
    }

    private void TLDiagonalMovment()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        Debug.Log("TLDiagonal");
        //<-1,1>
        startPosition = transform.position;
        startPosition.x -= Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
        startPosition.y += Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
        transform.position = startPosition;
    }

    private void TRDiagonalMovement()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        Debug.Log("TRDiagonal");
        //<1,1>
        startPosition = transform.position;
        startPosition.x += Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
        startPosition.y += Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
        transform.position = startPosition;
    }

    private void BLDiagonalMovement()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        Debug.Log("BLDiagonal");
        //<-1,-1>
        startPosition = transform.position;
        startPosition.x -= Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
        startPosition.y -= Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
        transform.position = startPosition;
    }

    private void BRDiagonalMovement()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        Debug.Log("BRDiagonal");
        //<1,-1>
        startPosition = transform.position;
        startPosition.x += Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
        startPosition.y -= Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
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
