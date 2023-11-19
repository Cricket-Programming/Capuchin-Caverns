using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float speedX;
    [SerializeField] float speedY;
    [SerializeField] float speedZ;
    
    private float rp(float speedValue)//rotateparam
    {
        return speedValue * 360 * Time.deltaTime;
    }
    private void Update()
    {
        transform.Rotate(rp(speedX), rp(speedY), rp(speedZ));   //360 is 1 full rotation    
    }
}
