using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
//using Photon.VR;
// On Culling mask - can be seen.
// Not on culling mask - cannot be seen.
// This script hides the gameobject with this script from this player's view. All the other players are able still see that gameobject.

// SET UP GUIDE
// - Create a layer for gameobjects that are going to be hidden from the player's view. Put that in this script
// - On the gorilla player's main camera, unselect that layer from the culling mask..
// - Attack this script to the gameobjects which are going to be hidden from the player's view. 
// - Fill in the photonView blank in the inspector.

// Not sure if it actually works lol. It seems to at least for the nose.
public class CullOnlyFromMyCamera : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    private string layerToHideName = "Realtime Directional Lightable and Cull From Main Camera";
    private void OnEnable()
    {
        if (photonView.IsMine) 
        {
            this.gameObject.layer = LayerMask.NameToLayer(layerToHideName);
        }
    }

}
