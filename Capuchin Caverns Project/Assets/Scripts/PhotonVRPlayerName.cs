using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

//this script edited by Liam, should be placed on the playerName in the photonVR player prefab.
// 1) Makes name face right direction.
// 2) 
// 
// 

namespace Photon.VR.Player
{
    public class PhotonVRPlayerName : MonoBehaviour
    {
        public Transform Head;

        [Header("NEVER ADJUST CHILD TMP RECT TRANSFORM, ONLY CHANGE PLAYERNAME GAMEOBJECT Y TRANSFORM to adjust playername.")]
        private float headOffset;

        private void Start() {
            //get playerName's initial transform where Head.positon is not being added.
            headOffset = transform.position.y - Head.position.y;
        }
        private void Update()
        {
            transform.position = Head.position + new Vector3(0f, headOffset, 0f);

            //make name face right direction.
            Vector3 direction = PhotonVRManager.Manager.Head.position - transform.position;
            Quaternion quaternion = new Quaternion(0, Quaternion.LookRotation(direction).y, 0, Quaternion.LookRotation(direction).w);
            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, 10 * Time.deltaTime);
        }
    }

}

// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using TMPro;

// //this script edited by Liam, should be placed on the playerName in the photonVR player prefab.
// // 1) Makes name face right direction.
// // 2) 
// // 
// // 

// namespace Photon.VR.Player
// {
//     public class PhotonVRPlayerName : MonoBehaviour
//     {
//         public Transform Head;

//         [Header("Head Offset is this Transform Component's X, Y, Z positions.")]
//         [Header("NEVER ADJUST CHILD TMP RECT TRANSFORM, ONLY CHANGE PLAYERNAME GAMEOBJECT TRANSFORM.")]
//         public Vector3 HeadOffset;

//         private void Update()
//         {
//             transform.position = Head.position + HeadOffset;

//             Vector3 direction = PhotonVRManager.Manager.Head.position - transform.position;
//             Quaternion quaternion = new Quaternion(0, Quaternion.LookRotation(direction).y, 0, Quaternion.LookRotation(direction).w);
//             transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, 10 * Time.deltaTime);
//         }
//     }

// }