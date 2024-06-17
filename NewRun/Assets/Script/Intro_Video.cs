using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class Intro_Video : MonoBehaviour
{
    public VideoPlayer intro;
    public Canvas fadeCanvas;
    public RawImage intro_zoom;
    private float zoomTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        intro = GetComponent<VideoPlayer>();
    }

    void Awake()
    {
        intro.loopPointReached += VideoEnd;
    }

    void VideoEnd(VideoPlayer vp)
    {
        StartCoroutine(ZoomEffect());
        fadeCanvas.gameObject.SetActive(true);
        
    }

    IEnumerator ZoomEffect()
    {
        zoomTime = 0f;
        while (zoomTime < 2f)
        {
            zoomTime += Time.deltaTime;
            intro_zoom.rectTransform.localScale += Vector3.one * Time.deltaTime * 8f; 
            intro_zoom.rectTransform.localPosition += new Vector3(0, Time.deltaTime * 800f, 0); 
            yield return null; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(2);
    }
}
