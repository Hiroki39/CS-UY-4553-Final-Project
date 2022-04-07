using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private string[] filenames = { "Nebula.mp4", "Earth.mp4", "Multi.mp4", "Red.mp4" };
    VideoPlayer videoplayer;

    // Start is called before the first frame update
    void Start()
    {
        string fileName = filenames[Random.Range(0, filenames.Length)];
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        videoplayer = GetComponent<VideoPlayer>();
        videoplayer.url = filePath;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Play();
        }
    }

    void Play()
    {
        videoplayer.Play();
        videoplayer.isLooping = true;
    }
}
