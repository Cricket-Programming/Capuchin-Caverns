using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    public LineRenderer line;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ray"))
        {
            line.enabled = !line.enabled;
        }
    }
}
