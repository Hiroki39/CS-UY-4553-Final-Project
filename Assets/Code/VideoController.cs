using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class VideoController : MonoBehaviour
{
    public string videoName;
	public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);
        videoPlayer.Play();
        videoPlayer.isLooping = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
