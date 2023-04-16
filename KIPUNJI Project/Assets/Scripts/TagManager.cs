using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.VR;
//same as player controller
public class TagManager : MonoBehaviour
{
	public PhotonView photonView;
	public GameObject thisPlayer;
	[SerializeField] private Color _taggedColour;

	private Color _initialColour;
	private bool _isTagged;
	private float _touchbackCountdown;
	[SerializeField] private float _touchbackDuration;
	private Component[] meshRenderers;
	
	void Awake() {
		StartCoroutine(waitThing());
		
	}
	IEnumerator waitThing() {
		yield return new WaitForSeconds(5);
		_initialColour = new Color(0f, 0f, 0f, 1f);//GetComponentInChildren<MeshRenderer>().material.color;
		if (PhotonNetwork.IsMasterClient) {

			thisPlayer.GetComponent<TagManager>().photonView.RPC("OnTagged", RpcTarget.AllBuffered); //maybe change to this keyword RpcTarget.AllBuffered is all players in room and new players in room and taggedd
		}
		
	}


	[PunRPC] //rpc stands for remote procedure calls
	public void OnTagged() {

		_isTagged = true;

		//start the touchbacks countdown
		_touchbackCountdown = _touchbackDuration;

		meshRenderers = GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer r in meshRenderers) {
			if (r.gameObject.tag != "DontColor") {
				r.material.color = _taggedColour;
			}
				
		}
		
	}

	[PunRPC]
	public void OnUnTagged() {
		//flag the player as tagged 
		_isTagged = false;
		//Restore the colour of the player to be the initial color
		
		meshRenderers = GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer r in meshRenderers) {
			if (r.gameObject.tag != "DontColor") {
				r.material.color = _initialColour;
		}
				
		}
	}

	private void Update() {
		if (photonView.IsMine) {
			//we only want this to run for the localPlayer
			if (_touchbackCountdown > 0f) {
				_touchbackCountdown -= Time.deltaTime;
			}
		}

	}

	void OnCollisionEnter(Collision other) {
		var otherPlayer = other.collider.GetComponent<TagManager>();
		if (otherPlayer != null) {

			//if we are tagged and not under the no touchbacks rule
			if (_isTagged && _touchbackCountdown <= 0f) {
				//untag ourselves, tag the other player
				photonView.RPC("OnUnTagged", RpcTarget.AllBuffered);

				otherPlayer.photonView.RPC("OnTagged", RpcTarget.AllBuffered);

			}
		}
	}

}



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.VR;
//same as player controller
public class TagManager : MonoBehaviour
{
	public PhotonView photonView;
	public GameObject thisPlayer;
	[SerializeField] private Color _taggedColour;

	private Color _initialColour;
	private bool _isTagged;
	private float _touchbackCountdown;
	[SerializeField] private float _touchbackDuration;
	
	
	void Awake() {
		PhotonVRManager.SetColour(new Color(0f, 0f, 0f, 1f));
		StartCoroutine(waitThing());
		
	}
	IEnumerator waitThing() {
		yield return new WaitForSeconds(5);
		_initialColour = new Color(0f, 0f, 0f, 1f);//GetComponentInChildren<MeshRenderer>().material.color;
		if (PhotonNetwork.IsMasterClient) {

			thisPlayer.GetComponent<TagManager>().photonView.RPC("OnTagged", RpcTarget.AllBuffered); //maybe change to this keyword RpcTarget.AllBuffered is all players in room and new players in room and taggedd
		}
		
	}


	[PunRPC] //rpc stands for remote procedure calls
	public void OnTagged() {

		_isTagged = true;

		//start the touchbacks countdown
		_touchbackCountdown = _touchbackDuration;

		PhotonVRManager.SetColour(_taggedColour);
		
	}

	[PunRPC]
	public void OnUnTagged() {
		//flag the player as tagged 
		_isTagged = false;
		//Restore the colour of the player to be the initial color
		PhotonVRManager.SetColour(_initialColour); //set color only works for you
	}

	private void Update() {
		if (photonView.IsMine) {
			//we only want this to run for the localPlayer
			if (_touchbackCountdown > 0f) {
				_touchbackCountdown -= Time.deltaTime;
			}
		}

	}

	void OnCollisionEnter(Collision other) {
		var otherPlayer = other.collider.GetComponent<TagManager>();
		if (otherPlayer != null) {

			//if we are tagged and not under the no touchbacks rule
			if (_isTagged && _touchbackCountdown <= 0f) {
				//untag ourselves, tag the other player
				photonView.RPC("OnUnTagged", RpcTarget.AllBuffered);

				otherPlayer.photonView.RPC("OnTagged", RpcTarget.AllBuffered);

			}
		}
	}

}






