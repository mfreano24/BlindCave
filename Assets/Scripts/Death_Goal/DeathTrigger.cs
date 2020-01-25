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
        if(other.CompareTag("Player")){
            winlose.GetComponent<WinLoseUI>().lose = true;
            StartCoroutine(restartScene());
        }
    }

    public IEnumerator restartScene(){
        yield return new WaitForSeconds(3);
        Application.LoadLevel(Application.loadedLevel);
    }
}
