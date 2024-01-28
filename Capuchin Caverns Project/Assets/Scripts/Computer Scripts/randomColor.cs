using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;

// Sets the player to a random color.
public class RandomColor : MonoBehaviour
{
	private void OnTriggerEnter()
	{
		float r = Random.Range(0f, 1f);
		float g = Random.Range(0f, 1f);
		float b = Random.Range(0f, 1f);
		PhotonVRManager.SetColour(new Color(r, g, b, 1f));

    }
}

// This Script is permanently broken, only left for visual purposes.
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Photon.VR;

// public class randomColor : MonoBehaviour
// {
// 	private void OnTriggerEnter()
// 	{
		
// 		float r, g, b;
// 		r = g = b = Random.Range(0f, 1f);	
// 		PhotonVRManager.SetColour(new Color(r, g, b, 1f));

//     }

// }


