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
            stream.SendNext(transform.position);
        }
        else{
            transform.position = (Vector3)stream.ReceiveNext();
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
