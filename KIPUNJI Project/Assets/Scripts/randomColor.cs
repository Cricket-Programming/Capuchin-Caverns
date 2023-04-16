using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;
public class randomColor : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		float r = Random.Range(0f, 1f);
		float g = Random.Range(0f, 1f);
		float b = Random.Range(0f, 1f);
		PhotonVRManager.SetColour(new Color(r, g, b, 1f));

    }
}
