using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Networking; // For UnityWebRequest.
    // How to set up MOTD:
    // 1) Create Github repository. THE REPOSITORY HOLDING THE MOTD TEXT FILE MUST BE PUBLIC.
    // 2) Create a .txt file holding your MOTD text.
    // 3) Put the URL to the raw .txt file into the textURL data field in the inspector.  

    // How to change MOTD text:
    // 1) On github.com, locate MOTD repository for Capuchin Caverns.
    // 2) Click on motd.txt file. 
    // 3) Edit (Pen looking button) OR press e 
    // 4) Make your edits 
    // 5) Commit changes/Ctrl S 
    // 6) Wait a little bit for changes to sync.
public class MOTDboard : MonoBehaviour
{
    [SerializeField] private string textURL = "https://raw.githubusercontent.com/Cricket-Programming/motd/main/motd.txt"; 
    private TMP_Text MOTD;
    void Start()
    {
        MOTD = GetComponent<TMP_Text>();
        StartCoroutine(GetTextFromGithub(textURL));
    }
    public IEnumerator GetTextFromGithub(string tURL)
    {
        UnityWebRequest www = UnityWebRequest.Get(tURL);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
            string pathTxt = www.downloadHandler.text;
            //Debug.Log(pathTxt);
            MOTD.text = pathTxt;
        }
    }
}