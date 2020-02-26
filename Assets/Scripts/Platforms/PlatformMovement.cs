using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformMovement : MonoBehaviour
{
    //private
    private Vector3 startPosition;
    private float calculatedAmplitude;
    
    //public
    public float amplitude = .5f;
    public float frequency = 1f;

    public enum movementType {
        Horizontal, Vertical, TLDiagonal, TRDiagonal, BLDiagonal, BRDiagonal
    };  
    //For diagonal movements the TL = top left, TR = top right BL = bottom left BR = bottom right
    //Platform starts in the "center" and moves towards the direction indicated
    public movementType moveType; 
    

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        calculatedAmplitude = amplitude / (2 * Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {

        switch (moveType)
        {
            case movementType.Horizontal:
                Debug.Log("Horizontal");
                startPosition = transform.position;
                startPosition.x += Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
                transform.position = startPosition;
                break;
            case movementType.Vertical:
                Debug.Log("Vertical");
                startPosition = transform.position;
                startPosition.y += Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
                transform.position = startPosition;
                break;
            case movementType.TLDiagonal:
                Debug.Log("TLDiagonal");
                //<-1,1>
                startPosition = transform.position;
                startPosition.x -= Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
                startPosition.y += Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
                transform.position = startPosition;
                break;
            case movementType.TRDiagonal:
                Debug.Log("TRDiagonal");
                //<1,1>
                startPosition = transform.position;
                startPosition.x += Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
                startPosition.y += Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
                transform.position = startPosition;
                break;
            case movementType.BLDiagonal:
                Debug.Log("BLDiagonal");
                //<-1,-1>
                startPosition = transform.position;
                startPosition.x -= Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
                startPosition.y -= Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
                transform.position = startPosition;
                break;
            case movementType.BRDiagonal:
                Debug.Log("BRDiagonal");
                //<1,-1>
                startPosition = transform.position;
                startPosition.x += Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
                startPosition.y -= Mathf.Sin(Time.time * frequency) * calculatedAmplitude;
                transform.position = startPosition;
                break;
        }

    }


    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.gameObject.transform.SetParent(this.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.gameObject.transform.SetParent(null); //currently, momentum is halted completely upon exiting the platform's collider
        }
    }
}
