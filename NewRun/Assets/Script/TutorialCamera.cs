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

        cameraSpeed = playerScript.playerSpeed;
    }

    private void Update()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3((direction.x + 8) * cameraSpeed * Time.deltaTime, (direction.y - 1.5f) * cameraSpeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);

        if (playerScript.camMove)
        {
            playerCamera.fieldOfView = 103;
            playerScript.playerSpeed = 0f;
        }
        else
        {
            playerCamera.fieldOfView = 60;
        }
    }
}
