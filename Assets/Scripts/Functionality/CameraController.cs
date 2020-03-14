using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CameraController : MonoBehaviourPunCallbacks, IPunObservable
{
    #region IPunObservable Implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        
        if(stream.IsWriting){
        }
        else{
        }
    }
    #endregion
    void Start()
    {
    
    }

    
    void Update()
    {
        
    }
}
