using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// This script loads the scene specified by sceneName by the player OnTriggerEnters.
public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("HandTag")) {
            SceneManager.LoadScene(sceneName);
        }
        
    }
}
