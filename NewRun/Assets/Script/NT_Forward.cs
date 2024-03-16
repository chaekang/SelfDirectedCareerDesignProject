using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NT_Forward : MonoBehaviour
{
    public float speed;
    private void Update()
    {
        Vector2 position = transform.position;
        position.x -= -0.1f * speed * Time.deltaTime;
        transform.position = position;
    }
}
