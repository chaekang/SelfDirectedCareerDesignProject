using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro_Video : MonoBehaviour
{
    public VideoPlayer intro;
    // Start is called before the first frame update
    void Start()
    {
        intro = GetComponent<VideoPlayer>();
        intro.loopPointReached += VideoEnd;
    }

    void VideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(2);
    }
}
