using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Camera : MonoBehaviour
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
        Vector3 moveVector = new Vector3((direction.x + 6) * cameraSpeed * Time.deltaTime, (direction.y + 0.5f) * cameraSpeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);

        if (playerScript.camMove)
        {
            playerCamera.fieldOfView = 103;
            playerScript.playerSpeed = 0f;
            cameraSpeed = 0f;
        }
        else
        {
            playerCamera.fieldOfView = 60;
        }
    }
}
