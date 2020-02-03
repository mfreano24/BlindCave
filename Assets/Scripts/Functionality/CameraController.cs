using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    int level;
    Vector3 curr;
    
    void Start()
    {
        level = 0; //this will be an index in an array that we will eventually reference here
        //alternatively, could just reference camera_pos[i-1] and that'd produce the same effect
        //as long as we do the same thing, or keep it in globalvars, consistency is cool but
        //im just testing the feature out right now!!!!
        //set starting positions probably unless we just start them wherever we want and update from there 
    }

    
    void Update()
    {
        /*Testing the camera move function with an input key real fast this will be gone soon
        please dont kill me i'm a good person*/

        //LEVEL 0 P1 = (-5.65, 12)
        //LEVEL 0 P2 = (-5.65, -7)
        Vector3 Level1_P1 = new Vector3(7, 14, transform.position.z);
        Vector3 Level1_P2 = new Vector3(7, -5, transform.position.z);

        if(Input.GetKeyDown(KeyCode.E)){
            level = 1;
        }
        
        if(level == 1){
            if(this.gameObject.name == "P1_Camera"){
                //move the camera over to the new position
                transform.position = new Vector3(Level1_P1.x, Level1_P1.y, Level1_P1.z);
            }
            else if(this.gameObject.name == "P2_Camera"){

            }
        }
        
    }
}
