using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Television : MonoBehaviour
{
    public List<VideoClip> videos;
    public int currentvid;
    public VideoPlayer player;

    public int lenght;
    void Update()
    {
        lenght = videos.Count;
        foreach(VideoClip i in videos) 
        {
            int e = videos.IndexOf(i);
            if(e == currentvid) 
            {
                player.clip = i;
            }
            
        }
    }
}
