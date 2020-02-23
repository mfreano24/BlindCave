using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformMovement : MonoBehaviour
{
    //private
    private Vector3 startPosition;
    
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
    }

    // Update is called once per frame
    void Update()
    {

        switch (moveType)
        {
            case movementType.Horizontal:
                Debug.Log("Horizontal");
                startPosition = transform.position;
                startPosition.x += Mathf.Sin(Time.time * frequency) * amplitude * Time.deltaTime;
                transform.position = startPosition;
                break;
            case movementType.Vertical:
                Debug.Log("Vertical");
                startPosition = transform.position;
                startPosition.y += Mathf.Sin(Time.time * frequency) * amplitude * Time.deltaTime;
                transform.position = startPosition;
                break;
            case movementType.Diagonal:
                Debug.Log("Diagonal");
                break;
        }

    }
}
