using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ion : MonoBehaviour
{
    private Rigidbody2D rb;

    public float timeOutside = 0f;
    private bool isInArea = true;

    private float randomTorque;
    private Vector2 randomForce;

    private Vector3 currentPosition;
    private Vector3 initialPosition;
    private Vector3 colliderCenter;
    private Vector3 forceDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPosition = transform.position;
        initialPosition = transform.position;
    }

    void Update()
    {
        ApplyRandomForce();
        countDown();
    }

    void ApplyRandomForce()
    {
        randomForce = Random.insideUnitCircle.normalized * Random.Range(0.0025f, 0.0045f);
        randomTorque = Random.Range(-1.0f, 1.0f);

        rb.angularVelocity = 0f;

        rb.AddForce(randomForce, ForceMode2D.Impulse);
        rb.AddTorque(randomTorque, ForceMode2D.Impulse);
    }

    void countDown()
    {
        if (!isInArea)
        {
            timeOutside += Time.deltaTime;
            if (timeOutside >= 2f)
            {
                transform.position = initialPosition;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IonItemSpace")
        {
            isInArea = false;

            colliderCenter = collision.bounds.center;
            currentPosition = transform.position;

            forceDirection = colliderCenter - currentPosition;

            rb.AddForce(forceDirection.normalized * 0.35f, ForceMode2D.Impulse);
        }
        else if(collision.gameObject.tag == "BackgroundIon_Space")
        {
            Debug.Log(collision.gameObject.tag);
            isInArea = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IonItemSpace" || collision.gameObject.tag == "BackgroundIon_Space")
        {
            isInArea = true;
            timeOutside = 0f;   
        }
    }


}
