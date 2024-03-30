using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NT_Forward : MonoBehaviour
{
    public float speed;
    private void Start()
    {
        speed = Random.Range(25f, 35f);
    }

    private void Update()
    {

        Vector2 position = transform.position;
        position.x -= -0.1f * speed * Time.deltaTime;
        transform.position = position;

    }
}
