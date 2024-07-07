using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class _Camera : MonoBehaviour
{
    public GameObject player;

    public float cameraSpeed;

    public Light2D sceneLight;

    private _Player playerScript;
    private Camera playerCamera;
    private ChangeScene sceneScript;


    private void Start()
    {
        playerScript = FindObjectOfType<_Player>();
        sceneScript = FindObjectOfType<ChangeScene>();   
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
            if(sceneScript.endStage == true)
            {
                sceneLight.shapeLightFalloffSize = 120;
            }
            
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
