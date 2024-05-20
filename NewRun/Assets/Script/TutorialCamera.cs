using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCamera : MonoBehaviour
{
    public GameObject player;

    public float cameraSpeed;

    private _Player playerScript;
    private Camera playerCamera;

    private void Start()
    {
        playerScript = FindObjectOfType<_Player>();
        playerCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (playerScript.camMove)
        {
            transform.parent = null;
            playerCamera.fieldOfView = 103;
            playerScript.playerSpeed = 0f;
        }
    }
}
