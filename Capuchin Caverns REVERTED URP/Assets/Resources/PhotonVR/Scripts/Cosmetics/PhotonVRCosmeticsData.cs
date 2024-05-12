using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.VR.Cosmetics
{
    [Serializable]
    public class PhotonVRCosmeticsData
    {
        public string Head;
        public string Face;
        public string Badge;
        public string Body;
        public string LeftHand;
        public string RightHand;
    }

    public enum CosmeticType
    {
        Head,
        Face,
        Badge,
        Body,
        BothHands,
        LeftHand,
        RightHand
    }
}