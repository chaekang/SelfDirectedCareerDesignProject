using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Init_Player : MonoBehaviour
{
    public GameObject initPlayer;
    public GameObject initCamera;
    public GameObject playerCamera;
    public GameObject mainPlayer;
    public GameObject velocitiyBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            initCamera.SetActive(false);
            initPlayer.SetActive(false);
            playerCamera.SetActive(true);
            velocitiyBar.SetActive(true);
            mainPlayer.SetActive(true);
        }    
    }
}
