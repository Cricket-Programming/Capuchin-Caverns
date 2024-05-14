using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.VR.Player; 
using Photon.VR;
using easyInputs;

// Go to Google Drive coding for the TAG SCRIPT DOCUMENTATION.
public class TagScript6 : MonoBehaviourPun
{
    [SerializeField] private Material tagItMaterial;
    [SerializeField] private AudioSource tagSound; // Volume: 0.5 | Spatial Blend: 0.6
    
    [Tooltip("When player gets tagged, this is the amount of time before the player can start tagging other players. It is also the amount of time movement is limited.")]
    [SerializeField] private float touchbackDuration = 2f;

    // FOR INFECTION MODE ONLY
    [SerializeField] private bool EnableInfectionMode_Alpha;     
    [SerializeField] private AudioSource resetGameSound; // Volume: 0.2 | Spatial blend: 0 (2D)
    [SerializeField] private Material infectionItMaterial;

    [HideInInspector] public Material initialMaterial; // Changed by ChangeSkin.cs
    private string tagItMaterialName, infectionItMaterialName;
    private bool isInfected = false;
    private float touchbackCountdown, limitMovementCountdown;
    
    private Rigidbody gorillaPlayerRigidbody;
    private List<MeshRenderer> colourObjects = new List<MeshRenderer>();
    private bool performFlag = true; // This prevents things happening multiple times repeatly.
    private GameObject[] players;
    private List<GameObject> infectionPlayers = new List<GameObject>();
    private bool isInTagArea;

    private void Start()
    {
        
        
        colourObjects = GetComponent<PhotonVRPlayer>().ColourObjects;
        initialMaterial = new Material(colourObjects[0].material); // This makes a copy, not a reference.

        tagItMaterialName = tagItMaterial.name + " (Instance)";
        infectionItMaterialName = infectionItMaterial.name + " (Instance)";

        gorillaPlayerRigidbody = GameObject.Find("GorillaPlayer")?.GetComponent<Rigidbody>();
        if (gorillaPlayerRigidbody == null)
            Debug.LogError("The name of the gorilla player must be `GorillaPlayer` or TagScript won't work correctly");
        
        Invoke("NewPlayerCatchUp", 0.05f); // This gives it time to process avoiding an argumentOutOfRange exception.
    }

    // Invoked by TagExit.cs and TagExit.cs.
    public void ChangeIsInTagArea(bool boolean) {
        //initialMaterial = new Material(colourObjects[0].material); // Sets initialMaterial again. This is so that if the player changed their color and then went into the tag area, initialMaterial is updated correctly.
        photonView.RPC("_RPCChangeIsInTagArea", RpcTarget.AllBuffered, boolean); // Buffering seems to work, and this will automatically catch up players with the value of it.
    }
    [PunRPC]
    private void _RPCChangeIsInTagArea(bool value) {
        isInTagArea = value;
    }

    public bool GetIsInTagArea() => isInTagArea;

    // because for TagPlayer stuff, RpcTarget.AllBuffered is not working, this manually "catches up" the new player with who is it.
    private void NewPlayerCatchUp() {
        UpdateInfectionPlayersList();
        foreach (GameObject infectionPlayer in infectionPlayers)
        {
            TagScript6 infectionPlayerTagScript6 = infectionPlayer.GetComponent<TagScript6>();
            Material infectionPlayerMaterial = infectionPlayerTagScript6.colourObjects[0].material; //the current material of the infectionPlayer
            if (infectionPlayerMaterial.name.Equals(tagItMaterialName) || infectionPlayerMaterial.name.Equals(infectionItMaterialName)) {
                infectionPlayerTagScript6.photonView.RPC("TagPlayer", photonView.Owner);
            }
        }
    }


    private void Update()
    {   
        //EnableInfectionMode_Alpha = (infectionPlayers.Count <= 2) ? false : true;
        //Debug.Log(GetIsInTagArea());
        if (PhotonNetwork.IsMasterClient)
        {
            UpdateInfectionPlayersList();

            if (infectionPlayers.Count > 1) {
                if (EnableInfectionMode_Alpha) {
                    CheckIfAllInfected();
                }
                CheckIfNoneInfected();
            }
        }
        // These stuff are for doing tag entrance logic.
        if (GetIsInTagArea() && infectionPlayers.Count > 1 && performFlag)
        {
            performFlag = false;
            photonView.RPC("PlayTagSound", RpcTarget.All);
        } 
        //outside tag area.
        else if (!GetIsInTagArea() && isInfected) {
            UntagPlayer();
            // photonView.RPC("UntagPlayer", RpcTarget.All);
        }
        //outside tag area.
        if (!GetIsInTagArea() && !performFlag) {
            performFlag = true;
        }

        if (touchbackCountdown > 0f)
        {
            touchbackCountdown -= Time.deltaTime;      
        }

        if (limitMovementCountdown > 0f) 
        {
            limitMovementCountdown -= Time.deltaTime;
            gorillaPlayerRigidbody.velocity = Vector3.zero;
        }

    }

