using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    BoxCollider2D area;

    private float timer;
    public bool ntFinish = false;
    private bool isSpawning = false;

    private void Awake()
    {
        area = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (GameManager.instance.player.onSynapse && !ntFinish)
        {
            timer += Time.deltaTime;

            float randomTime = Random.Range(0.5f, 0.8f);

            if (timer > randomTime)
            {
                StartCoroutine(SpawnCoroutine());
                timer = 0;
            }
        }
    }

    private Vector2 GetRandomPosition(BoxCollider2D area)
    {
        Vector2 basePosition = transform.position;  // 오브젝트의 위치

        // x, y축 랜덤 좌표 얻기
        float posX = basePosition.x;
        float posY = basePosition.y;

        Vector2 spawnPos = new Vector2(posX, posY);

        return spawnPos;
    }

    IEnumerator SpawnCoroutine()
    {
        isSpawning = true;
        Spawn();
        yield return new WaitForSeconds(5f);
        GameManager.instance.SynapseBar.curPoint = 0;
        StartCoroutine(RestCoroutine());
        isSpawning = false;
    }

    IEnumerator RestCoroutine()
    {
        yield return new WaitForSeconds(2f);  // 2초 대기
        ntFinish = true;
    }

    void Spawn()
    {
        float curPoint = GameManager.instance.SynapseBar.curPoint;
        Vector2 spawnPosition = GetRandomPosition(area);

        GameObject spawnedPrefab = GameManager.instance.pool.Get(0);
        spawnedPrefab.transform.position = spawnPosition;
    }
}
