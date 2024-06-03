using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomaPlayer : MonoBehaviour
{
    public GameObject targetPosition;
    float acceleration = 5;
    bool appear = false;
    float time = 0;

    private float currentSpeed = 0f;

    private void Update()
    {
        transform.localScale = new Vector3(0.07f, 0.07f, 1f) * (1 + 2 * time);
        time += Time.deltaTime;

        if (time > 3f)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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
                GameManager.instance.dendriteManager.SomaPlayer = true;
            }
        }
    }
}
