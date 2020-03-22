using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtons : MonoBehaviour
{


    private bool platformRotating;
    private bool buttonPressed = false;
    private bool triggeredEffect;

    private Color oldColor;


    public enum buttonType
    {
        rotatePlatformLeft, rotatePlatformRight, colorChange
    };




    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("changeColor", 1.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //in here check to see if the button is triggered or not
     
        
    }

    private void changeColor()
    {
        GameObject childObject = transform.GetChild(0).gameObject;
        if(childObject == null)
        {
            Debug.Log("The child is NULL");
        }
        else
        {
            SpriteRenderer renderer = childObject.GetComponent<SpriteRenderer>();
            if(renderer!= null)
            {
                triggeredEffect = false;
                if ((buttonPressed == false) && (triggeredEffect == false)) {
                    oldColor = renderer.color;

                    renderer.color = Color.red;

                    buttonPressed = true;
                    triggeredEffect = true;

                }
                else if((buttonPressed == true) && (triggeredEffect == false))
                {
                    renderer.color = oldColor;

                    buttonPressed = false;
                    triggeredEffect = true;
                }
            }
            else
            {
                Debug.Log("The sprite renderer of the child is null");
            }
        }
    }

}
