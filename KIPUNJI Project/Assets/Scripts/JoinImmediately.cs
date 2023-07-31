using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;

public class JoinImmediately : MonoBehaviour
{

    void Start()
    {
        PhotonVRManager.ChangeServers("afacb457-a628-4810-b8f1-51ff7d6e3012", "86dbaa97-0c17-45ca-a33c-656d22a9f1e2");
    }

}
