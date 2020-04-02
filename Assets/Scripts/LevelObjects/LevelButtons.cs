using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtons : MonoBehaviour
{


    private bool platformRotating;

    //should be private, public for testing
    public bool buttonPressed;
    public bool triggeredEffect;

    private Color oldColor;

    [SerializeField]
    GameObject colorChangingBlock;

    SpriteRenderer renderer;

    public enum bType
    {
        rotatePlatformLeft, rotatePlatformRight, colorChange
    };
    public bType buttonType;




    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("changeColorTimer", 1.0f, 2.0f);
        buttonPressed = false;
        if (colorChangingBlock != null)
        {
            renderer = colorChangingBlock.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                oldColor = renderer.color;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //in here check to see if the button is triggered or not
     
        
    }

    private void changeColor()
    {
        if (colorChangingBlock == null)
        {
            Debug.Log("The colorChangingBlock is NULL");
        }
        else
        {
            if (renderer != null)
            {
                //triggeredEffect = false;
                if ((triggeredEffect == false))
                {
                    //oldColor = renderer.color;

                    renderer.color = Color.red;

                    //buttonPressed = true;
                    triggeredEffect = true;

                }
                else if (triggeredEffect == true)
                {
                    renderer.color = oldColor;

                    //buttonPressed = false;
                    triggeredEffect = false;
                }
            }
            else
            {
                Debug.Log("The sprite renderer of the child is null");
            }
        }
    }

    private void changeColorTimer()
    {
        //GameObject childObject = transform.GetChild(0).gameObject;
        if(colorChangingBlock == null)
        {
            Debug.Log("The colorChangingBlock is NULL");
        }
        else
        {
            SpriteRenderer renderer = colorChangingBlock.GetComponent<SpriteRenderer>();
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

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Level Button On Trigger Stay");
        if (other.CompareTag("Totem"))
        {
            Debug.Log("Colliding with a totem");
            
        }
        else
        {
            Debug.Log("NOT Totem");
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Totem"))
        {
            Totems t = other.GetComponent<Totems>();
            if (t.carry == false)
            {
                //need to check the enum in here
                switch (buttonType)
                {
                    case bType.colorChange:
                        buttonPressed = true;
                        triggeredEffect = false;
                        changeColor();
                        break;

                }
                
            }
        }
        else
        {

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Totem"))
        {
            Totems t = other.GetComponent<Totems>();
            if (t.carry == true)
            {
                //need to check the enum in here
                switch (buttonType)
                {
                    case bType.colorChange:
                        buttonPressed = false;
                        triggeredEffect = true;
                        changeColor();
                        break;

                }

            }
        }
        else
        {

        }
    }

}
