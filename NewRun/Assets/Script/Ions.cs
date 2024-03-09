using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ions : MonoBehaviour
{
    private Rigidbody2D rb;

    private float randomTorque;
    private Vector2 randomForce;
    private Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPosition = transform.position;
    }

    void Update()
    {
        ApplyRandomForce();
    }

    void ApplyRandomForce()
    {
        randomForce = Random.insideUnitCircle.normalized * Random.Range(0.0015f, 0.0035f);
        randomTorque = Random.Range(-1.0f, 1.0f);

        rb.angularVelocity = 0f;

        rb.AddForce(randomForce, ForceMode2D.Impulse);
        rb.AddTorque(randomTorque, ForceMode2D.Impulse);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IonSpace" || collision.gameObject.tag == "IonItemSpace")
        {
            Debug.Log("Ion 범위 초과");

            Vector3 closestPoint = collision.ClosestPoint(currentPosition);
            transform.position = closestPoint;
        }
    }


}
