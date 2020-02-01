using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public GameObject winlose;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
    }

    public IEnumerator restartScene(){
        yield return new WaitForSeconds(3);
        Application.LoadLevel(Application.loadedLevel);
    }
}
