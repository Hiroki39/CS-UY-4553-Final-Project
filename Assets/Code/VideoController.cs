using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public string filename;
    VideoPlayer videoplayer;

    // Start is called before the first frame update
    void Start()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, filename);
        videoplayer = GetComponent<VideoPlayer>();
        videoplayer.url = filePath;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) {
            Play();
        }
    }

    void Play()
    {
        videoplayer.Play();
        videoplayer.isLooping = true;
    }
}
