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


