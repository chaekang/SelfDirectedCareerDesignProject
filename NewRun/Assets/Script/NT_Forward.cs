using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NT_Forward : MonoBehaviour
{
    float speed;
    
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("NT_Destroy"))
        {
            speed = 0;
            Invoke("ReturnToPoolWithDelay", 0.1f);
        }
    }

    private void ReturnToPoolWithDelay()
    {
        GameManager.instance.pool.ReturnToPool(0, gameObject);
        speed = Random.Range(25f, 35f);
    }

}
