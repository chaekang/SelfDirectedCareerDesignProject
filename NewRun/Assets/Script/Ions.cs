using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ions : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ApplyRandomForce();
    }

    void ApplyRandomForce()
    {
        Vector2 randomForce = Random.insideUnitCircle.normalized * Random.Range(0.04f, 0.09f);
        rb.AddForce(randomForce, ForceMode2D.Impulse);

        float randomTorque = Random.Range(-1.0f, 1.0f);
        rb.AddTorque(randomTorque, ForceMode2D.Impulse);
    }
}
