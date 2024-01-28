using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This script rotates the gameobject. 
public class Rotate : MonoBehaviour
{
    // The speed is the seconds needed to make one full rotation.
    [SerializeField] float speedX;
    [SerializeField] float speedY;
    [SerializeField] float speedZ;
    
    private float rp(float speedValue)// RotateParam
    {
        return speedValue * 360 * Time.deltaTime;
    }
    private void Update()
    {
        transform.Rotate(rp(speedX), rp(speedY), rp(speedZ));   // 360 is 1 full rotation    
    }
}
