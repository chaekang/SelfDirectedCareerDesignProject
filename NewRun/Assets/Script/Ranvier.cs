using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranvier : MonoBehaviour
{
    private Transform player;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();

        transform.position += direction * 7f * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("플레이어 이온 흡입");
        Destroy(gameObject);
    }
}
