using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totems : MonoBehaviour
{

    GameObject player = GameObject.FindWithTag("Player");
    GameObject child = new GameObject();

    // Start is called before the first frame update
    void Start()
    {
        child.transform.localPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        child.transform.parent = transform;
        child.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
