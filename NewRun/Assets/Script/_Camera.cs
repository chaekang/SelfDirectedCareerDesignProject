using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Camera : MonoBehaviour
{
    public float cameraSpeed;

    public GameObject player;

    private void Update()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3((direction.x + 5) * cameraSpeed * Time.deltaTime, (direction.y + 1) * cameraSpeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);
    }
}
