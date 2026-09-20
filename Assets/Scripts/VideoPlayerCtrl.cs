using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerCtrl : MonoBehaviour
{
    
    VideoPlayer vp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vp = GetComponent <VideoPlayer> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayVideo()
    {
        vp.Play();
    }
}
