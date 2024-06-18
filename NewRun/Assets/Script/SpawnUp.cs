using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUp : MonoBehaviour
{
    BoxCollider2D area;

    private float timer;

    private void Awake()
    {
        area = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "NT")
        {
            Spawn();
        }
    }

    private Vector2 GetRandomPosition(BoxCollider2D area)
    {
        Vector2 basePosition = transform.position;  //오브젝트의 위치

        //x, y축 좌표 얻기
        float posX = basePosition.x;
        float posY = basePosition.y;

        Vector2 spawnPos = new Vector2(posX, posY);

        return spawnPos;
    }

    void Spawn()
    {
        float curPoint = GameManager.instance.SynapseBar.curPoint;
        Vector2 spawnPosition = GetRandomPosition(area);

        GameObject spawnedPrefab = GameManager.instance.pool.Get(1);
        spawnedPrefab.transform.position = spawnPosition;

    }
}
