//ENTRANCE BASED TAG MODE SCRIPT WORKING 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.VR.Player; 
using easyInputs;

//SEE DOCUMENTATION (in google drive coding) FOR INFORMATION

public class TagScript6 : MonoBehaviourPunCallbacks
{
    [SerializeField] private Material itMaterial;
    [SerializeField] private AudioSource tagSound;
    
    [Tooltip("When player gets tagged, this is the amount of time before the player can start tagging other players. It is also the amount of time movement is limited.")]
    [SerializeField] private float touchbackDuration = 2f;

    [HideInInspector] public Material initialMaterial; //gets changed by ChangeSkin.cs
    private string itMaterialName;
    private bool isInfected = false;
    private float touchbackCountdown;
    private float limitMovementCountdown;
    
    private Rigidbody gorillaPlayerRigidbody;
    private List<MeshRenderer> colourObjects = new List<MeshRenderer>();

    private bool performFlag = true; //this prevents things happening multiple times repeatly.

    private GameObject[] players;
    private List<GameObject> infectionPlayers = new List<GameObject>();

    private bool isInTagArea;

    private void Start()
    {
        colourObjects = GetComponent<PhotonVRPlayer>().ColourObjects;
        initialMaterial = colourObjects[0].material; 

        itMaterialName = itMaterial.name + " (Instance)";

        gorillaPlayerRigidbody = GameObject.Find("GorillaPlayer")?.GetComponent<Rigidbody>();
        if (gorillaPlayerRigidbody == null)
        {
            Debug.LogError("The name of the gorilla player must be `GorillaPlayer` or TagScript won't work correctly");
        }

        Invoke("newPlayerCatchUp", 0.05f); //this gives it time to process avoiding an argumentOutOfRange exception.
    }


    public void ChangeIsInTagArea(bool boolean) {
        photonView.RPC("_RPCChangeIsInTagArea", RpcTarget.AllBuffered, boolean); // Buffering seems to work, and this will automatically catch up players with the value of it.
        
    }
    [PunRPC]
    private void _RPCChangeIsInTagArea(bool value) {
        isInTagArea = value;
    }

    public bool GetIsInTagArea() => isInTagArea;

    // because for TagPlayer stuff, RpcTarget.AllBuffered is not working, this manually "catches up" the new player with who is it.
    private void newPlayerCatchUp() {
        UpdateInfectionPlayersList();
        foreach (GameObject infectionPlayer in infectionPlayers)
        {
            TagScript6 infectionPlayerTagScript6 = infectionPlayer.GetComponent<TagScript6>();
            Material infectionPlayerMaterial = infectionPlayerTagScript6.colourObjects[0].material; //the current material of the infectionPlayer
            if (infectionPlayerMaterial.name.Equals(itMaterialName)) {
                infectionPlayerTagScript6.photonView.RPC("TagPlayer", photonView.Owner);
            }
        }
    }


    private void Update()
    {   
        //Debug.Log(GetIsInTagArea());
        if (PhotonNetwork.IsMasterClient)
        {
            UpdateInfectionPlayersList();
            //Debug.Log(infectionPlayers.Count);
            if (infectionPlayers.Count > 1) {
                CheckIfNoneInfected();
            }
        }
        // These stuff are for doing tag entrance logic.
        ////red is the enter
        if (GetIsInTagArea() && infectionPlayers.Count > 1 && performFlag)
        {
            photonView.RPC("PlayTagSound", RpcTarget.All);
            performFlag = false;
        } 
        //outside tag area.
        else if (!GetIsInTagArea() && isInfected) {
            photonView.RPC("UntagPlayer", RpcTarget.All);

        }
        //outside tag area.
        if (!GetIsInTagArea() && !performFlag) {
            performFlag = true;
        }

        // Touchback countdown
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
            //Debug.Log(player.GetComponent<TagScript6>().GetIsInTagArea());         
            if (player.GetComponent<TagScript6>().GetIsInTagArea()) 
            {      
                infectionPlayers.Add(player);
            }
        }
    }

    private void CheckIfNoneInfected()
    {
        //if there is a lava, then someone is infected.
        foreach (GameObject infectionPlayer in infectionPlayers)    
        {
            Material infectionPlayerMaterial = infectionPlayer.GetComponent<TagScript6>().colourObjects[0].material;
            if (infectionPlayerMaterial.name.Equals(itMaterialName))
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

    //handles what happens when two player collide, called from TagScriptRemoteCollider which is on a child gameObject with a tag of "PlayerModel" inside head which is inside player parent

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
                photonView.RPC("UntagPlayer", RpcTarget.All);

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

        foreach (Renderer colourObject in colourObjects)
        {
            colourObject.material = itMaterial;
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

    [PunRPC]
    private void UntagPlayer()
    {
        isInfected = false;

        foreach (Renderer colourObject in colourObjects)
        {
            colourObject.material = initialMaterial;
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

