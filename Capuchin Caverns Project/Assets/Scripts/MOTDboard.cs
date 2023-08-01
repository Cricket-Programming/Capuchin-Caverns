using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Networking;
public class MOTDboard : MonoBehaviour
{
    //In github, the repository holding the MOTD text file MUST to be public.

    // How to change MOTD text:
    // 1) On github.com, locate MOTD repository for Capuchin Caverns.
    // 2) Click on motd.txt file. 
    // 3) Edit (Pen looking button) OR press e 
    // 4) Make your edits 
    // 5) Commit changes/Ctrl S 
    // 6) Wait a little bit for changes to sync.
    private string textURL = "https://raw.githubusercontent.com/Cricket-Programming/motd/main/motd.txt"; //I changed this to public from samsams script
    private TMP_Text MOTD;
    void Start()
    {
        MOTD = GetComponent<TMP_Text>();
        StartCoroutine(GetText(textURL));
    }
    public IEnumerator GetText(string tURL)
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