    private void UpdateInfectionPlayersList()
    {
        infectionPlayers.Clear();
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            // Debug.Log(player.GetComponent<TagScript6>().GetIsInTagArea());         
            if (player.GetComponent<TagScript6>().GetIsInTagArea()) 
            {      
                infectionPlayers.Add(player);
            }
        }
    }

    // CheckIfAllInfected(), ResetPlayers(), and PlayResetGameSound() are exclusively for the infection mode.
    private void CheckIfAllInfected()
    {
        foreach (GameObject infectionPlayer in infectionPlayers)
        {
            //if there is a not a lava, then return because not all infected.
            Material infectionPlayerMaterial = infectionPlayer.GetComponent<TagScript6>().colourObjects[0].material;
            if (!infectionPlayerMaterial.name.Equals(infectionItMaterialName))
            {
                return;
            }
        }

        // If all players infected
        ResetPlayers();
        SetRandomPlayerAsIt();
    }
    private void ResetPlayers()
    {
        photonView.RPC("PlayResetGameSound", RpcTarget.All);

        foreach (GameObject infectionPlayer in infectionPlayers)
        {
            infectionPlayer.GetComponent<TagScript6>().photonView.RPC("UntagPlayer", RpcTarget.All);
        }
    }
    [PunRPC]
    private void PlayResetGameSound()
    {
        if (GetIsInTagArea()) {
            resetGameSound.Play();
        }
    }

    private void CheckIfNoneInfected()
    {
        //if there is a lava, then someone is infected.
        foreach (GameObject infectionPlayer in infectionPlayers)    
        {
            Material infectionPlayerMaterial = infectionPlayer.GetComponent<TagScript6>().colourObjects[0].material;
            if (infectionPlayerMaterial.name.Equals(tagItMaterialName) || infectionPlayerMaterial.name.Equals(tagItMaterialName))
            {
                return;
            }
        }

        // If no players infected
        SetRandomPlayerAsIt();
    }

    private void SetRandomPlayerAsIt()
    {
        int randomPlayerIndex = Random.Range(0, infectionPlayers.Count);
        PhotonView randomPlayerPhotonView = infectionPlayers[randomPlayerIndex].GetComponent<TagScript6>().photonView;
        randomPlayerPhotonView.RPC("TagPlayer", RpcTarget.All);
        randomPlayerPhotonView.RPC("PlayTagSound", RpcTarget.All);
        randomPlayerPhotonView.RPC("LimitMovementAndVibrateHands", randomPlayerPhotonView.Owner);
        Debug.Log("Setting random player as it");
    }
    private void VibrateHands()
    {
        StartCoroutine(EasyInputs.Vibration(EasyHand.LeftHand, 0.3f, 0.4f));    
        StartCoroutine(EasyInputs.Vibration(EasyHand.RightHand, 0.3f, 0.4f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine) return;
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("OthersVRHand")) return; //if you want this to be an if statement, use de morgans law!

        
        //Other will be null when: tagger body touches taggee hand, tagger hand touches taggee hand. 
        TagScript6 otherTagScript6 = other?.gameObject.GetComponent<TagScript6>(); 
        // if other is a VRHand, then other HAS NO TAGSCRIPT. The null conditional operator (?) in otherTagScript6 sets it to null when other has no tag script, preventing a NullReferenceException.
        if (otherTagScript6 != null) {
            if (isInfected && touchbackCountdown <= 0f && !otherTagScript6.isInfected)
            {
                // an "it" player handles stuff here.
                VibrateHands(); 

                if (!EnableInfectionMode_Alpha) {
                    UntagPlayer();
                    //photonView.RPC("UntagPlayer", RpcTarget.All);
                }

                PhotonView otherPhotonView = otherTagScript6.photonView;
                otherPhotonView.RPC("TagPlayer", RpcTarget.All);
                otherPhotonView.RPC("PlayTagSound", RpcTarget.All);
                otherPhotonView.RPC("LimitMovementAndVibrateHands", otherPhotonView.Owner);
            }
        }
    }

    [PunRPC]
    private void TagPlayer()
    {
        isInfected = true;
        
        touchbackCountdown = touchbackDuration;
        if (!EnableInfectionMode_Alpha) {
            foreach (Renderer colourObject in colourObjects) {
                colourObject.material = tagItMaterial;
            }
        }
        else {
            foreach (Renderer colourObject in colourObjects) {
                colourObject.material = infectionItMaterial;
            }
        }

    }

    [PunRPC]
    //this happens when you just got tagged.
    private void LimitMovementAndVibrateHands() {
        limitMovementCountdown = touchbackDuration;
        VibrateHands();
    }


    [PunRPC]
    private void PlayTagSound()
    {
        if (!tagSound.isPlaying && GetIsInTagArea())
        {
            tagSound.Play();
        }
    }
    
    private void UntagPlayer() {
        photonView.RPC("RPCUntagPlayer", RpcTarget.All);
        if (initialMaterial.mainTexture != null) {
            //int initialMaterialSkinIndex = PhotonVRManager.Manager.LocalPlayer.GetComponent<NetworkSkin>().GetSkinIndex(initialMaterial);
            PhotonVRManager.Manager.LocalPlayer.GetComponent<NetworkSkin>().RunSetNetworkSkin(initialMaterial);  // RPCing to everyone that is untagged?
        }
    }
    [PunRPC]
    private void RPCUntagPlayer()
    {
        isInfected = false;
        
        // Because every player instance does not have the same initialMaterial, I can't network this through and RPC call, this won't network it properly. It will only network it locally.
        foreach (Renderer colourObject in colourObjects)
        {
            colourObject.material = new Material(initialMaterial);
        }

    }
}



// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

// using Photon.Pun;
// using Photon.VR.Player; 
// using easyInputs;

// //SEE DOCUMENTATION (in google drive coding) FOR INFORMATION
// //Direction to Tag Area: > Z


// public class TagScript6 : MonoBehaviourPunCallbacks
// {
//     [SerializeField] private Transform headTransform; // Head GameObject of PhotonVR player prefab.
//     [SerializeField] private Material itMaterial;
//     [SerializeField] private AudioSource tagSound;
//     [Tooltip("When player gets tagged, this is the amount of time in seconds before the player can start tagging other players. Also, it is the amount of time movement is limited.")]
//     [SerializeField] private float touchbackDuration = 2f;
//     [SerializeField] private float divideLine;  
//     [HideInInspector] public Material initialMaterial; //gets changed by ChangeSkin.cs
//     private string itMaterialName;
//     private bool isInfected = false;
//     private float touchbackCountdown;
//     private float limitMovementCountdown;

//     private Rigidbody gorillaPlayerRigidbody;
//     private List<MeshRenderer> colourObjects = new List<MeshRenderer>();

//     private bool performFlag = true; //this prevents things happening multiple times repeatly.

//     private GameObject[] players;
//     private List<GameObject> infectionPlayers = new List<GameObject>();

//     private void Start()
//     {
//         colourObjects = GetComponent<PhotonVRPlayer>().ColourObjects;
//         initialMaterial = colourObjects[0].material;

//         itMaterialName = itMaterial.name + " (Instance)";

//         gorillaPlayerRigidbody = GameObject.Find("GorillaPlayer")?.GetComponent<Rigidbody>();
//         if (gorillaPlayerRigidbody == null)
//         {
//             Debug.LogError("Make sure that the name of the gorilla player is `GorillaPlayer`");
//         }
//         Invoke("newPlayerCatchUp", 0.05f); //this gives it time to process avoiding an argumentOutOfRange exception
//     }

//     // because for TagPlayer stuff, RpcTarget.AllBuffered is not working, this manually "catches up" the new player with who is it.
//     private void newPlayerCatchUp() {
//         UpdateInfectionPlayersList();
//         foreach (GameObject infectionPlayer in infectionPlayers)
//         {
//             TagScript6 infectionPlayerTagScript6 = infectionPlayer.GetComponent<TagScript6>();
//             Material infectionPlayerMaterial = infectionPlayerTagScript6.colourObjects[0].material; //the current material of the infectionPlayer
//             if (infectionPlayerMaterial.name.Equals(itMaterialName)) {
//                 infectionPlayerTagScript6.photonView.RPC("TagPlayer", photonView.Owner);
//             }
//         }
//     }
//     private void Update()
//     {

//         if (PhotonNetwork.IsMasterClient)
//         {
//             UpdateInfectionPlayersList();
//             if (infectionPlayers.Count > 1) {
//                 CheckIfNoneInfected();
//             }
//         }
//         //Debug.Log(headTransform.position.z);
//         // These stuff are for doing tag entrance logic.
//         ////red bar is the enter
//         ///inside tag area
//         if (headTransform.position.z > divideLine  && infectionPlayers.Count > 1 && performFlag)
//         {
//             photonView.RPC("PlayTagSound", RpcTarget.All);
//             performFlag = false;
            
