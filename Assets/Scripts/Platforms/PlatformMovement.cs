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
        Horizontal, Vertical, Diagonal
    };  
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
            case movementType.Diagonal:
                Debug.Log("Diagonal");
                break;
        }

    }
}
