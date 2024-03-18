using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NT_Forward : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        speed = Random.Range(15f, 25f);
    }

    private void Update()
    {
        Vector2 position = transform.position;
        position.x -= -0.1f * speed * Time.deltaTime;
        transform.position = position;
    }

    private void Collision2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.CompareTag("NT_Destroy"))
        {
            GameManager.instance.pool.ReturnToPool(0, gameObject);
        }
    }
}