//         } 
//         //outside tag area.
//         else if (headTransform.position.z <= divideLine && isInfected) {
//             photonView.RPC("UntagPlayer", RpcTarget.All);

//         }
//         //outside no mans zone.
//         if (headTransform.position.z <= divideLine && !performFlag) {
//             performFlag = true;
//         }

//         // Touchback countdown
//         if (touchbackCountdown > 0f)
//         {
//             touchbackCountdown -= Time.deltaTime;      
//         }

//         if (limitMovementCountdown > 0f) 
//         {
//             limitMovementCountdown -= Time.deltaTime;
//             gorillaPlayerRigidbody.velocity = Vector3.zero;
//         }
        

//     }

//     private void UpdateInfectionPlayersList()
//     {
        
//         infectionPlayers.Clear();
//         players = GameObject.FindGameObjectsWithTag("Player");
//         foreach (GameObject player in players)
//         {
//             //Debug.Log(player.transform.position.z);
//             if (player.transform.position.z > divideLine - 0.5f)
//             {
//                 infectionPlayers.Add(player);
//             }
//         }
//     }

//     private void CheckIfNoneInfected()
//     {
//         //if there is a lava, then someone is infected.
//         foreach (GameObject infectionPlayer in infectionPlayers)    
//         {
//             Material infectionPlayerMaterial = infectionPlayer.GetComponent<TagScript6>().colourObjects[0].material;
//             if (infectionPlayerMaterial.name.Equals(itMaterialName))
//             {
//                 return;
//             }
//         }

//         // If no players infected
//         SetRandomPlayerAsIt();
//     }

//     private void SetRandomPlayerAsIt()
//     {
//         int randomPlayerIndex = Random.Range(0, infectionPlayers.Count);
//         PhotonView randomPlayerPhotonView = infectionPlayers[randomPlayerIndex].GetComponent<TagScript6>().photonView;
//         randomPlayerPhotonView.RPC("TagPlayer", RpcTarget.All);
//         randomPlayerPhotonView.RPC("PlayTagSound", RpcTarget.All);
//         randomPlayerPhotonView.RPC("LimitMovementAndVibrateHands", randomPlayerPhotonView.Owner);
//     }

//     private void VibrateHands()
//     {
//         StartCoroutine(EasyInputs.Vibration(EasyHand.LeftHand, 0.3f, 0.4f));    
//         StartCoroutine(EasyInputs.Vibration(EasyHand.RightHand, 0.3f, 0.4f));
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (!photonView.IsMine) return;
//         if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("OthersVRHand")) return; //if you want this to be an if statement, use de morgans law!

        
//         //Other will be null when: tagger body touches taggee hand, tagger hand touches taggee hand. 
//         TagScript6 otherTagScript6 = other?.gameObject.GetComponent<TagScript6>(); 
//         // if other is a VRHand, then other HAS NO TAGSCRIPT. The null conditional operator (?) in otherTagScript6 sets it to null when other has no tag script, preventing a NullReferenceException.
//         if (otherTagScript6 != null) {
//             if (isInfected && touchbackCountdown <= 0f && !otherTagScript6.isInfected)
//             {
//                 // an "it" player handles stuff here.
//                 VibrateHands(); 
//                 photonView.RPC("UntagPlayer", RpcTarget.All);

//                 PhotonView otherPhotonView = otherTagScript6.photonView;
//                 otherPhotonView.RPC("TagPlayer", RpcTarget.All);
//                 otherPhotonView.RPC("PlayTagSound", RpcTarget.All);
//                 otherPhotonView.RPC("LimitMovementAndVibrateHands", otherPhotonView.Owner);
//             }
//         }

//     }

//     [PunRPC]
//     private void TagPlayer()
//     {
//         isInfected = true;
        
//         touchbackCountdown = touchbackDuration;

//         foreach (Renderer colourObject in colourObjects)
//         {
//             colourObject.material = itMaterial;
//         }
//     }

//     [PunRPC]
//     //this happens when you just got tagged.
//     private void LimitMovementAndVibrateHands() {
//         limitMovementCountdown = touchbackDuration;
//         VibrateHands();
//     }


//     [PunRPC]
//     private void PlayTagSound()
//     {
//         if (!tagSound.isPlaying && headTransform.position.z > divideLine)
//         {
//             tagSound.Play();
//         }
//     }

//     [PunRPC]
//     private void UntagPlayer()
//     {
//         isInfected = false;

//         foreach (Renderer colourObject in colourObjects)
//         {
//             colourObject.material = initialMaterial;
//         }
//     }
// }

