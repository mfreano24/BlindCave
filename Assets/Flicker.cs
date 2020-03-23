using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        GetComponent<Light>().range += 0.5f*Mathf.Sin(60*Time.time);
        
    }
}
