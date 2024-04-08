using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dendrite : MonoBehaviour
{
    float time = 0;
    bool appear = false;
    public GameObject targetPosition;
    float currentSpeed = 0f;
    float acceleration = 2;

    void Update()
    {
        transform.localScale = new Vector3(0.07f, 0.07f, 1f) * (1 + time);
        time += Time.deltaTime;

        if (time > 3f)
        {
            transform.localScale = new Vector3(0.25f, 0.25f, 1f);
            GameManager.instance.player.appear = false;
            appear = true;
        }
        if (appear)
        {
            Vector3 direction = (targetPosition.transform.position - transform.position).normalized;

            currentSpeed += acceleration * Time.deltaTime;

            float distanceToMove = currentSpeed * Time.deltaTime;

            transform.position += direction * distanceToMove;

            if (Vector3.Distance(transform.position, targetPosition.transform.position) < 0.1f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