using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
//same as player controller
public class TagManager : MonoBehaviour
{
	public PhotonView photonView;
	[SerializeField] private Color _taggedColour;

	private Color _initialColour;
	private bool _isTagged;
	public GameObject thisPlayer;
	private Component[] meshRenderers;
	
	void Awake() {
		Debug.Log("Awake");
		if (PhotonNetwork.IsMasterClient) {
		Debug.Log("isMasterClient");
		thisPlayer.GetComponent<TagManager>().photonView.RPC("OnTagged", RpcTarget.AllBuffered); //maybe change to this keyword RpcTarget.AllBuffered is all players in room and new players in room and taggedd
		}
		_initialColour = GetComponentInChildren<MeshRenderer>().material.color;
	}


	}
	void Update() {

		if (PhotonNetwork.IsMasterClient) {
			thisPlayer.GetComponent<TagManager>().photonView.RPC("OnTagged", RpcTarget.AllBuffered); //maybe change to this keyword RpcTarget.AllBuffered is all players in room and new players in room and taggedd
		}
		

	}
	[PunRPC] //rpc stands for remote procedure calls
	public void OnTagged() {

		_isTagged = true;
		//Change the colour of the player's meshrenders to be the tagged colour unless they have `DontColor` tag
		meshRenderers = GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer r in meshRenderers) {
			if (r.gameObject.tag != "DontColor") {
				r.material.color = _taggedColour;
			}
				
		}
		
	}

	[PunRPC]
	public void OnUnTagged() {
		//flag the player as tagged 
		_isTagged = false;
		//Restore the colour of the player to be the initial color
		GetComponentInChildren<MeshRenderer>().material.color = _initialColour;
	}


}

















using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : MonoBehaviour
{
	public GameObject leftHand;
	public GameObject rightHand;
	public GameObject body;
	public Texture defaultTexture;
	public List<Texture> taggerTextures = new List<Texture>();
	public float taggerTextureChangeInterval = 2.0f;

	private bool gameStarted = false;
	private List<GameObject> players = new List<GameObject>();
	private int taggerIndex = -1;
	private float timeSinceLastTaggerTextureChange = 0.0f;
	private Texture currentTaggerTexture;
	private GameObject objectToChangeTexture;

	void Start()
	{
       	players.Add(leftHand);
    	players.Add(rightHand);
    	players.Add(body);

    	currentTaggerTexture = defaultTexture;
	}

	void Update()
	{
    	    	if (!gameStarted)
    	{
        	        	if (players.Count > 0)
        	{
                        	gameStarted = true;
            	taggerIndex = Random.Range(0, players.Count);
            	objectToChangeTexture = players[taggerIndex];
            	objectToChangeTexture.GetComponent<Renderer>().material.mainTexture = currentTaggerTexture;
        	}
    	}
    	else
    	{
            	bool allTaggers = true;
        	foreach (GameObject player in players)
        	{
            	if (player.GetComponent<Renderer>().material.mainTexture != currentTaggerTexture)
            	{
                	allTaggers = false;
                	break;
            	}
        	}

        	if (allTaggers)
        	{
                        	gameStarted = false;
            	taggerIndex = -1;
            	foreach (GameObject player in players)
            	{
                	player.GetComponent<Renderer>().material.mainTexture = defaultTexture;
            	}
        	}
        	else
        	{
                      	timeSinceLastTaggerTextureChange += Time.deltaTime;
            	if (timeSinceLastTaggerTextureChange >= taggerTextureChangeInterval)
            	{
                	timeSinceLastTaggerTextureChange = 0.0f;
                	objectToChangeTexture.GetComponent<Renderer>().material.mainTexture = currentTaggerTexture;
            	}
        	}
    	}
	}

	void OnTriggerEnter(Collider other)
	{
        	if (gameStarted && taggerIndex >= 0 && other.gameObject == body && (other.gameObject.transform.parent == leftHand.transform || other.gameObject.transform.parent == rightHand.transform))
    	{
            	other.gameObject.GetComponent<Renderer>().material.mainTexture = currentTaggerTexture;
    	}
	}

	public void SetTaggerTexture(Texture texture)
	{
        	currentTaggerTexture = texture;
	}

	public void SetObjectToChangeTexture(GameObject obj)
	{
        	objectToChangeTexture = obj;
	}
}



*/
