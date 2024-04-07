using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomaDendrite : MonoBehaviour
{
    public GameObject targetPosition;
    public float speed;

    Vector3 velo = Vector3.zero;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition.transform.position, ref velo, speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Soma"))
        {
            gameObject.SetActive(false);
        }
    }
}