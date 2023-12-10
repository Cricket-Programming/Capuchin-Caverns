using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.VR.Player; 
using easyInputs;

//SEE DOCUMENTATION (in google drive coding) FOR INFORMATION
//Direction to Tag Area: > Z


public class TagScript6 : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform headTransform; // Head GameObject of PhotonVR player prefab.
    [SerializeField] private Material itMaterial;
    [SerializeField] private AudioSource tagSound;
    [Tooltip("When player gets tagged, this is the amount of time in seconds before the player can start tagging other players. Also, it is the amount of time movement is limited.")]
    [SerializeField] private float touchbackDuration = 2f;
    [SerializeField] private float divideLine;  
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

    private void Start()
    {
        colourObjects = GetComponent<PhotonVRPlayer>().ColourObjects;
        initialMaterial = colourObjects[0].material;

        itMaterialName = itMaterial.name + " (Instance)";

        gorillaPlayerRigidbody = GameObject.Find("GorillaPlayer")?.GetComponent<Rigidbody>();
        if (gorillaPlayerRigidbody == null)
        {
            Debug.LogError("Make sure that the name of the gorilla player is `GorillaPlayer`");
        }
        Invoke("newPlayerCatchUp", 0.05f); //this gives it time to process avoiding an argumentOutOfRange exception
    }

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

        if (PhotonNetwork.IsMasterClient)
        {
            UpdateInfectionPlayersList();
            if (infectionPlayers.Count > 1) {
                CheckIfNoneInfected();
            }
        }
        //Debug.Log(headTransform.position.z);
        // These stuff are for doing tag entrance logic.
        ////red bar is the enter
        ///inside tag area
        if (headTransform.position.z > divideLine  && infectionPlayers.Count > 1 && performFlag)
        {
            photonView.RPC("PlayTagSound", RpcTarget.All);
            performFlag = false;
            
        } 
        //outside tag area.
        else if (headTransform.position.z <= divideLine && isInfected) {
            photonView.RPC("UntagPlayer", RpcTarget.All);

        }
        //outside no mans zone.
        if (headTransform.position.z <= divideLine && !performFlag) {
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
            //Debug.Log(player.transform.position.z);
            if (player.transform.position.z > divideLine - 0.5f)
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
        if (!tagSound.isPlaying && headTransform.position.z > divideLine)
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

/*
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.VR.Player; 
using easyInputs;

//How to set up tag in a project without it.
// 1) say thank you gorilla tag for inspiration.
// 2) add player model mesh renderer/filter and collider onto your player model. Make sure that the objects you want to be tagged is referenced in Photon.VR.Player's colourObjects.
// 3) configure tag/infection script, add the audiosources, add a sphere collider set to isTrigger, and add a rigidbody and uncheck use gravity on the player parent.
// 4) Set up tag entrance by putting values into script.
// 5) In PhotonVRPlayer.cs put this.transform.position = PhotonVRManager.Manager.Head.transform.position in Update() function, for some reason this makes head not go random place.
// 6) This is for handtagging:  Add mesh collider is trigger and OthersVRHand tag and Walkthrough layer to VR Hand left and VRHandRight. Also, for both, add the TagIfOtherVRHandScript.

//initial color moved to no mans zone possibly
//tag sound being buffered.
public class TagScript6 : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform headTransform; // Head GameObject of PhotonVR player prefab.
    [SerializeField] private Material itMaterial;
    [SerializeField] private AudioSource tagSound;
    [SerializeField] private float touchbackDuration = 2f;

    private Material initialMaterial;
    private string itMaterialName;
    private bool isInfected = false;
    private float touchbackCountdown;
    private float limitMovementCountdown;
    private Rigidbody gorillaPlayerRigidbody;
    private List<MeshRenderer> colourObjects = new List<MeshRenderer>();

    private bool performFlag = true; //this prevents things happening multiple times repeatly.

    private GameObject[] players;
    private List<GameObject> infectionPlayers = new List<GameObject>();

    private void Start()
    {
        colourObjects = GetComponent<PhotonVRPlayer>().ColourObjects;
        initialMaterial = colourObjects[0].material;

        itMaterialName = itMaterial.name + " (Instance)";

        gorillaPlayerRigidbody = GameObject.Find("GorillaPlayer")?.GetComponent<Rigidbody>();
        if (gorillaPlayerRigidbody == null)
        {
            Debug.LogError("Make sure that the name of the gorilla player is `GorillaPlayer`");
        }
        Invoke("newPlayerCatchUp", 0.05f); //this gives it time to process avoiding a argumentOutOf range exception
    }

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
        // These stuff are for doing tag entrance logic.
        //inside of no mans zone.
        if (headTransform.position.x > 11f && headTransform.position.x < 16f  && infectionPlayers.Count > 1 && performFlag)
        {
            photonView.RPC("PlayTagSound", RpcTarget.All);
            performFlag = false;
        } 
        //outside tag area.
        else if (headTransform.position.x > 16f && isInfected) {
            photonView.RPC("UntagPlayer", RpcTarget.All);

        }
        //outside no mans zone.
        if ((headTransform.position.x < 11f || headTransform.position.x > 16f) && !performFlag) {
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
        
        if (PhotonNetwork.IsMasterClient)
        {
            UpdateInfectionPlayersList();
            if (infectionPlayers.Count > 1) {
                CheckIfNoneInfected();
            }
        }
    }

    private void UpdateInfectionPlayersList()
    {
        infectionPlayers.Clear();
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.transform.position.x < 16f)
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
        if (!tagSound.isPlaying && headTransform.position.x < 16f)
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
*/